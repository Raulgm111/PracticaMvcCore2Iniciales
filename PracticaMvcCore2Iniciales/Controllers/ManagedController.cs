using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;
using System.Security.Claims;
using PracticaMvcCore2Iniciales.Filters;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class ManagedController : Controller
    {
        public RepositoryUsuarios repo;

        public ManagedController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email
            , string password)
        {
            Usuario usuario =
                await this.repo.ExisteUsuario(email, password);
            if (usuario != null)
            {
                ClaimsIdentity identity =
               new ClaimsIdentity
               (CookieAuthenticationDefaults.AuthenticationScheme
               , ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim
                    (new Claim(ClaimTypes.Name, usuario.Email));
                identity.AddClaim
                    (new Claim(ClaimTypes.NameIdentifier, usuario.Password.ToString()));
                identity.AddClaim
                    (new Claim("Nombre", usuario.Nombre));
                identity.AddClaim
                    (new Claim("Apellidos", usuario.Aepllidos));
                identity.AddClaim
                    (new Claim("Imagen", usuario.Imagen));
                identity.AddClaim
                        (new Claim("IdUsuario", usuario.IdUsuario.ToString()));
                ClaimsPrincipal user = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }

        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }
    }
}
