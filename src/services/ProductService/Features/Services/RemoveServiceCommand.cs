using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ProductService.Features.Services
{
    public class RemoveServiceCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Service.ServiceId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Service Service { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Services.Remove(await _context.Services.FindAsync(request.Service.ServiceId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
