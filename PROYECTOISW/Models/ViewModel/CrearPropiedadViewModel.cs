using System.ComponentModel.DataAnnotations;

namespace PROYECTOISW.Models.ViewModel
{
    public class CrearPropiedadViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Titulo")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Tipo de propiedad")]
        public string? TipoPropiedad { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Precio de renta")]
        public int PrecioRenta { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Superficie")]
        public string? Superficie { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Número de habitaciones")]
        public string? NumeroHabitaciones { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Número de baños")]
        public string? NumeroBaños { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Servicios")]
        public string? Servicios { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Distancia")]
        public int Distancia { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Condiciones Especiales")]
        public string? CondicionesEspeciales { get; set; }

    }
}
