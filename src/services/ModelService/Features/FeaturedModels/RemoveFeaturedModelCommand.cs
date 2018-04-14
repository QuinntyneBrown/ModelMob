using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ModelMob.Infrastructure.Data;
using ModelMob.Core.Entities;
using FluentValidation;

namespace ModelService.Features.FeaturedModels
{
    public class RemoveFeaturedModelCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.FeaturedModel.FeaturedModelId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public FeaturedModel FeaturedModel { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IModelMobDbContext _context { get; set; }
            public Handler(IModelMobDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.FeaturedModels.Remove(await _context.FeaturedModels.FindAsync(request.FeaturedModel.FeaturedModelId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
