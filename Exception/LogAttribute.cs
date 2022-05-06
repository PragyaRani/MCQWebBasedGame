using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MCQPuzzleGame.Exception
{

    public class LogAttribute : Attribute,IActionFilter
    {
        public LogAttribute() { }
        public bool AllowMultiple => throw new NotImplementedException();

        //public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        //{
        //    Trace.Write(string.Format("Action Method {0} Executeting at {1}", 
        //        actionContext.ActionDescriptor.ActionName,DateTime.Now));
        //    var result = continuation();
        //    result.Wait();
        //    Trace.Write(string.Format("Action Method {0} Exceuted at {1}",
        //        actionContext.ActionDescriptor.ActionName, DateTime.Now));
        //    return result;
        //}

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

        //public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        //{
        //    Trace.WriteLine("No suffieent roles");

        //    return Task.Run(() => actionContext.Request.CreateErrorResponse(HttpStatusCode.UnavailableForLegalReasons,
        //        "Unfortunately you are not allowd to access request resource"));
        //    throw new NotImplementedException();
        //}

    }
}
