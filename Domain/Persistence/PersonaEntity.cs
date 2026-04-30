using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Persistence
{
    public class PersonaEntity
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Nombres { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Hobby { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
    }
}
