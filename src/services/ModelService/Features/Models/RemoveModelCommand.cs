using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ModelService.Features.Models
{
    public class RemoveModelCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Model.ModelId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public ModelApiModel Model { get; set; }
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
                _context.Models.Remove(await _context.Models.FindAsync(request.Model.ModelId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
