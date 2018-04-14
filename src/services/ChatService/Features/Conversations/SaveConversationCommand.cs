using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using ModelMob.Infrastructure.Data;
using ModelMob.Core.Entities;

namespace ChatService.Features.Conversations
{
    public class SaveConversationCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Conversation.ConversationId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ConversationApiModel Conversation { get; set; }
        }

        public class Response
        {			
            public int ConversationId { get; set; }
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
                var conversation = await _context.Conversations.FindAsync(request.Conversation.ConversationId);

                if (conversation == null) _context.Conversations.Add(conversation = new Conversation());
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ConversationId = conversation.ConversationId };
            }
        }
    }
}
