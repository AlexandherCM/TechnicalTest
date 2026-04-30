
using System;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class PersonaViewModel
    {
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public int? ID { get; set; }  

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public string UserID { get; set; } = string.Empty;

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede superar los 200 caracteres.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El hobby es obligatorio.")]
        [StringLength(100, ErrorMessage = "El hobby no puede superar los 100 caracteres.")]
        [Display(Name = "Hobby")]
        public string Hobby { get; set; }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public string FechaNacimientoStr { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres.")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }
            
        public string Preposiciones { get; set; } = string.Empty;
    }
}
