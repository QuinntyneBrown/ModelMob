using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using ModelMob.Infrastructure.Data;
using ModelMob.Core.Entities;

namespace ModelService.Features.Models
{
    public class SaveModelCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Model.ModelId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ModelApiModel Model { get; set; }
        }

        public class Response
        {			
            public int ModelId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IModelMobDbContext _context { get; set; }
            public Handler(IModelMobDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var model = await _context.Models.FindAsync(request.Model.ModelId);

                if (model == null) _context.Models.Add(model = new Model());

                model.Firstname = request.Model.Firstname;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ModelId = model.ModelId };
            }
        }
    }
}
