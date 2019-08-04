using System.Web.Http.ExceptionHandling;

namespace UserInformation.WebService.App_Start
{
    public class ExceptionLoggerConfig : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Serilog.Log.Logger.Error(context.Exception.Message);
        }
    }
}
