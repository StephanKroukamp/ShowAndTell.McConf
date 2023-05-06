using System;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GenerateMessage
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GenerateMessageQueryHandler : IRequestHandler<GenerateMessageQuery, bool>
    {
        private readonly IOpenAiClient _openAiClient;

        public GenerateMessageQueryHandler(IOpenAiClient openAiClient)
        {
            _openAiClient = openAiClient;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<bool> Handle(GenerateMessageQuery request, CancellationToken cancellationToken)
        {
            string generatedText = await _openAiClient.GenerateText(request.PrompText);

            return true;
        }
    }
}