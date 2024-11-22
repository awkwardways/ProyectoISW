using Microsoft.AspNetCore.Mvc;

namespace PROYECTOISW.Models.ViewModel
{
    public class DetallesViewModel : Controller
    {
        public List<(byte[] img, string mimeType)> imagenesDePropiedad { get; set; }
        public Propiedade propiedadAMostrar { get; set; }
    }
}
