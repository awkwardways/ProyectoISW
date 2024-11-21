using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModel;
using PROYECTOISW.Servicios;
using System.IO;
//Cookies
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net;

namespace PROYECTOISW.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ProyectoiswContext _contexto;
        private readonly IServicioCorreo _servicioCorreo;

        public UsuarioController(ProyectoiswContext contexto, IServicioCorreo servicioCorreo)
        {
            _contexto = contexto;
            _servicioCorreo = servicioCorreo;
        }
        public IActionResult Index()
        {
            //Muestra las opciones Registrarse o iniciar sesión
            return View();
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
            string correoAlumno = "@Alumno.ipn.mx";
            if (correoAlumno == "")
            {
                nuevo.Tipo = "A";
            }
            else
                nuevo.Tipo = "P";
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
                return RedirectToAction("Index", "Home"); // Redirigir después de guardar
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
            if (ModelState.IsValid)
            {
                var usuario = await (from u in _contexto.Usuarios
                                     where u.CorreoElectronico == entrar.Correo && u.Contraseña == Cifrado.GetSHA256(entrar.Contraseña)
                                     select u).FirstOrDefaultAsync();
                if (usuario == null)
                {
                    ViewBag.Invalido = "Usuario no encontrado";
                    return View(entrar);
                }
                //Agrega el usuario a cookie
                var claims = new List<Claim>
                {
                    new Claim("Id_Usuario", usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                    new Claim(ClaimTypes.Email, usuario.CorreoElectronico),
                    new Claim(ClaimTypes.MobilePhone, usuario.Telefono),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View(entrar);
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
            //Cifra el token
            string token = GenerarToken();

            if (ModelState.IsValid)
            {
                var encontrado = _servicioCorreo.BuscarCorreo(recuperar.Correo);

                if (encontrado == null)
                {
                    ViewBag.Invalido = "Este correo no esta asociado a una cuenta";
                    return View(recuperar);
                }
                //Genera toquen
                _servicioCorreo.GuardarToken(Cifrado.GetSHA256(token), encontrado);
                //Mandar alerta de nuevo token
                _servicioCorreo.EnviarCorreo(encontrado, token);
                return View("ValidarToken");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ValidarToken(string token, string correo)
        {
            var model = new CodigoViewModel { Token = token, Correo = correo };
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> ValidarToken(CodigoViewModel codigo)
        { 
            if (ModelState.IsValid)
            {
                //Busca el token con un correo y tokens validos 
                if ( await _servicioCorreo.ValidarCon(codigo.Correo, Cifrado.GetSHA256(codigo.Token)) == false)
                {
                    ViewBag.Invalido = "Codigo no valido.";
                    return View(codigo);
                }
                ViewBag.Contraseñas = true;
                return RedirectToActionPermanent("NuevaCon", new {correo = codigo.Correo });
            }
            return View();
        }
        #endregion
        #region Nueva Contraseña
        [HttpGet]
        public IActionResult NuevaCon(string? correo)
        {
            var model = new NuevaConViewModel { Email = correo};
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> NuevaCon(NuevaConViewModel crear)
        {
            if (ModelState.IsValid)
            {
                //Verificar que las contraseñas sean iguales
                //si no son iguales regresa a la vista con un error
                if (crear.Nueva != crear.Confirmar)
                {
                    ViewBag.Incorrecto = "Las contraseñas no coinciden";
                    return View(crear);
                }
                //Restablecer contraseña
                await _servicioCorreo.ActualizarCon(Cifrado.GetSHA256(crear.Nueva), crear.Email);
                ViewBag.Contrase = "Contraseña restablecida con exito";
                return View();
            }
            return View();
        }
        #endregion
        public string GenerarToken()
        {
            Random random = new Random();
            int token = random.Next(1000,9000);
            return token.ToString();
        }
    }
}
