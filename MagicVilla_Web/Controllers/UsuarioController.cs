﻿using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(LoginRequestDto modelo)
        {
            return View();
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

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
