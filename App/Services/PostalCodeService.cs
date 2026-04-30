using App.Interfaces;
using App.Presenters;
using Domain;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Services
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly IHttp _http;
        private readonly ISettings _settings;
        private readonly ISerialize _serialize;

        public PostalCodeService(IHttp http, ISettings settings, ISerialize serialize)
        {
            _http = http;
            _settings = settings;
            _serialize = serialize;
        }

        public async Task<ResponseWebApi<InfoEstado>> GetPostalCodeInfo(string postalCode)
        {
            try
            {
                string thonaApiUrl = _settings.GetConfigValue("ThonaApiUrl");
                thonaApiUrl += postalCode;

                var contentXml = await _http.GetAsync(thonaApiUrl, httpMethod: HttpContentType.Xml);
                var infoEstado = _serialize.DeserializeFullXML<PostalCodeXmlResponseEntity>(contentXml);

                return new ResponseWebApi<InfoEstado>
                {
                    status = "success",
                    message = "Información del código postal obtenida correctamente.",
                    messageDetail = "La información del código postal se ha procesado correctamente.",
                    Data = MapperEstado(infoEstado)
                };
            }
            catch
            {
                return new ResponseWebApi<InfoEstado>
                {
                    status = "error",
                    message = "Error al obtener la información del código postal.",
                    messageDetail = "Ocurrió un error al procesar la solicitud. Por favor, inténtelo de nuevo más tarde.",
                    Data = null
                };
            }
        }

        public InfoEstado MapperEstado(PostalCodeXmlResponseEntity response)  
           => new InfoEstado
           {
               Nombre = response.DescripcionEstado,
               Codigo = response.CodigoEstado,
               Colonias = response.Colonias.Select(c => new InfoColonia
               {
                   Nombre = c.DescripcionColonia,
                   Codigo = c.CodigoColonia
               }).ToList()
           };

    }
}
