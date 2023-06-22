using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaCreateDto
    {

        [Required(ErrorMessage = "{0} es requerido")]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string Detalle { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        public double Tarifa { get; set; }

        public int Ocupantes { get; set; }

        [Display(Name ="Metros cuadrados")]
        public double MetrosCuadrados { get; set; }

        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }

    }
}
