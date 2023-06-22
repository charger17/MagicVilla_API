using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }

        [Required]
        public int Ocupantes { get; set; }

        [Required]
        [Display(Name = "Metros Cuadrados")]
        public double MetrosCuadrados { get; set; }

        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }

    }
}
