using GestionDeFinanzasPersonales.Models.Security;
using GestionLigasDeportivas.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestionLigasDeportivas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly LigaDeportivaContext _context;

        public UsuarioController(LigaDeportivaContext context)
        {
            _context = context;
        }

        //LOGIN
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string? returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Correo);

                if (user != null && PasswordHasher.VerificarClave(model.Contrasena, user.Contrasena))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Nombre),
                        new Claim(ClaimTypes.Email, user.Correo),
                        new Claim("Id", user.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Role, user.TipoUsuario)


                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = false }
                        );

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction(nameof(HomeController.Index), "Home");

                }

            }
           

               // ModelState.AddModelError(string.Empty, "Contraseña incorrecta");
                ViewBag.ErrorMessage = "Correo o contraseña incorrectos";

            return View(model);
        }


        // GET: Usuario/Registro
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Registro
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Usuarios.Any(u=>u.Correo==model.Correo))
                {
                    return View(model);
                }

                var nuevoUsuario = new Usuario
                {
                    Nombre=model.Nombre,
                    Correo= model.Correo,
                    Contrasena= PasswordHasher.HashClave(model.Contrasena),
                    TipoUsuario=model.TipoUsuario
                };

                _context.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                TempData["RegistroExitoso"] = "¡Registro exitoso! Por favor inicie sesión.";
                return RedirectToAction(nameof(Login));


            }
            return View(model);
        }

        // Recuperar contraseña
        public IActionResult RecuperarContrasena()
        {
            return View(new RecuperarContrasena());
        }

        // POST: 
        // Recuperar contraseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecuperarContrasena(RecuperarContrasena model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Correo && u.Nombre==model.Nombre);


                if (user != null)
                {
                    HttpContext.Session.SetInt32("RecoveryUserId", user.UsuarioId);
                    return RedirectToAction("NuevaContrasena");

                }
                
            }
            ModelState.AddModelError("Error", "Los datos no coinciden o no existen para recuperar la contraseña");
            return View(model);
        }

        //CAMBIAR CONTRASEÑA
        public IActionResult NuevaContrasena() 
        {
            var usuarioId = HttpContext.Session.GetInt32("RecoveryUserId");

            if (usuarioId == null)
            {
                return RedirectToAction(nameof(RecuperarContrasena)); 
            }

            return View(new NuevaContrasenaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NuevaContrasena(NuevaContrasenaViewModel model) {

            if (ModelState.IsValid)
            {

                var userId= HttpContext.Session.GetInt32("RecoveryUserId");
                var usuario = _context.Usuarios.Find(userId);

                if (usuario != null) 
                { 
                usuario.Contrasena= PasswordHasher.HashClave(model.NuevaContrasena);
                    _context.SaveChanges();

                    HttpContext.Session.Remove("RecoveryUserId");
                    TempData["ClaveActualizada"] = "Contraseña actualizada correctamente";
                    return RedirectToAction(nameof(Login));
                }
            }

        return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
