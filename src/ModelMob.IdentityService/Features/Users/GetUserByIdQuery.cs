using ModelMob.Infrastructure.Data;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace ModelMob.IdentityService
{
    public class GetUserByIdQuery
    {
        public class Request : IRequest<Response> {
            public int UserId { get; set; }
        }

        public class Response
        {			
            public UserApiModel User { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IModelMobDbContext _context;
            public Handler(IModelMobDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    User = UserApiModel.FromUser(await _context.Users.FindAsync(request.UserId))
                };
        }
    }
}
