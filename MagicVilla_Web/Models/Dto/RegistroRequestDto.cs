using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class RegistroRequestDto 
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Password { get; set; }

        public string Rol { get; set; } 
    }
}
