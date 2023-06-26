namespace MagicVilla_API.Models.Dto
{
    public class LoginResponseDTO
    {
        public UsuarioDto Usuario { get; set; }

        public string Token { get; set; }

        public string Rol { get; set; }
    }
}
