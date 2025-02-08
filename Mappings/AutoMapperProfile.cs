using AutoMapper;
using carrinho_api.DTOs;
using carrinho_api.Entities;

namespace carrinho_api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Witness, WitnessDTO>().ReverseMap();
            CreateMap<Witness, WitnessCreateDTO>().ReverseMap();
            CreateMap<Local, LocalDTO>().ReverseMap();
            
        }
    }
}
