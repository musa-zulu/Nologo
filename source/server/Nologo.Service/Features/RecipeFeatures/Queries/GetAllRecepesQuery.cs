using MediatR;
using Nologo.Domain.Common;
using Nologo.Domain.Entities;
using Nologo.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nologo.Service.Features.RecipeFeatures.Queries
{
    public class GetAllRecepesQuery : IRequest<Response<IEnumerable<Recipe>>>
    {
        public class GetAllRecepesQueryHandler : IRequestHandler<GetAllRecepesQuery, Response<IEnumerable<Recipe>>>
        {
            private readonly IRecipeService _recipeService;
            public GetAllRecepesQueryHandler(IRecipeService recipeService)
            {
                _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            }
            public async Task<Response<IEnumerable<Recipe>>> Handle(GetAllRecepesQuery request, CancellationToken cancellationToken)
            {
                return await _recipeService.GetAllAsync();
            }
        }
    }
}
