using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PROYECTOISW.Models.ViewModel
{
    public class CrearPropiedadViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(50,ErrorMessage ="La longitud máxima es de 50 letras")]
        [Display(Name = "Titulo")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(600, ErrorMessage ="La longitud máxima es 600 letras")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Tipo de propiedad")]
        public string? TipoPropiedad { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1,100000, ErrorMessage = "El precio es incorrecto")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
        [Display(Name = "Precio de renta")] 
        public int PrecioRenta { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1,9999, ErrorMessage = "El rango maximo es 9999")]
        [Display(Name = "Superficie")]
        public string? Superficie { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1, 9, ErrorMessage = "El máximo es 9")]
        [Display(Name = "Número de habitaciones")]
        public string? NumeroHabitaciones { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1,9, ErrorMessage = "El máximo es 9")]
        [Display(Name = "Número de baños")]
        public string? NumeroBaños { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima es 50 letras")]
        [Display(Name = "Servicios")]
        public string? Servicios { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(200, ErrorMessage = "La longitud máxima es 200 letras")]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1,999, ErrorMessage = "El máximo es 999 KM")]
        [Display(Name = "Distancia")]
        public int Distancia { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [MaxLength(200, ErrorMessage = "La longitud máxima es 200 letras")]
        [Display(Name = "Condiciones Especiales")]
        public string? CondicionesEspeciales { get; set; }

        public List<SelectListItem>? tiposDePropiedad { get; set; }
        public CrearPropiedadViewModel()
        {
            tiposDePropiedad = new List<SelectListItem>
            {
                new SelectListItem { Value = "Casa", Text = "Casa"},
                new SelectListItem { Value = "Departamento", Text = "Departamento" },
                new SelectListItem { Value = "Habitacion", Text = "Habitacion"}
            };
        }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "fotoPropiedad")]
        public List<IFormFile> archivosImagenes { get; set; }
    }
}
