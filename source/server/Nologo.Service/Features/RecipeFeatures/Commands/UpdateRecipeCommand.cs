using AutoMapper;
using MediatR;
using Nologo.Domain.Common;
using Nologo.Domain.Dtos;
using Nologo.Domain.Entities;
using Nologo.Service.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nologo.Service.Features.RecipeFeatures.Commands
{
    public class UpdateRecipeCommand : IRequest<Response<bool>>
    {
        public AddOrEditRecipeDto RecipeDto { get; set; }

        public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IRecipeService _recepeService;
            public UpdateRecipeCommandHandler(IRecipeService recepeService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _recepeService = recepeService ?? throw new ArgumentNullException(nameof(recepeService));
            }
            public async Task<Response<bool>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
            {
                var recipe = _mapper.Map<Recipe>(request.RecipeDto);
                if (recipe == null)
                {
                    return new Response<bool>
                    {
                        Message = "Invalid object",
                        Succeeded = false
                    };
                }
                var savedRecord = await _recepeService.GetByIdAsync(recipe.RecipeId);

                if (savedRecord != null)
                {
                    var data = savedRecord.Data;
                    
                    recipe.DateCreated = data.DateCreated;

                    if (recipe.RecipeFileName == null)
                        recipe.RecipeFileName = data.RecipeFileName;
                    if (recipe.Author == null)
                        recipe.Author = data.Author;
                }
                if (request.RecipeDto.Ingredients != null)
                {
                    foreach (var ingredient in request.RecipeDto.Ingredients)
                    {
                        recipe.Ingredients += ingredient.Name + ",";
                    }
                }
                recipe.Ingredients = recipe.Ingredients.Trim(',');
                recipe.Author = recipe.Author;
                recipe.DateUpdated = DateTime.UtcNow;
                recipe.UpdatedBy = recipe.Author;
                return await _recepeService.UpdateAsync(recipe);
            }
        }
    }
}
