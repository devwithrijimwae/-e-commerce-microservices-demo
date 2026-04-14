using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using eCommerceSharedLibary.Logs;

namespace eCommerce.SharedLibary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //Deleclare Default variables
            string message = "sorry, internal srever error occurred.kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";
            try
            {
                await next(context);
                //chech if Response is too many request // 429 status code
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many request made.";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                    await ModifyHeader(context, title, message, statusCode);

                }

                //if response is unauthorized // 401 status code
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are not authorized to access.";
                    statusCode = (int)StatusCodes.Status401Unauthorized;
                    await ModifyHeader(context, title, message, statusCode);
                }
                // If Response is Forbidden // 403 status code
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Out of Access";
                    message = "Ypu are not allowed/required to access.";
                    statusCode = StatusCodes.Status403Forbidden;
                    await ModifyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                //log Original Exception /File,Debugger,Console
                LogException.LogExceptions(ex);
                
                //Check Exception is TimeOut
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "Out of time";
                    message = "Request timeout.... tyr again";
                    statusCode = StatusCodes.Status408RequestTimeout;

                }

                // If Exception is Caught. 
                // If none of the exceptions then do the default.
                await ModifyHeader(context, title, message, statusCode);

            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            //display scary-free message to client
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {

                Detail = message,
                Status = statusCode,
                Title = title

            }),CancellationToken.None);

        }
    }
}
