using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace ApacheKafkaApp.INFRA.LogFilter
{
    public class LogFilter : IActionFilter
    {
        private readonly ILogger _ilogger;


        public LogFilter(ILoggerFactory loggerFactory)
        {
            _ilogger = loggerFactory.CreateLogger("Log Filter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log("Existing", context.RouteData);
        }

        public void OnActionExecuting(ActionExecutingContext filtercontext)
        {
            Log("Entering", filtercontext.RouteData, filtercontext.ActionArguments);
        }

        private void Log(string method,RouteData routedata,IDictionary<String , object> arguments = null)
        {
            var controllerName = routedata.Values["controller"];
            var actionName = routedata.Values["actions"];
            StringBuilder message = new StringBuilder();
            message.Append(String.Format($"{method} : Controller : {controllerName}; action:{actionName};"));

            if(arguments != null && arguments.Count > 0)
            {
                message.Append("Arguments = ");
                foreach(var keyvalue in arguments)
                {
                    if(keyvalue.Key != null && keyvalue.Value != null)
                    {
                        message.Append($"Key : {keyvalue.Key} : Value : {keyvalue.Value.ToString()};");
                    }
                }
            }
        }
    }
}

