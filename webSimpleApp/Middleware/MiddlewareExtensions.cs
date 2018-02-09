using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webSimpleApp.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseMyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyMiddleware>();
        }
    }

}
