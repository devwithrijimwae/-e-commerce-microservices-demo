using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.Middleware
{
    public class ListenToOnlyApiGetway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //Extract specific header from the request 
            var signedHeader = context.Request.Headers["Api-Gateway"];

            //Null means, the request is not coming from the Api Getway
            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry,Service is Unavailable");
                return;
            }
            else
            {
                await next (context);
            }

        }
    }
}
