using Hco.Base.DataAccess.Exceptions;
using Hco.Base.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Qurl.Exceptions;
using Serilog;
using WorkBoard.Api.Exceptions;
using WorkBoard.Application.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WorkBoard.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.Warning("The response has already started, the error handler will not be executed.");
                    throw;
                }
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            var errorResult = new ErrorResultModel
            {
                Message = "Errors processing request.",
                Status = HttpStatusCode.InternalServerError,
                Code = ApiException.InternalError
            };

            if (exception is DomainException || exception is CommandException || exception is QurlException)
            {
                errorResult.Status = HttpStatusCode.BadRequest;
                errorResult.Code = HttpStatusCode.BadRequest.ToString();
            }
            else if (exception is DataAccessException daEx)
            {
                errorResult.Status = HttpStatusCode.BadRequest;
                errorResult.Code = ApiException.DataAccessError;
            }
            else if (exception is ApiException apiEx)
            {
                errorResult.Message = exception.Message;
                errorResult.Status = apiEx.Status;
                errorResult.Code = apiEx.Code;
                errorResult.Payload = apiEx.Payload;
            }
            else
            {
                errorResult.Message = "An unexpected error occurred.";
            }

            logger.Error(exception, "Exception was thrown: {0}. Url: {1}", exception.Message, context.CompleteUrlWithMethod());

            var result = JsonConvert.SerializeObject(errorResult, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResult.Status;
            return context.Response.WriteAsync(result);
        }
    }
}
