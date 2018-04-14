using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ModelService.Features.FeaturedModels
{
    public class SaveFeaturedModelCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.FeaturedModel.FeaturedModelId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public FeaturedModelApiModel FeaturedModel { get; set; }
        }

        public class Response
        {			
            public int FeaturedModelId { get; set; }
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
                var featuredModel = await _context.FeaturedModels.FindAsync(request.FeaturedModel.FeaturedModelId);

                if (featuredModel == null) _context.FeaturedModels.Add(featuredModel = new FeaturedModel());

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { FeaturedModelId = featuredModel.FeaturedModelId };
            }
        }
    }
}
