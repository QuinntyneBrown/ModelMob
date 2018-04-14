using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ModelService.Features.FeaturedModels
{
    public class GetFeaturedModelByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.FeaturedModelId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int FeaturedModelId { get; set; }
        }

        public class Response
        {
            public FeaturedModelApiModel FeaturedModel { get; set; }
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
                    FeaturedModel = FeaturedModelApiModel.FromFeaturedModel(await _context.FeaturedModels.FindAsync(request.FeaturedModelId))
                };
        }
    }
}
