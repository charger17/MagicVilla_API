using AutoMapper;
using MagicVilla_Utilites;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> villaList = new();

            var response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.Seesiontoken));

            if (response != null && response.IsExitoso)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado));
            }

            return View(villaList);
        }

        //get
        public async Task<IActionResult> CrearVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearVilla(VillaCreateDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.Crear<APIResponse>(modelo, HttpContext.Session.GetString(DS.Seesiontoken));
                if (response is not null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Villa Creada Exitosamente";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarVilla(int id)
        {
            var response = await _villaService.Obtener<APIResponse>(id, HttpContext.Session.GetString(DS.Seesiontoken));

            if (response != null && response.IsExitoso)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Resultado));

                return View(_mapper.Map<VillaUpdateDto>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarVilla(VillaUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.Actualziar<APIResponse>(dto, HttpContext.Session.GetString(DS.Seesiontoken));

                if (response is not null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Villa Actualizada Exitosamente";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> RemoverVilla(int id)
        {
            var response = await _villaService.Obtener<APIResponse>(id, HttpContext.Session.GetString(DS.Seesiontoken));

            if (response != null && response.IsExitoso)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Resultado));
                
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverVilla(VillaDto dto)
        {
            var response = await _villaService.Remover<APIResponse>(dto.Id, HttpContext.Session.GetString(DS.Seesiontoken));

            if (response is not null && response.IsExitoso)
            {
                TempData["exitoso"] = "Villa Eliminada Exitosamente";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Algo Paso!";
            return View(dto);
        }
    }
}
