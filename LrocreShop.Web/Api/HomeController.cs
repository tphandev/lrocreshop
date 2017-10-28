using LrocreShop.Service;
using LrocreShop.Web.Infrastructure.Core;
using System.Web.Http;

namespace LrocreShop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        public HomeController(IErrorService errorService) : base(errorService)
        {
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, Lrocre Member. ";
        }
    }
}