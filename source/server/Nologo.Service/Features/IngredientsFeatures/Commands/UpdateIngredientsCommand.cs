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
    public class UpdateIngredientsCommand : IRequest<Response<bool>>
    {
        public AddOrEditIngredientsDto IngredientsDto { get; set; }

        public class UpdateIngredientsCommandHandler : IRequestHandler<UpdateIngredientsCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IIngredientService _ingredientService;
            public UpdateIngredientsCommandHandler(IIngredientService ingredientService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            }
            public async Task<Response<bool>> Handle(UpdateIngredientsCommand request, CancellationToken cancellationToken)
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
                return await _ingredientService.UpdateAsync(ingredient);
            }
        }
    }
}
