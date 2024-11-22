using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROYECTOISW.Models.ViewModel
{
    public class CrearUsuariosViewModel
    {
        public string? Tipo { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(40,ErrorMessage ="El limite son 40 letras")]
        [Display(Name = "Nombre Completo")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no valido")]
        [Display(Name = "Correo Electrónico")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Contraseña")]
        public string? Contraseña { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Confirmar Contraseña")]
        public string? RContraseña { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(10,ErrorMessage ="El numero de contener 10 digitos")]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [Display(Name = "Imagen")]
        public byte[]? Foto { get; set; }
    }
}
