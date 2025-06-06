using AutoMapper;
using GrupoColorado.Application.DTOs;
using GrupoColorado.Domain.Entities;

namespace GrupoColorado.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Telefone, TelefoneDto>().ReverseMap();
        }
    }
}
