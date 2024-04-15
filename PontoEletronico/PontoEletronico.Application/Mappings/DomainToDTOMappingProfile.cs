using AutoMapper;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Domain.Entities;

namespace PontoEletronico.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {            
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
            CreateMap<RegistroPonto, RegistroPontoDTO>().ReverseMap();
        }
    }
}
