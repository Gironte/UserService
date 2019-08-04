using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using Serilog;

namespace UserInformation.WebService.Models
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            object request = null;
            actionContext.ActionArguments.TryGetValue("request", out request);

            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);

                var allErrors = actionContext.ModelState.Values.SelectMany(v => v.Errors);

                Log.Logger.Error("POST-User-WEB-Service: Errors {@request} , {errors}", request, allErrors.Select(x => x.ErrorMessage));
            }
        }
    }
}
