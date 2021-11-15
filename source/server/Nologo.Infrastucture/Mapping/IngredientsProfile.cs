using AutoMapper;
using Nologo.Domain.Dtos;
using Nologo.Domain.Entities;

namespace Nologo.Infrastructure.Mapping
{
    public class IngredientsProfile : Profile
    {
        public IngredientsProfile()
        {
            CreateMap<AddOrEditIngredientsDto, Ingredients>().ReverseMap();
        }
    }
}
