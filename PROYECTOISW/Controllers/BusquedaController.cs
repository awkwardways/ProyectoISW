using Microsoft.AspNetCore.Mvc;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModel;

namespace PROYECTOISW.Controllers
{
    public class BusquedaController : Controller
    {
        private readonly ProyectoiswContext _context; 
        public BusquedaController(ProyectoiswContext context) 
        {
            _context = context;
        }
        public IActionResult Busqueda()
        {
            var propiedades = _context.Propiedades.ToList();
            return View(propiedades);
        }
    }
}
