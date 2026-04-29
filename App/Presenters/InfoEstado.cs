using System;
using System.Collections.Generic;
using System.Text;

namespace App.Presenters
{
    public class InfoEstado
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }  
        public List<InfoColonia> Colonias { get; set; } 
    }
}
