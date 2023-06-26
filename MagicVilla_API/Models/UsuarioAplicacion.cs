using Microsoft.AspNetCore.Identity;

namespace MagicVilla_API.Models
{
    public class UsuarioAplicacion: IdentityUser
    {
        public string Nombres { get; set; }
    }
}
