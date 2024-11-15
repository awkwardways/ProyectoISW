using System.ComponentModel.DataAnnotations;

namespace PROYECTOISW.Models.ViewModel
{
    public class RecuperarConViewModel
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        [Display(Name = "Correo Electrónico")]
        public string? Correo { get; set; }

        public string? Token { get; set; }

    }
}
