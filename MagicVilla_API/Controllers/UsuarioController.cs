using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private APIResponse _response;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _response = new APIResponse();
        }

        [HttpPost("login")]// /api/usuario/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = await _usuarioRepository.Login(modelo);

            if (loginResponse.Usuario is null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("User o password son incorrectos");
                return BadRequest(_response);
            }

            _response.IsExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Resultado = loginResponse;
            return Ok(_response);

        }

        [HttpPost("registrar")]// /api/usuario/registrar
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            bool isUsuarioUnico = _usuarioRepository.IsUsuarioUnico(modelo.UserName);

            if (!isUsuarioUnico)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Usuario ya existe");
                return BadRequest(_response);
            }

            var usuario = await _usuarioRepository.Registrar(modelo);
            if (usuario is null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Error al registrar Usuario");
                return BadRequest(_response);
            }

            _response.statusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            return Ok(_response);
             
        }

    }
}
