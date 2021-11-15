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
    public class CreateRecipeCommand : IRequest<Response<bool>>
    {
        public AddOrEditRecipeDto RecipeDto { get; set; }

        public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IRecipeService _recipeService;
            public CreateRecipeCommandHandler(IRecipeService recipeService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            }
            public async Task<Response<bool>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
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
                recipe.RecipeId = Guid.NewGuid();
                return await _recipeService.AddAsync(recipe);
            }
        }
    }
}
