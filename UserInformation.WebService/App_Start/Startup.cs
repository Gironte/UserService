using Owin;
using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Newtonsoft.Json;
using Serilog;
using UserInformation.WebService.Filters;
using UserInformation.WebService.Models;

namespace UserInformation.WebService.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var autofacResolver = AutofacConfig.ConfigureContainer().Build();
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidateModelAttribute());

            config.MessageHandlers.Add(new LogRequestAndResponseHandler());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new AccountRequestConverterConfig()); 
            config.DependencyResolver = new AutofacWebApiDependencyResolver(autofacResolver);
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLoggerConfig());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandlerConfig());
            
            appBuilder.UseWebApi(config);
        }
    }
}
