using Microsoft.AspNetCore.Mvc;

namespace _3abarni_backend.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next) {  _next = next; }
        public async Task InvokeAsync(HttpContext context)
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
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = ex.Message,

            };
            context.Response.StatusCode=StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);

        }
    }
}
