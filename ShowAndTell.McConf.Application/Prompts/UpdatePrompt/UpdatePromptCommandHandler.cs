using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Domain.Repositories;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.UpdatePrompt
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdatePromptCommandHandler : IRequestHandler<UpdatePromptCommand>
    {
        private readonly IPromptRepository _promptRepository;

        [IntentManaged(Mode.Ignore)]
        public UpdatePromptCommandHandler(IPromptRepository promptRepository)
        {
            _promptRepository = promptRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Unit> Handle(UpdatePromptCommand request, CancellationToken cancellationToken)
        {
            var existingPrompt = await _promptRepository.FindByIdAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}