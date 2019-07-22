using System;
using System.Net;
using System.Threading.Tasks;
using Hco.Base.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkBoard.Api.Exceptions;

namespace WorkBoard.Api.Filters
{
    public class TransactionActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionActionFilter(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _unitOfWork.Begin();

            var executed = await next();

            if (executed.Exception != null)
            {
                _unitOfWork.Rollback();
                return;
            }

            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException("Errors when persisting data.", HttpStatusCode.InternalServerError, ApiException.DataAccessError, innerException: ex);
            }
        }
    }
}
