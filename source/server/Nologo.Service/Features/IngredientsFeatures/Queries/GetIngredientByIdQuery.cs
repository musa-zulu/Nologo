using System;
using MediatR;
using Nologo.Domain.Common;
using Nologo.Domain.Entities;
using Nologo.Service.Contracts;
using System.Threading.Tasks;
using System.Threading;

namespace Nologo.Service.Features.IngredientsFeatures.Queries
{
    public class GetIngredientByIdQuery : IRequest<Response<Ingredients>>
    {
        public Guid IngredientsId { get; set; }
        public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, Response<Ingredients>>
        {
            private readonly IIngredientService _ingredientService;
            public GetIngredientByIdQueryHandler(IIngredientService ingredientService)
            {
                _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            }
            public async Task<Response<Ingredients>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
            {
                return await _ingredientService.GetByIdAsync(request.IngredientsId);
            }
        }
    }
}
