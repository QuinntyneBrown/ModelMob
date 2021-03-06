using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using System;

namespace IdentityService
{
    public class UpdateUserCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Username).NotNull();
                RuleFor(request => request.UserId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public int UserId { get; set; }
            public string Username { get; set; }
        }

        public class Response
        {			
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);
                user.Username = request.Username;                    
                await _context.SaveChangesAsync(cancellationToken);
                return new Response() { UserId = user.UserId };
            }
        }
    }
}
