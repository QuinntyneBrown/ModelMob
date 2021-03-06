using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityService
{
    [ApiController]
    [Route("api/users")]
    public class UsersController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateUserCommand.Response>> Save(CreateUserCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateUserCommand.Response>> Update(UpdateUserCommand.Request request)
            => await _mediator.Send(request);

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult<AuthenticateCommand.Response>> SignIn(AuthenticateCommand.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetUsersQuery.Response>> GetAll()
            => await _mediator.Send(new GetUsersQuery.Request());

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetUserByIdQuery.Response>> GetById(GetUserByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpPost("password")]
        public async Task ChangePassword(UserChangePasswordCommand.Request request)
            => await _mediator.Send(request);        
    }
}
