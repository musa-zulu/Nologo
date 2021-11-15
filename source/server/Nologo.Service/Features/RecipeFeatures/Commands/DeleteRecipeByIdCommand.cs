using MediatR;
using Nologo.Domain.Common;
using Nologo.Service.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nologo.Service.Features.RecipeFeatures.Commands
{
    public class DeleteRecipeByIdCommand : IRequest<Response<bool>>
    {
        public Guid RecipeId { get; set; }
        public class DeleteRecipeByIdCommandHandler : IRequestHandler<DeleteRecipeByIdCommand, Response<bool>>
        {
            private readonly IRecipeService _recipeService;
            public DeleteRecipeByIdCommandHandler(IRecipeService recipeService)
            {
                _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            }
            public async Task<Response<bool>> Handle(DeleteRecipeByIdCommand request, CancellationToken cancellationToken)
            {
                var response = await _recipeService.GetByIdAsync(request.RecipeId);
                var recipe = response.Data;

                if (recipe != null)
                {
                    var deleted = await _recipeService.RemoveAsync(recipe);
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