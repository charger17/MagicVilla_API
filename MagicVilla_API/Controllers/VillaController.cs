using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepository, IMapper mapper)
        {
            _logger = logger;
            _villaRepository = villaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");

            IEnumerable<Villa> villaList = await _villaRepository.Obtenertodos();

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer la villa con id" + id);
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id.Equals(id));
            //var villa = await _db.Villas.FirstAsync(x => x.Id.Equals(id));
            var villa = await _villaRepository.Obtener(x => x.Id.Equals(id), tracked: false);

            if (villa is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _villaRepository.Obtener(v => v.Nombre.ToLower().Equals(createDto.Nombre), tracked: false) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese Nombre ya Existe");
                return BadRequest(ModelState);
            }

            if (createDto is null)
            {
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(createDto);
            modelo.FechaActualiazcion = DateTime.Now;

            await _villaRepository.Crear(modelo);

            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepository.Obtener(v => v.Id.Equals(id));
            if (villa is null)
            {
                return NotFound();
            }

            await _villaRepository.Remover(villa);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto is null || (id != updateDto.Id))
            {
                return BadRequest();
            }
            Villa modelo = _mapper.Map<Villa>(updateDto);
            modelo.FechaActualiazcion = DateTime.Now;

            await _villaRepository.Actualizar(modelo);
            

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto is null || (id == 0))
            {
                return BadRequest();
            }

            var villa = await _villaRepository.Obtener(v => v.Id.Equals(id), tracked: false);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            if (villa is null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);
            modelo.FechaActualiazcion = DateTime.Now;

            await _villaRepository.Actualizar(modelo);

            return NoContent();
        }

    }
}
