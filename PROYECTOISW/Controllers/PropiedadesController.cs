using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModel;
using System.IO;

namespace PROYECTOISW.Controllers
{
    public class PropiedadesController : Controller
    {
        private readonly ProyectoiswContext _contexto;

        public PropiedadesController(ProyectoiswContext contexto, IWebHostEnvironment hostingEnvironment)
        {
            _contexto = contexto;
        }

        #region Crear
        [HttpGet]
        public IActionResult CrearPropiedad()
        {
            return View();

        }
        //TODO: Validar que el usuario haya iniciado sesion y sea propietario.
        //TODO2: Validar la entrada de datos.
        [HttpPost]
        public async Task<IActionResult> CrearPropiedad(CrearPropiedadViewModel nuevo  ) 
        {
            if (ModelState.IsValid)
            {
                var crear = new Propiedad
                {
                    Estado = "D",
                    IdUsuario = 1,
                    Titulo = nuevo.Titulo,
                    Descripcion = nuevo.Descripcion,
                    TipoPropiedad = nuevo.TipoPropiedad,
                    PrecioRenta = nuevo.PrecioRenta,
                    Superficie = nuevo.Superficie,
                    NumeroHabitaciones = nuevo.NumeroHabitaciones,
                    NumeroBaños = nuevo.NumeroBaños,
                    Servicios = nuevo.Servicios,
                    Direccion = nuevo.Direccion,
                    Distancia = nuevo.Distancia,
                    CondicionesEspeciales = nuevo.CondicionesEspeciales,
                    FechaPublicacion = DateTime.Now
                };
                _contexto.Propiedades.Add(crear);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(nuevo);
        }
        #endregion
    }
}
