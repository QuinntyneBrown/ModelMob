using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductService.Features.Services
{
    [Authorize]
    [ApiController]
    [Route("api/services")]
    public class ServicesController
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveServiceCommand.Response>> Save(SaveServiceCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Service.ServiceId}")]
        public async Task Remove(RemoveServiceCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ServiceId}")]
        public async Task<ActionResult<GetServiceByIdQuery.Response>> GetById([FromRoute]GetServiceByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetServicesQuery.Response>> Get()
            => await _mediator.Send(new GetServicesQuery.Request());
    }
}
