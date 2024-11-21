using System.ComponentModel.DataAnnotations;

namespace PROYECTOISW.Models.ViewModel
{
    public class NuevaConViewModel
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nueva Contraseña")]
        public string? Nueva { get; set; }
        [Required(ErrorMessage = "El campo es requirodo")]
        [Display(Name = "Confirmar Contraseña")]
        public string? Confirmar { get; set; }
        public string? Email { get; set; }
    }
}
