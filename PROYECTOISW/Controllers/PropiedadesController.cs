using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModel;
using System.IO;

//Agregar using
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace PROYECTOISW.Controllers
{
    [Authorize]
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
            var model = new CrearPropiedadViewModel();
            return View(model);
        }
        //TODO: Validar que el usuario haya iniciado sesion y sea propietario.
        //TODO2: Validar la entrada de datos.
        [HttpPost]
        public async Task<IActionResult> CrearPropiedad(CrearPropiedadViewModel nuevo)
        {

            if (ModelState.IsValid)
            {

                //Deseralizar una cookie
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var  id = int.Parse(claimsIdentity.FindFirst("Id_Usuario")?.Value);
                    // System.Diagnostics.Debug.WriteLine("Se subieron fotos\n");
                    var crear = new Propiedade
                    {
                        Estado = "H",
                        IdUsuario = id,
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
                        FechaPublicacion = DateTime.Now,
                    };
                    _contexto.Propiedades.Add(crear);
                    await _contexto.SaveChangesAsync();

                    if(nuevo.archivosImagenes != null & nuevo.archivosImagenes.Count > 0) 
                    {
                        foreach (var foto in nuevo.archivosImagenes) 
                        {
                            if (foto.Length > 0) 
                            {
                                using (var memoryStream = new MemoryStream()) 
                                {
                                    await foto.CopyToAsync(memoryStream);
                                    var imagen = new Imagene
                                    {
                                        IdPropiedad = crear.IdPropiedad,
                                        Imagen = memoryStream.ToArray()
                                    };
                                    _contexto.Imagenes.Add(imagen);
                                }
                            }
                        }
                        await _contexto.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
            }
            return View(nuevo);
        }
        #endregion
    }
}
