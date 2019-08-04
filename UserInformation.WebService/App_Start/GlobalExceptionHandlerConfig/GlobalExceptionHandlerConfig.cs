using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;

namespace UserInformation.WebService.App_Start
{
    public class GlobalExceptionHandlerConfig : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ArgumentNullException)
            {
                var result = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    RequestMessage = context.Request,
                    Content = new StringContent(context.ExceptionContext.Exception.Message),
                    ReasonPhrase = "ParametrNullException"
                };

                context.Result = new ArgumentNullResult(context.Request, result);
            }
        }
    }
}