using Microsoft.AspNetCore.Mvc;
using PROYECTOISW.Models;
using System.Diagnostics;

//Using agregado
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PROYECTOISW.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index(Usuario imagen)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var id = claimsIdentity?.FindFirst("Tipo")?.Value;
            var foto = claimsIdentity?.FindFirst("UserPhoto")?.Value;
            ViewBag.UserPhoto = $"data:image/jpeg;base64,{foto}";
            var usuario = new Usuario();
            usuario.Tipo = id;
            ViewData["Usuario"] = usuario;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
