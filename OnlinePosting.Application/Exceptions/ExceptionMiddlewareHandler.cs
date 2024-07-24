using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace OnlinePosting.Application.Exceptions
{
    public class ExceptionMiddlewareHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionMiddlewareHandler> _logger;

        public ExceptionMiddlewareHandler(ILogger<ExceptionMiddlewareHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Exception occurred: {exception.Message}");

            var probDetail = new ProblemDetails()
            {
                Detail = "something happend",
                Title = "Server Error",
                Type = "System error contact admin"
            };

            var respons = JsonSerializer.Serialize(probDetail);
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(respons, cancellationToken);
            return true;
        }
    }
}
