using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Nologo.Service.Features.IngredientsFeatures.Commands;
using Nologo.Service.Features.IngredientsFeatures.Queries;

namespace Nologo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
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
        public async Task<IActionResult> Create(CreateIngredientsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllIngredientsQuery()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid ingredientsId)
        {
            return Ok(await Mediator.Send(new GetIngredientByIdQuery { IngredientsId = ingredientsId }));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid ingredientsId)
        {
            return Ok(await Mediator.Send(new DeleteIngredientsByIdCommand { IngredientsId = ingredientsId }));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Guid taskId, UpdateIngredientsCommand command)
        {
            if (taskId != command.IngredientsDto.IngredientsId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
