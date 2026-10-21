using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Naloga.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            context.Result = new ObjectResult(new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Details = context.Exception.Message
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
