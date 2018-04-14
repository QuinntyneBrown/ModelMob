using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ProductService.Features.Services
{
    public class SaveServiceCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Service.ServiceId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ServiceApiModel Service { get; set; }
        }

        public class Response
        {			
            public int ServiceId { get; set; }
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
                var service = await _context.Services.FindAsync(request.Service.ServiceId);

                if (service == null) _context.Services.Add(service = new Service());

                service.Name = request.Service.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ServiceId = service.ServiceId };
            }
        }
    }
}
