﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MCQPuzzleGame.Exception
{

    public class LogAttribute :IActionFilter
    {
        public LogAttribute() { }
        public bool AllowMultiple => throw new NotImplementedException();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result;
            Trace.Write(string.Format("Action Method {0} Exceuted at {1}",
                context.ActionDescriptor.DisplayName, DateTime.Now, result));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            Trace.Write(string.Format("Action Method {0} Executeting at {1}",
                context.ActionDescriptor.DisplayName, DateTime.Now, context.Result));
        }

    }
}
