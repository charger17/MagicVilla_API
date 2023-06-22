using MagicVilla_Utilites;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _usuarioService.Login<APIResponse>(modelo);

                if (response is not null && response.IsExitoso)
                {
                    LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Resultado));

                    //Claims
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, loginResponse.Usuario.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, loginResponse.Usuario.Rol));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    //Session
                    HttpContext.Session.SetString(DS.Seesiontoken, loginResponse.Token);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("ErroMessage", response.ErrorMessages.FirstOrDefault());
            }

            return View(modelo);
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroRequestDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _usuarioService.Registrar<APIResponse>(modelo);

                if (response is not null && response.IsExitoso)
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(modelo);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(DS.Seesiontoken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
