using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Domain.Entities;
using ShowAndTell.McConf.Domain.Repositories;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.CreatePrompt
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreatePromptCommandHandler : IRequestHandler<CreatePromptCommand, Guid>
    {
        private readonly IPromptRepository _promptRepository;

        [IntentManaged(Mode.Ignore)]
        public CreatePromptCommandHandler(IPromptRepository promptRepository)
        {
            _promptRepository = promptRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Guid> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            var newPrompt = new Prompt
            {
            };

            _promptRepository.Add(newPrompt);
            await _promptRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newPrompt.Id;
        }
    }
}