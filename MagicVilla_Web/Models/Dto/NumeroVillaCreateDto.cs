using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class NumeroVillaCreateDto
    {
        [Required]
        [Display(Name = "Numero de villa")]
        public int VillaNo { get; set; }

        [Required]
        [Display(Name = "Id de villa")]

        public int VillaId { get; set; }

        [Display(Name = "Detalles Especiales")]

        public string DetallesEspeciales { get; set; }

    }
}
