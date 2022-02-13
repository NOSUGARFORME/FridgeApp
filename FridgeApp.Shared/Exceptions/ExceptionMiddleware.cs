using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FridgeApp.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FridgeApp.Shared.Exceptions
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (FridgeException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.Headers.Add("content-type", "application/json");

                var errorCode = ToSnakeCase(ex.GetType().Name.Replace("Exception", string.Empty));
                var json = JsonSerializer.Serialize(new {ErrorCode = errorCode, ex.Message});
                await context.Response.WriteAsync(json);
            }
        }
        
        private static string ToSnakeCase(string input)
            => string.Concat(input.Select((x,i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()));
    }
}