using System;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Domain.Repositories;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.DeletePrompt
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class DeletePromptCommandHandler : IRequestHandler<DeletePromptCommand>
    {
        private readonly IPromptRepository _promptRepository;

        [IntentManaged(Mode.Ignore)]
        public DeletePromptCommandHandler(IPromptRepository promptRepository)
        {
            _promptRepository = promptRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Unit> Handle(DeletePromptCommand request, CancellationToken cancellationToken)
        {
            var existingPrompt = await _promptRepository.FindByIdAsync(request.Id, cancellationToken);
            _promptRepository.Remove(existingPrompt);
            return Unit.Value;
        }
    }
}