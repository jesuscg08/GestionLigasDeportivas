using System.Diagnostics;
using System.Security.Claims;
using GestionLigasDeportivas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionLigasDeportivas.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Depuración: Mostrar roles del usuario
            ViewBag.Roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (User.IsInRole("Administrador"))
            {
                return RedirectToAction("Index", "Liga");
            }
            else if (User.IsInRole("Entrenador"))
            {
                return RedirectToAction("Index", "Equipo");

            } else if (User.IsInRole("Jugador")) {

                return RedirectToAction("Index", "Estadistica");
            }

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
