using AutoMapper;
using MagicVilla_Utilites;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModels;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace MagicVilla_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public NumeroVillaController(INumeroVillaService numeroVillaService, IMapper mapper, IVillaService villaService)
        {
            _numeroVillaService = numeroVillaService;
            _mapper = mapper;
            _villaService = villaService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexNumerovilla()
        {
            List<NumeroVillaDto> numeroVillaList = new();

            var response = await _numeroVillaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));
            }

            return View(numeroVillaList);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CrearNumeroVilla()
        {
            NumeroVillaViewModel numeroVillaVM = new();

            var response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
            }

            return View(numeroVillaVM);
        }

        [HttpPost]
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Crear<APIResponse>(modelo.NumeroVilla, HttpContext.Session.GetString(DS.Seesiontoken));

                if (response is not null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Numero de Villa Almacenado Exitosamente";
                    return RedirectToAction(nameof(IndexNumerovilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (res is not null && res.IsExitoso)
            {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado)).Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
            }

            return View(modelo);
        }


        [HttpGet]
        public async Task<IActionResult> ActualizarNumeroVilla(int villaNo)
        {
            NumeroVillaUpdateViewModel numeroVillaVM = new NumeroVillaUpdateViewModel();
            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo, HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = _mapper.Map<NumeroVillaUpdateDto>(modelo);
            }

            response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
                return View(numeroVillaVM);
            }

            return NotFound();  
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarNumeroVilla (NumeroVillaUpdateViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Actualizar<APIResponse>(modelo.NumeroVilla, HttpContext.Session.GetString(DS.Seesiontoken));

                if (response is not null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Numero de Villa Actualizado Exitosamente";

                    return RedirectToAction(nameof(IndexNumerovilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (res is not null && res.IsExitoso)
            {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado)).Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
            }

            return View(modelo);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoverNumeroVilla(int villaNo)
        {
            NumeroVillaDeleteViewModel numeroVillaVM = new NumeroVillaDeleteViewModel();
            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo, HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = modelo;
            }

            response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
                return View(numeroVillaVM);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RemoverNumeroVilla(NumeroVillaDeleteViewModel modelo)
        {
            var response = await _numeroVillaService.Remover<APIResponse>(modelo.NumeroVilla.VillaNo, HttpContext.Session.GetString(DS.Seesiontoken));

            if(response is not null && response.IsExitoso)
            {
                TempData["exitoso"] = "Numero de Villa Eliminado Exitosamente";

                return RedirectToAction(nameof(IndexNumerovilla));
            }
            TempData["error"] = "Algo sucedio";

            return View(modelo);
        }
    }
}
