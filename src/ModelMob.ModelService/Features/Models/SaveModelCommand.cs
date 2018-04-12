using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using ModelMob.Infrastructure.Data;
using ModelMob.Core.Entities;

namespace ModelMob.ModelService.Features.Models
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
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var model = await _context.Models.FindAsync(request.Model.ModelId);

                if (model == null) _context.Models.Add(model = new Model());

                model.Name = request.Model.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ModelId = model.ModelId };
            }
        }
    }
}
