using MagicVilla_Api.Models.DTOs;

namespace MagicVilla_Api.Data;

public static class VillaStore
{
    public static IList<VillaDto> VillaList = new List<VillaDto>
    {
        new VillaDto() { Id = 1, Name = "Villa 1" },
        new VillaDto() { Id = 2, Name = "Villa 2" },
    };
}