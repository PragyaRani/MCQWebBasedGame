using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MCQPuzzleGame.Exception
{
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        public CustomAuthorization() { }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status451UnavailableForLegalReasons);
            //context.Result = (ObjectResult)"Not Allowed to access resource"
            //((ObjectResult)result).Value = "Sorry user is not Authorized";
        }
    }
}
