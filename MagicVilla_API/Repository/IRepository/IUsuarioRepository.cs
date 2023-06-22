using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        bool IsUsuarioUnico(string userName);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
