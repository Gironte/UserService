using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using UserInformation.WebService.Models;
using UserInformation.WebService.Providers;


namespace UserInformation.WebService.App_Start
{            
    public class AutofacConfig
    {
        public static ContainerBuilder ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); 
            builder.RegisterType<UserWebApiRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<UserProvider>().As<IUserProvider>().WithParameter(
                    (p, c) => p.ParameterType == typeof(IUserRepository),
                    (p, c) => c.Resolve<IUserRepository>())
            .SingleInstance();

            return builder;
        }
    }
}
