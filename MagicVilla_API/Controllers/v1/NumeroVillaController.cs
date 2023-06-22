using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepository _villaRepository;
        private readonly INumeroVillaRepository _numeroVillaRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepository villaRepository, IMapper mapper, INumeroVillaRepository numeroVillaRepository)
        {
            _logger = logger;
            _villaRepository = villaRepository;
            _mapper = mapper;
            _response = new();
            _numeroVillaRepository = numeroVillaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numero de villas");

                IEnumerable<NumeroVilla> numeroVillasList = await _numeroVillaRepository.Obtenertodos(includeProperties: "Villa");

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillasList);
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traerel numero de villa con id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { "Error al traer la villa con id" + id };
                    _response.IsExitoso = false;
                    return _response;
                }

                var numeroVilla = await _numeroVillaRepository.Obtener(x => x.VillaNo.Equals(id), tracked: false, includeProperties: "Villa");

                if (numeroVilla is null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { "Vacio" };
                    _response.IsExitoso = false;
                    return _response;
                }

                _response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.statusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.statusCode = HttpStatusCode.BadRequest;
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroVillaRepository.Obtener(v => v.VillaNo.Equals(createDto.VillaNo), tracked: false) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La villa con ese numero ya Existe");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { "La villa con ese numero ya Existe" };
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                if (await _villaRepository.Obtener(v => v.Id.Equals(createDto.VillaId)) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id de la Villa no existe!");
                    return BadRequest(ModelState);
                }

                if (createDto is null)
                {
                    return BadRequest(createDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroVillaRepository.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.statusCode = HttpStatusCode.BadRequest;
                return _response;
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _numeroVillaRepository.Obtener(v => v.VillaNo.Equals(id));
                if (villa is null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroVillaRepository.Remover(villa);
                _response.statusCode = HttpStatusCode.NoContent;

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.statusCode = HttpStatusCode.BadRequest;
            }

            return Ok(_response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            try
            {
                if (updateDto is null || (id != updateDto.VillaNo))
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _villaRepository.Obtener(v => v.Id.Equals(updateDto.VillaId)) is null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id de la Villa no existe!");
                    return BadRequest(ModelState);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroVillaRepository.Actualizar(modelo);
                _response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.statusCode = HttpStatusCode.BadRequest;
            }

            return Ok(_response);
        }


    }
}
