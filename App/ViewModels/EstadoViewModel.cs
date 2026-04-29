using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModels
{
    public class EstadoViewModel
    {
        public string NombreEstado { get; set; }    
        public string Codigo { get; set; }    
        public List<ColoniasViewModel> Colonias { get; set; }   
    }
}
