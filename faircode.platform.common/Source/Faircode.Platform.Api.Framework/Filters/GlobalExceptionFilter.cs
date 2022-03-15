using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Faircode.Platform.Api.Framework.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {

        public GlobalExceptionFilter()
        {

        }
        public override void OnException(ExceptionContext exceptionContext)
        {
            HandleException(exceptionContext);
            base.OnException(exceptionContext);
        }

        private void HandleException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            if (exception == null)
            {
                return;
            }
            string message = exception.Message;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }

            else if (exception is ArgumentNullException || exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            else if (exception is InvalidDataException)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            else if (exception is InvalidOperationException)
            {

                statusCode = HttpStatusCode.Forbidden;

            }

            exceptionContext.Result = new JsonResult($"{message} ")
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
