using App.Interfaces;
using Domain;
using System.Threading.Tasks;
using System.Web.Http;

namespace TechnicalTest.Controllers.Api
{
    public class PostalCodeController : ApiController
    {
        private readonly IPostalCodeService _service; 
        public PostalCodeController(IPostalCodeService service)
            =>_service = service;

        [HttpGet]
        public async Task<PostalCodeXmlResponse> Get(string id)
        {   
            return await _service.GetPostalCodeInfo(id);
        }   
    }
}
