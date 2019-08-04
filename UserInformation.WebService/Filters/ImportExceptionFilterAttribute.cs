using System;
using System.Data.Entity.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace UserInformation.WebService.Filters
{
    public class ImportExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static  HttpActionExecutedContext Context { get; set; }
        public override void OnException(HttpActionExecutedContext context)
        {
            Context = context;

            if (context.Exception is UpdateException)
            {
                context.Response = GetHttpResponseException(HttpStatusCode.NotModified, "Item is not Updated");
            }

            if (context.Exception is InvalidOperationException)
            {
                context.Response = GetHttpResponseException(HttpStatusCode.Conflict, "Item is not Inserted");
            }
        }

        private HttpResponseMessage GetHttpResponseException(HttpStatusCode code, string messageText)
        {
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(messageText),
                
                ReasonPhrase = messageText
            };
        }
    }
}