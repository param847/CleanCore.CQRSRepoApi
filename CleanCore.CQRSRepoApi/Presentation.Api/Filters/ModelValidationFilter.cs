using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Models.ResponseModel;

namespace Presentation.Api.Filters
{
    /// <summary>
    /// Ensures any invalid ModelState results in a 400 with our ResponseData envelope.
    /// </summary>
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Aggregate all errors into a single message
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                var result = new ResponseData<object>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = string.Join(" | ", errors),
                    Data = null!
                };

                context.Result = new BadRequestObjectResult(result);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}