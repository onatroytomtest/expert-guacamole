using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Faircode.Platform;
using Faircode.Platform.Api.Framework.Filters;


namespace Faircode.Platform.Api.Framework.Controllers
{
    [GlobalExceptionFilter]
    public abstract class BaseController : Controller
    {
        protected string ControllerName
        {
            get
            {
                return GetType().Name;
            }
        }
        protected BaseController()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
