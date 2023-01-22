using AutoMapper;
using MagicVilla_Web.Models.DTOs;

namespace MagicVilla_Web;
public class MappingConfig : Profile {
    public MappingConfig() {
        CreateMap<VillaDto, VillaCreateDTO>().ReverseMap();
        CreateMap<VillaDto, VillaUpdateDTO>().ReverseMap();

        CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
        CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
    }
}