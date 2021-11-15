using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Nologo.Domain.Common;
using Nologo.Service.Contracts;

namespace Nologo.Service.Features.IngredientsFeatures.Commands
{
    public class DeleteIngredientsByIdCommand : IRequest<Response<bool>>
    {
        public Guid IngredientsId { get; set; }
        public class DeleteIngredientsByIdCommandHandler : IRequestHandler<DeleteIngredientsByIdCommand, Response<bool>>
        {
            private readonly IIngredientService _ingredientService;
            public DeleteIngredientsByIdCommandHandler(IIngredientService ingredientService)
            {
                _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            }
            public async Task<Response<bool>> Handle(DeleteIngredientsByIdCommand request, CancellationToken cancellationToken)
            {
                var response =  await _ingredientService.GetByIdAsync(request.IngredientsId);
                var ingredient = response.Data;

                if (ingredient != null)
                {
                    var deleted = await _ingredientService.RemoveAsync(ingredient);
                    return new Response<bool>
                    {
                        Succeeded = deleted.Succeeded,
                        Message = deleted.Message
                    };
                }                 
                return new Response<bool>
                {
                    Succeeded = false,
                    Message = "Object Could not be found!"
                };
            }
        }
    }
}