using System.ComponentModel.DataAnnotations;

namespace PROYECTOISW.Models.ViewModel
{
    public class CodigoViewModel
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Ingresa el código")]
        public string? Token { get; set; }
        [Required]
        [EmailAddress]
        public string? Correo { get; set; }
    }
}
