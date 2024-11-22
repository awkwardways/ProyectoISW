using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PROYECTOISW.Models;
using PROYECTOISW.Models.ViewModels;
using PROYECTOISW.Servicios;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace PROYECTOISW.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ProyectoiswContext _contexto;
     
        public UsuarioController(ProyectoiswContext context) 
        {
            _contexto = context;
        }
        #region Crear
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> CrearUsuario(CrearUsuarioViewModel nuevo, IFormFile? Foto)
        {
            if (ModelState.IsValid)
            {
                //Verificar el tipo de usuario
                nuevo.Tipo = nuevo.Tipo == "estudiante" ? "A" : "P";

                //Verifiacar la foto
                if (Foto != null && Foto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Foto.CopyToAsync(memoryStream);
                        nuevo.Foto = memoryStream.ToArray();
                    }
                }
                else
                {
                    //Si la foto viene vacia le asginamos una foto en el proyecto
                    string rutaArchivo = Path.Combine("wwwroot", "Imagenes", "perfil.png");
                    using (var fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        nuevo.Foto = memoryStream.ToArray();
                    }
                }

                //Validar el correo en caso de ser alumno
                if (nuevo.Tipo == "A")
                {
                    string emailPattern = @"^[^@\s]+@alumno\.ipn\.mx$";
                    if (!Regex.IsMatch(nuevo.CorreoElectronico, emailPattern, RegexOptions.IgnoreCase))
                    {
                        ViewBag.EmailError = "El correo electrónico debe pertenecer al dominio alumno.ipn.mx si eres estudiante.";
                        return View(nuevo); // Regresar a la vista con el modelo para mostrar el error
                    }
                }

                //Guardar usuario
                var crear = new Usuario
                {
                    Tipo = nuevo.Tipo,
                    NombreCompleto = nuevo.NombreCompleto,
                    CorreoElectronico = nuevo.CorreoElectronico,
                    Contraseña =Cifrado.GetSHA256(nuevo.Contraseña),
                    Telefono = nuevo.Telefono,
                    Foto = nuevo.Foto
                };
                _contexto.Usuarios.Add(crear);
                await _contexto.SaveChangesAsync();

                //Agrega el usuario a cookie
                var claims = new List<Claim>
                {
                    new Claim("Id_Usuario", crear.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, crear.NombreCompleto),
                    new Claim(ClaimTypes.Email, crear.CorreoElectronico),
                    new Claim(ClaimTypes.MobilePhone, crear.Telefono)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home"); // Redirigir después de guardar
            }
            return View(nuevo);
        }
        #endregion

        #region Inicar sesion
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(PROYECTOISW.Models.ViewModel.IniciarSesionViewModel entrar)
        {
            if (ModelState.IsValid)
            {
                var usuario = await(from u in _contexto.Usuarios
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
                    new Claim(ClaimTypes.MobilePhone, usuario.Telefono)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View(entrar);
        }
        #endregion
    }
}
