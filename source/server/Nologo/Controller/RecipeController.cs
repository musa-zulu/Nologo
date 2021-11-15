using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Nologo.Service.Features.RecipeFeatures.Commands;
using Nologo.Service.Features.RecipeFeatures.Queries;

namespace Nologo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IMediator _mediator;
        public IMediator Mediator
        {
            get { return _mediator ??= HttpContext.RequestServices.GetService<IMediator>(); }
            set
            {
                if (_mediator != null) throw new InvalidOperationException("Mediator is already set");
                _mediator = value;
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateRecipeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllRecepesQuery()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid recipeId)
        {
            return Ok(await Mediator.Send(new GetRecipeIdByIdQuery { RecipeId = recipeId }));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid recipeId)
        {
            return Ok(await Mediator.Send(new DeleteRecipeByIdCommand { RecipeId = recipeId }));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Guid recipeId, UpdateRecipeCommand command)
        {
            if (recipeId != command.RecipeDto.RecipeId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
