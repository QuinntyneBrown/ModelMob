using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ModelMob.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ModelMob.IdentityService
{
    public class GetUsersQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {			
            public IEnumerable<UserApiModel> Users { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IModelMobDbContext _context;
            public Handler(IModelMobDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response() {
                    Users = await _context.Users.Select(x => UserApiModel.FromUser(x)).ToListAsync()
                };
        }
    }
}
