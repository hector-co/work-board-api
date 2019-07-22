using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace WorkBoard.Api.Middlewares
{
    public static class HttpContextExtensions
    {
        public static string CompleteUrl(this HttpContext context)
        {
            return $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
        }

        public static string CompleteUrlWithMethod(this HttpContext context)
        {
            return $"{context.Request.Method} {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
        }

        public static bool IsAnonymous(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identities.Count() == 0
                || claimsPrincipal.Identities.Any(i => !i.IsAuthenticated);
        }
    }
}
