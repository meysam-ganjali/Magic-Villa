using AutoMapper;
using MagicVilla_Api.Models.DTOs;
using MagicVilla_Api.Models;

namespace MagicVilla_Api;

public class MappingConfig:Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDto>();
        CreateMap<VillaDto, Villa>();

        CreateMap<Villa, VillaUpdateDTO>();
        CreateMap<VillaUpdateDTO, Villa>();

        CreateMap<Villa, VillaCreateDTO>();
        CreateMap<VillaCreateDTO, Villa>();

        CreateMap<VillaNumber, VillaNumberCreateDTO>();
        CreateMap<VillaNumberCreateDTO, VillaNumber>();

        CreateMap<VillaNumber, VillaNumberUpdateDTO>();
        CreateMap<VillaNumberUpdateDTO, VillaNumber>();

        CreateMap<VillaNumberDTO, VillaNumber>();
        CreateMap<VillaNumber, VillaNumberDTO>();
    }
}