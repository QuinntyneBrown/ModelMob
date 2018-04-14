using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ModelMob.Infrastructure.Data;
using ModelMob.Core.Entities;
using FluentValidation;

namespace ChatService.Features.Conversations
{
    public class RemoveConversationCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Conversation.ConversationId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Conversation Conversation { get; set; }
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
                _context.Conversations.Remove(await _context.Conversations.FindAsync(request.Conversation.ConversationId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
