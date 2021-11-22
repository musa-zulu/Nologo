using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Nologo.Service.Features.RecipeFeatures.Commands;
using Nologo.Service.Features.RecipeFeatures.Queries;
using System.IO;
using System.Net.Http.Headers;
using Nologo.Domain.Dtos;

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
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] AddOrEditRecipeDto recipeDto)
        {
            CreateRecipeCommand command = new() { RecipeDto = recipeDto };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllRecepesQuery()));
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById(Guid recipeId)
        {
            return Ok(await Mediator.Send(new GetRecipeIdByIdQuery { RecipeId = recipeId }));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid recipeId)
        {
            return Ok(await Mediator.Send(new DeleteRecipeByIdCommand { RecipeId = recipeId }));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(AddOrEditRecipeDto recipeDto)
        {
            if (recipeDto.RecipeId == Guid.Empty)
            {
                return BadRequest();
            }
            UpdateRecipeCommand command = new() { RecipeDto = recipeDto };
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("uploadFile"), DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length <= 0) return BadRequest();
                //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                var fileName = file.FileName;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
