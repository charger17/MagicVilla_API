using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        bool IsUsuarioUnico(string userName);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<UsuarioDto> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
