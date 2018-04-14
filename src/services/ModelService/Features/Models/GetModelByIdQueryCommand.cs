using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ModelMob.Infrastructure.Data;
using FluentValidation;

namespace ModelService.Features.Models
{
    public class GetModelByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ModelId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ModelId { get; set; }
        }

        public class Response
        {
            public ModelApiModel Model { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IModelMobDbContext _context { get; set; }
            public Handler(IModelMobDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Model = ModelApiModel.FromModel(await _context.Models.FindAsync(request.ModelId))
                };
        }
    }
}
