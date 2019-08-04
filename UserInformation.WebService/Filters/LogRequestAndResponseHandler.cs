using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Serilog;

namespace UserInformation.WebService.Filters
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                Log.Logger.Information("POST-User-WEB-Service: Request {@request}", requestBody == ""? null : requestBody);
                var result = await base.SendAsync(request, cancellationToken);

                if (result.Content != null)
                {
                    var responseBody = await result.Content.ReadAsStringAsync();
                }

                Log.Logger.Information("POST-User-WEB-Service: Responce {@Responce} for Request {Request} ReasonPhrase {@ReasonPhrase} ", result.StatusCode, requestBody, result.ReasonPhrase);

            return result;
            }
    }
}
