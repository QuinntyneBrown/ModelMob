using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ProductService.Features.Services
{
    public class GetServiceByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ServiceId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ServiceId { get; set; }
        }

        public class Response
        {
            public ServiceApiModel Service { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Service = ServiceApiModel.FromService(await _context.Services.FindAsync(request.ServiceId))
                };
        }
    }
}
