using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ModelMob.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ModelMob.ModelService.Features.Models
{
    public class GetModelsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ModelApiModel> Models { get; set; }
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
                    Models = await _context.Models.Select(x => ModelApiModel.FromModel(x)).ToListAsync()
                };
        }
    }
}
