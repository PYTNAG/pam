using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PAM.Core.App.Common.Exceptions;

namespace PAM.WebApi.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                
                case ValidationException validationException:
                    code = HttpStatusCode.Forbidden;
                    break;
                
                case AlreadyExistsException alreadyExistsException:
                    code = HttpStatusCode.Conflict;
                    break;
                
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            
            string result = JsonSerializer.Serialize(new
            {
                errpr = exception.Message
            });

            return context.Response.WriteAsync(result);
        }
    }
}