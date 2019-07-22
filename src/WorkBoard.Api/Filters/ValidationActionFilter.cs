using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WorkBoard.Api.Exceptions;

namespace WorkBoard.Api.Filters
{
    public class ValidationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = (Controller)context.Controller;
            if (controller.ModelState.IsValid)
            {
                await next();
                return;
            }

            throw new ApiException("Validation errors.", HttpStatusCode.BadRequest, ApiException.ValidationError, ToSimpleKeyValueObject(controller.ModelState));
        }

        private static Dictionary<string, ModelErrorCollection> ToSimpleKeyValueObject(ModelStateDictionary modelState)
        {
            var errorObject = new Dictionary<string, ModelErrorCollection>();
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count == 0) continue;
                errorObject.Add(key, modelState[key].Errors);
            }
            return errorObject;
        }
    }
}
