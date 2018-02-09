using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace webSimpleApp.Middleware
{



    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
         public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke( HttpContext ctx)
        {
            await _next(ctx);

            if (ctx.Response.StatusCode == StatusCodes.Status200OK)
            {
                Debug.WriteLine("Page OK");

            }
            else
            {
                Debug.WriteLine($"Status Code {ctx.Response.StatusCode}");

            }
        }
    }
}
