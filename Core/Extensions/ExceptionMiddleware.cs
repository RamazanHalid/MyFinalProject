using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IHostingEnvironment hostingEnvironment)
        {
            try
            {

                await _next(httpContext);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(httpContext, e, hostingEnvironment);
            }
        }
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e, IHostingEnvironment hostingEnvironment)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 400;

            IEnumerable<ValidationFailure> errors;
            if (e.GetType() == typeof(ValidationException))
            {
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    SituationCode = 2,
                    Message = e.Message,
                    Errors = errors
                }.ToString());
            }
           
            if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    Success = false,
                    SituationCode = 4,
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Unauthorized operation",
                }.ToString());
            }
            Log(httpContext, e, hostingEnvironment);
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                SituationCode = 1,
                Success = false,
                StatusCode = httpContext.Response.StatusCode,
                Message = e.Message
            }.ToString());
        }
        private void Log(HttpContext context, Exception exception, IHostingEnvironment hostingEnvironment)
        {
            var savePath = hostingEnvironment.WebRootPath;
            var now = DateTime.UtcNow;
            var fileName = $"myLogs.txt";
            var filePath = Path.Combine(savePath, "logs", fileName);

            using (var writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{now.ToString("HH:mm:ss")} {context.Request.Path}");
                writer.WriteLine(exception.Message + "\n" + exception.InnerException.Message);
            }
        }
    }
}
