using Microsoft.AspNetCore.Mvc;
using PROYECTOISW.Models;
using PROYECTOISW.Helper;
using PROYECTOISW.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PROYECTOISW.Controllers
{
    public class BusquedaController : Controller
    {
        private readonly ProyectoiswContext _contexto; 
        public BusquedaController(ProyectoiswContext context) 
        {
            _contexto = context;
        }
        [HttpGet]
        public async Task<IActionResult> Busqueda()
        {
            var model = new BusquedaViewModel
            {
                TiposDePropiedad = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Casa", Text = "Casa" },
                    new SelectListItem { Value = "Departamento", Text = "Departamento" },
                    new SelectListItem { Value = "Habitacion", Text = "Habitación"}
                },
                ListaDePropiedades = new List<(byte[] rawImagen, Propiedade propiedad, string mimeType)>()
            };
            //BusquedaViewModel viewModel = new BusquedaViewModel();
            //List<(byte[] rawImagen, Propiedade propiedad, string TipoImagen)> cardsPropiedades = new List<(byte[] rawImagen, Propiedade propiedad, string TipoImagen)>();
            var propiedades = await _contexto.Propiedades.ToListAsync();  // Recuperar todas las propiedades disponibles en la base de datos.
            foreach (var currPropiedad in propiedades) 
            {
                var primerImagenPropiedad = await _contexto.Imagenes.FirstOrDefaultAsync(f => f.IdPropiedad == currPropiedad.IdPropiedad);
                var mimeType = MimeTypeHelper.GetMimeType(primerImagenPropiedad.Imagen);
                model.ListaDePropiedades.Add((primerImagenPropiedad.Imagen, currPropiedad, mimeType));
            }
            //cardsPropiedades.
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Busqueda(BusquedaViewModel model) 
        {
            var propiedades = _contexto.Propiedades.AsQueryable();  //Recuperar todas las propiedades de base de datos
            if (model.MinPrecio != null)
            {
                propiedades = propiedades.Where(p => p.PrecioRenta >= model.MinPrecio.Value);
            }
            if (model.MaxPrecio != null)
            {
                propiedades = propiedades.Where(p => p.PrecioRenta >= model.MaxPrecio.Value);
            }
            if (model.TipoInmueble != null) 
            {
                propiedades = propiedades.Where(p => p.TipoPropiedad == model.TipoInmueble);
            }
            if (model.DistanciaAEscuela != null) 
            {
                propiedades = propiedades.Where(p => p.Distancia <= model.DistanciaAEscuela);
            }
            var propiedadesList = await propiedades.ToListAsync();  
            foreach (var currPropiedad in propiedades) 
            {
                var primerImagenPropiedad = await _contexto.Imagenes.FirstOrDefaultAsync(f => f.IdPropiedad == currPropiedad.IdPropiedad);
                var mimeType = MimeTypeHelper.GetMimeType(primerImagenPropiedad.Imagen);
                model.ListaDePropiedades.Add((primerImagenPropiedad.Imagen, currPropiedad, mimeType));
            }
            return View(model);
        }
        public async Task<IActionResult> Detalles(int id) 
        {
            List<(byte[] rawImagen, string mimeType)> imagenes = new List<(byte[] rawImagen, string mimeType)>();
            var propiedad = await _contexto.Propiedades.FirstOrDefaultAsync(p => p.IdPropiedad == id);
            var fotosPropiedad = await _contexto.Imagenes.Where(f => f.IdPropiedad == id).ToListAsync();
            if (propiedad == null)
            {
                return View("Error");
            }

            foreach (var foto in fotosPropiedad) 
            {
                var mimeType = MimeTypeHelper.GetMimeType(foto.Imagen);
                imagenes.Add((foto.Imagen, mimeType));
            }
            DetallesViewModel model = new DetallesViewModel();
            model.imagenesDePropiedad = imagenes;
            model.propiedadAMostrar = propiedad;
            return View(model);
        }
    }
}
