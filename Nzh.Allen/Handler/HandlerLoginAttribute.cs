using Microsoft.AspNetCore.Mvc.Filters;
using Nzh.Allen.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Handler
{
    public class HandlerLoginAttribute : Attribute, IActionFilter
    {
        public bool Ignore = true;

        public HandlerLoginAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (Ignore == false)
            {
                return;
            }
            if (new OperatorProvider(context.HttpContext).GetCurrent() == null)
            {
                context.HttpContext.Response.Redirect("/Login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if (context.Result?.GetType() == typeof(JsonResult))
            //{
            //    JsonResult result = context.Result as JsonResult;
            //    JObject valueObject = (JObject)(result.Value);
            //    valueObject["xb"] = "xxxx";
            //    result.Value = valueObject;
            //    context.Result = result;
            //    string yy = "";
            //}
        }
    }
}
