using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ModelMob.ModelService.Features.Models
{
    [ApiController]
    [Route("api/models")]
    public class ModelsController
    {
        private readonly IMediator _mediator;

        public ModelsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveModelCommand.Response>> Save(SaveModelCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Model.ModelId}")]
        public async Task Remove(RemoveModelCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ModelId}")]
        public async Task<ActionResult<GetModelByIdQuery.Response>> GetById([FromRoute]GetModelByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetModelsQuery.Response>> Get()
            => await _mediator.Send(new GetModelsQuery.Request());
    }
}
