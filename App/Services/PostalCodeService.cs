using App.Interfaces;
using Domain;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Services
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly IHttp _http;
        private readonly ISettings _settings;
        private readonly ISerialize _serialize;
                
        public PostalCodeService(IHttp http, ISettings settings, ISerialize serialize   )
        {
            _http = http;
            _settings = settings;
            _serialize = serialize;
        }

        public async Task<PostalCodeXmlResponse> GetPostalCodeInfo(string postalCode)
        {
            string thonaApiUrl = _settings.GetConfigValue("ThonaApiUrl");
            thonaApiUrl += postalCode;
            
            var contentXml = await _http.GetAsync(thonaApiUrl, httpMethod: HttpContentType.Xml);
            return _serialize.DeserializeFullXML<PostalCodeXmlResponse>(contentXml);
        }       
    }
}
