using Brugnner.API.Core.Resources;
using Brugnner.API.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace Brugnner.API.Filters
{
    /// <summary>
    /// A filter that validates if the model state of an <see cref="APIResource"/> is valid.
    /// </summary>
    public class ValidationFilter : IActionFilter
    {
        /// <summary>
        /// Validates if the model state is valid before the controlled method gets called.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is APIResource);

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new APIResponse(HttpStatusCode.BadRequest, error: new APIError(context.ModelState)));
                return;
            }
        }

        /// <summary>
        /// Ignored.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
