using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ModelService.Features.FeaturedModels
{
    [Authorize]
    [ApiController]
    [Route("api/featuredModels")]
    public class FeaturedModelsController
    {
        private readonly IMediator _mediator;

        public FeaturedModelsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveFeaturedModelCommand.Response>> Save(SaveFeaturedModelCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{FeaturedModel.FeaturedModelId}")]
        public async Task Remove(RemoveFeaturedModelCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{FeaturedModelId}")]
        public async Task<ActionResult<GetFeaturedModelByIdQuery.Response>> GetById([FromRoute]GetFeaturedModelByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetFeaturedModelsQuery.Response>> Get()
            => await _mediator.Send(new GetFeaturedModelsQuery.Request());
    }
}
