using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GenerateMessage
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GenerateMessageQueryHandler : IRequestHandler<GenerateMessageQuery, string>
    {
        private readonly IOpenAiClient _openAiClient;

        public GenerateMessageQueryHandler(IOpenAiClient openAiClient)
        {
            _openAiClient = openAiClient;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<string> Handle(GenerateMessageQuery request, CancellationToken cancellationToken)
        {
            return await _openAiClient.GenerateText(request.PrompText);
        }
    }
}