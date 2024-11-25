using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROYECTOISW.Models.ViewModel
{
    public class BusquedaViewModel 
    {
        public int? MaxPrecio { get; set; }  //Filtro de precio maximo
        public int? MinPrecio { get; set; }  //Filtro de precio minimo
        public string? TipoInmueble { get; set; }   //filtro de tipo de propiedad
        public int? DistanciaAEscuela { get; set; }  
        public List<(byte[] rawImagen, PROYECTOISW.Models.Propiedade propiedad, string mimeType)> ListaDePropiedades { get; set; }  //Lista de propiedades a desplegar
        public List<SelectListItem> TiposDePropiedad { get; set; }
        public BusquedaViewModel() 
        {
            TiposDePropiedad = new List<SelectListItem>
            {
                new SelectListItem { Value = "Casa", Text = "Casa" },
                new SelectListItem { Value = "Departamento", Text = "Departamento" },
                new SelectListItem { Value = "Habitacion", Text = "Habitación"}
            };
        }
    }
}
