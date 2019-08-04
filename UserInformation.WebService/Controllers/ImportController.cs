using System.Web.Http;
using Serilog;
using UserInformation.WebService.Filters;
using UserInformation.WebService.Models;
using UserInformation.WebService.Providers;

namespace UserInformation.WebService.Controllers
{
    public class ImportController : ApiController
    {
        private readonly IUserProvider _userProvider;

        public ImportController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpPost]
        [ImportExceptionFilter]
        public IHttpActionResult Import(MyAccountRequestBase request)
        {
            if (ModelState.IsValid)
            {
                _userProvider.ImportUser(request);
                return Ok(request);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
