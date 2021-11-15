using AutoMapper;
using MediatR;
using Nologo.Domain.Common;
using Nologo.Domain.Dtos;
using Nologo.Domain.Entities;
using Nologo.Service.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nologo.Service.Features.IngredientsFeatures.Commands
{
    public class CreateIngredientsCommand : IRequest<Response<bool>>
    {
        public AddOrEditIngredientsDto IngredientsDto { get; set; }

        public class CreateIngredientsCommandHandler : IRequestHandler<CreateIngredientsCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IIngredientService _ingredientService;
            public CreateIngredientsCommandHandler(IIngredientService ingredientService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            }
            public async Task<Response<bool>> Handle(CreateIngredientsCommand request, CancellationToken cancellationToken)
            {
                var ingredient = _mapper.Map<Ingredients>(request.IngredientsDto);

                if (ingredient == null)
                {
                    return new Response<bool>
                    {
                        Message = "Invalid object",
                        Succeeded = false
                    };
                }
                ingredient.IngredientsId = Guid.NewGuid();
                return await _ingredientService.AddAsync(ingredient);
            }
        }
    }
}
