using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Filters
{
    public class MyExceptionFilterAttribute:ExceptionFilterAttribute
    {
        private ILogger _log = null;
        public MyExceptionFilterAttribute(ILoggerFactory logger)
        {
            _log = logger.CreateLogger("MyExceptionLogger"); 
        }

        public override void OnException(ExceptionContext context)
        {
            _log.LogError($"Problem: {context.Exception.Message}");
            base.OnException(context);
        }
    }
}
