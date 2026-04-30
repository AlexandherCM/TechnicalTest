using Newtonsoft.Json;

namespace App.ViewModels
{
    public class AlertaViewModel
    {
        public RequestViewModel badRequest = new RequestViewModel()
        {
            Titulo = "¡Error!",
            Leyenda = "Ocurrio un error interno al procesar la solicitud.",
            Estado = false
        };

        public RequestViewModel goodRequest = new RequestViewModel()
        {
            Titulo = "¡Éxito!",
            Leyenda = "Solicitud procesada correctamente.",
            Estado = true
        };
    }   

    public class RequestViewModel
    {
        [JsonProperty("Titulo")]
        public string Titulo { get; set; } = string.Empty;

        [JsonProperty("Leyenda")]
        public string Leyenda { get; set; } = string.Empty;

        [JsonProperty("Estado")]
        public bool Estado { get; set; }
        
        [JsonProperty("Html")]
        public string Html { get; set; } = string.Empty;
    }
}
