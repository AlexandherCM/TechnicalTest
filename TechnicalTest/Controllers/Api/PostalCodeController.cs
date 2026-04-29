using System.Web.Http;

namespace TechnicalTest.Controllers.Api
{
    public class PostalCodeController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "55635";
        }   
    }
}
