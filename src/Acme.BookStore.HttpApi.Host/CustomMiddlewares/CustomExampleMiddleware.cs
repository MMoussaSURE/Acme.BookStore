using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace Acme.BookStore.CustomMiddlewares
{
    public class CustomExampleMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Logic before the next middleware
           

            // Call the next middleware in the pipeline
            await _next(context);

            // Logic after the next middleware
           
        }
    }
}
