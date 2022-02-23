using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FridgeApp.Shared.Abstractions.Exceptions;
using FridgeApp.Shared.Models;
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
            // TODO: Add handling other errors
            catch (BaseNotFoundException ex)
            {
                await HandleException(context, ex, HttpStatusCode.NotFound);
            }
            catch (FridgeException ex)
            {
                await HandleException(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, HttpStatusCode code)
        {
            var response = context.Response;
            var errorCode = ToSnakeCase(ex.GetType().Name.Replace("Exception", string.Empty));
            var message = ex.Message;

            response.ContentType = "application/json";
            response.StatusCode = (int) code;
            var json = JsonSerializer.Serialize(new BaseResponseModel
                {ErrorCode = errorCode, StatusCode = code, Message = message});
            await context.Response.WriteAsync(json);
        }

        private static string ToSnakeCase(string input)
            => string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()));
    }
}