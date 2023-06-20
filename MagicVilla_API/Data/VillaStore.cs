using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id = 1, Nombre = "Vista a la piscina"},
            new VillaDto {Id = 2, Nombre= "Vista a la playa"}
        };
    }
}
