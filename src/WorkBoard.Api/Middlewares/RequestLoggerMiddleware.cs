using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBoard.Api.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            _logger.Information("Executed request: {@requestInfo}", new RequestInfo(context));
        }
    }

    internal class RequestInfo
    {
        public RequestInfo()
        {

        }

        public RequestInfo(HttpContext context)
        {
            Method = context.Request.Method;
            Url = context.CompleteUrl();
            UserAgent = context.Request.Headers["User-Agent"].ToString();
            Ip = context.Connection.RemoteIpAddress.ToString();
            if (context.User.Identity.IsAuthenticated)
            {
                User = new Dictionary<string, object>();
                var claimId = context.User.Claims.FirstOrDefault(c => c.Type.Equals("sub", StringComparison.CurrentCultureIgnoreCase));
                var claimName = context.User.Claims.FirstOrDefault(c => c.Type.Equals("name", StringComparison.CurrentCultureIgnoreCase));
                var claimCompanyId = context.User.Claims.FirstOrDefault(c => c.Type.Equals("companyId", StringComparison.CurrentCultureIgnoreCase));
                var claimRole = context.User.Claims.FirstOrDefault(c => c.Type.Equals("role", StringComparison.CurrentCultureIgnoreCase));

                User.Add("Id", claimId.Value);
                User.Add("Name", claimName.Value);
                User.Add("Role", claimRole.Value);
                if (claimCompanyId != null)
                {
                    User.Add("CompanyId", claimCompanyId.Value);
                }
            }
        }

        public string Method { get; set; }
        public string Url { get; set; }
        public Dictionary<string, object> User { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
    }
}
