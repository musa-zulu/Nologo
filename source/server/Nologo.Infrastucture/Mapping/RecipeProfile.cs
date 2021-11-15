using AutoMapper;
using Nologo.Domain.Dtos;
using Nologo.Domain.Entities;

namespace Nologo.Infrastructure.Mapping
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {            
            CreateMap<AddOrEditRecipeDto, Recipe>()
                 .ForMember(dest => dest.Ingredients, opt =>
                    opt.Ignore());
        }
    }
}
