using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Filters
{
    public class MyActionFilterAttribute:ActionFilterAttribute
    {
        private bool noJSON = true;

        private ILogger _logger;
        public MyActionFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("*** <><><> ActionFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("********************************  OnActionExecuting  ************************************");
            if(noJSON==false) _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(context,Newtonsoft.Json.Formatting.Indented,new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All }).ToString());
            _logger.LogInformation("*************************** ************** **************************");

            if (context.HttpContext.Request.Query["boo"] == "casper")
            {
                context.Result = new ContentResult() { Content = "Exiting Pipeline Early : Ghosts in the machine?" };
            }
            if (context.HttpContext.Request.Query["primes"] == "true")
            {
                var controller = context.Controller as Controller;
                controller.ViewBag.Primes = new List<int> { 1, 2, 3, 5 };
            }
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("********************************  OnActionExecuted  ************************************");
            if(noJSON==false) _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(context, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All }).ToString());
            _logger.LogInformation("*************************** ************** **************************");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("******************************** OnResultExecuting   ************************************");
            if(noJSON==false) _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(context, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All }).ToString());
            _logger.LogInformation("*************************** ************** **************************");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("********************************  OnResultExecuted  ************************************");
            if(noJSON==false) _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(context, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All }).ToString());
            _logger.LogInformation("*************************** ************** **************************");
            base.OnResultExecuted(context);
        }
    }
}
