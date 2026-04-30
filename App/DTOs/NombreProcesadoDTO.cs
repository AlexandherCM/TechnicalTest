using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTOs
{
    public class NombreProcesadoDTO
    {
        public string NombreCompletoNormalizado { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public List<string> PreposicionesEncontradas { get; set; }
        public string UserID { get; set; }
    }
}
