using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ModelMob.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ModelMob.ChatService.Features.Conversations
{
    public class GetConversationsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ConversationApiModel> Conversations { get; set; }
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
                    Conversations = await _context.Conversations.Select(x => ConversationApiModel.FromConversation(x)).ToListAsync()
                };
        }
    }
}
