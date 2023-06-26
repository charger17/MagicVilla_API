using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly IMapper _mapper;

        public UsuarioRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<UsuarioAplicacion> userManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            secretKey = configuration.GetValue<String>("ApiSettings:Secret");
        }

        public bool IsUsuarioUnico(string userName)
        {
            var usuario = _db.UsuariosAplicacion.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());

            return usuario is null;

        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var usuario = await _db.UsuariosAplicacion.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName);

            bool isValid = await _userManager.CheckPasswordAsync(usuario, loginRequestDTO.Password);

            if(usuario is null || isValid is false)
            {
                return new LoginResponseDTO() { Token = "", Usuario =  null };
            }

            //Si el usuario existe generamos el JWT token.
            var roles = await _userManager.GetRolesAsync(usuario);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),

                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDto>(usuario),
                Rol = roles.FirstOrDefault()
            };

            return loginResponseDTO;
        }

        public async Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO)
        {
            Usuario usuario = new()
            {
                UserName = registroRequestDTO.UserName,
                Password = registroRequestDTO.Password,
                Nombres = registroRequestDTO.Nombres,
                Rol = registroRequestDTO.Rol,
            };

            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();

            usuario.Password = null;

            return usuario;
        }
    }
}
