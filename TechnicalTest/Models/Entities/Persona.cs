using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTest.Models.Entities
{
    [Table("Personas")]
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string UserID { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(150)]
        public string ApellidoUno { get; set; }

        [Required]
        public string ApellidoDos { get; set; }

        [Required]
        [StringLength(150)]
        public string Hobby { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }   
    }
}