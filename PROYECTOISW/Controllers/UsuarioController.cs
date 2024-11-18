using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModel;
using System.IO;

namespace PROYECTOISW.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ProyectoiswContext _contexto;

        public UsuarioController(ProyectoiswContext contexto, IWebHostEnvironment hostingEnvironment)
        {
            _contexto = contexto;
        }

        #region Crear
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuariosViewModel nuevo, IFormFile? Foto)
        {
            if (Foto != null && Foto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Foto.CopyToAsync(memoryStream);
                    nuevo.Foto = memoryStream.ToArray();
                }
            }

            // Verificar si las contraseñas coinciden antes de validar el modelo
            if (nuevo.RContraseña != nuevo.Contraseña)
            {
                ViewBag.Contrase = "Las contraseñas no coinciden.";
                return View(nuevo);
            }

            if (ModelState.IsValid)
            {
                var crear = new Usuario
                {
                    Tipo = "A",
                    NombreCompleto = nuevo.NombreCompleto,
                    CorreoElectronico = nuevo.CorreoElectronico,
                    Contraseña = nuevo.Contraseña,
                    Telefono = nuevo.Telefono,
                    Foto = nuevo.Foto
                };
                _contexto.Usuarios.Add(crear);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("Index","Home"); // Redirigir después de guardar
            }

            // Imprimir errores de validación
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(nuevo);
        }
        #endregion
        #region Iniciar
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(IniciarSesionViewModel entrar)
        {
            var usuario = await (from u in _contexto.Usuarios
                                 where u.CorreoElectronico == entrar.Correo && u.Contraseña == entrar.Contraseña
                                 select u).FirstOrDefaultAsync();
            if (usuario == null)
            {
                ViewBag.Invalido = "Usuario no encontrado";
                return View(entrar);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region Recuperar
        [HttpGet]
        public IActionResult RecuperarCon()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RecuperarCon(RecuperarConViewModel recuperar)
        {

            return View();
        }
        #endregion
    }
}
