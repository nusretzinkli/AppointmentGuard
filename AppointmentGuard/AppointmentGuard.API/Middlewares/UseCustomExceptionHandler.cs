using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace AppointmentGuard.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionFeature != null)
                    {
                        var statusCode = 500;
                        var errorMessage = exceptionFeature.Error.Message;

                        context.Response.StatusCode = statusCode;

                        var response = new
                        {
                            StatusCode = statusCode,
                            Error = errorMessage,
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}