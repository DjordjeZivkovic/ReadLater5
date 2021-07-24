using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ReadLater5.Domain.Dtos;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadLater5.Presentation.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,  Exception ex)
        {
            object error = null;

            switch (ex)
            {
                case RestException re:
                    _logger.LogError(ex, "REST ERROR");
                    error = re.Error;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    _logger.LogError(ex, "SERVER ERROR");
                    error = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (error == null)
                return;
                 
            var result = JsonSerializer.Serialize(new { error });

            await context.Response.WriteAsync(result);
        }
    }
}
