using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nologo.Domain.Common;
using Nologo.Service.Contracts;
using Nologo.Domain.Entities;
using System;

namespace Nologo.Service.Features.IngredientsFeatures.Queries
{
    public class GetAllIngredientsQuery : IRequest<Response<IEnumerable<Ingredients>>>
    {
        public class GetAllIngredientsQueryHandler : IRequestHandler<GetAllIngredientsQuery, Response<IEnumerable<Ingredients>>>
        {
            private readonly IIngredientService _ingredientService;
            public GetAllIngredientsQueryHandler(IIngredientService ingredientService)
            {                
                _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            }
            public async Task<Response<IEnumerable<Ingredients>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
            {
                return await _ingredientService.GetAllAsync();
            }
        }
    }
}
