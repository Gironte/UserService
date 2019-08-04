using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Serilog;

using System.ServiceModel;
using System.ServiceModel.Description;
using UserInformation.WCFService.Providers;
using UserInformation.WebService.App_Start;
using UserInformation.WebService.Models;

namespace UserInformation.SelfHost
{
    class Program
    {
        //прошу учесть при проверке задания
        //на моем пк по какой-то причине вэб-сервис запускался
        //только если запустить студию или .exe файл приложения от имени администартора
        static void Main(string[] args)
        {
            var baseAddress = ConfigurationManager.AppSettings["BaseAddress"];
            var logFile = ConfigurationManager.AppSettings["LogFile"];
            var webServicePort = int.TryParse(ConfigurationManager.AppSettings["WebServicePort"], out int port) ? port : 900;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(x => x.File(logFile))
                .WriteTo.Async(x => x.Console())
                .CreateLogger();

            Console.OutputEncoding = Encoding.UTF8;

            var tasks = new List<Task>
            {
                Task.Factory.StartNew(() =>
                {
                    using (WebApp.Start<Startup>(new StartOptions {AppStartup = baseAddress, Port = webServicePort}))
                    {
                        Log.Logger.Information("POST-User-WEB-Service is Running. Press any key to exit");
                        Console.ReadKey();
                    }
                }),
                Task.Factory.StartNew(() =>
                {
                    using (var host = new ServiceHost(typeof(UserInfoProvider), new Uri(baseAddress)))
                    {
                        var smb = new ServiceMetadataBehavior
                        {
                            HttpGetEnabled = true,
                            MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                        };
                        host.Description.Behaviors.Add(smb);

                        host.Open();

                        Log.Logger.Information("GET-User-SOAP-Service is Running. Press any key to exit");

                        Console.ReadKey();
                        host.Close();
                    }
                })
            };

            Task.WaitAll(tasks.ToArray());
        }
    }
}
