using Microsoft.AspNetCore.Mvc;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModel;
using PROYECTOISW.Servicios;
using System.Diagnostics;

namespace PROYECTOISW.Controllers
{
    public class ContactoPropietarioController : Controller
    {
        public readonly ProyectoiswContext _contexto;
        public ContactoPropietarioController(ProyectoiswContext contexto) 
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            Debug.WriteLine("DIOS AYUDA");
            return View();
        }
    }
}
