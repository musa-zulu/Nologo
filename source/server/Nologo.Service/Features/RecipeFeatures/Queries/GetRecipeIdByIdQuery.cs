using MediatR;
using Nologo.Domain.Common;
using Nologo.Domain.Entities;
using Nologo.Service.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nologo.Service.Features.RecipeFeatures.Queries
{
    public class GetRecipeIdByIdQuery : IRequest<Response<Recipe>>
    {
        public Guid RecipeId { get; set; }
        public class GetRecipeIdByIdQueryHandler : IRequestHandler<GetRecipeIdByIdQuery, Response<Recipe>>
        {
            private readonly IRecipeService _recipeService;
            public GetRecipeIdByIdQueryHandler(IRecipeService recipeService)
            {
                _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            }
            public async Task<Response<Recipe>> Handle(GetRecipeIdByIdQuery request, CancellationToken cancellationToken)
            {
                return await _recipeService.GetByIdAsync(request.RecipeId);
            }
        }
    }
}
