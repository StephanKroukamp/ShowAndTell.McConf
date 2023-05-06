using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GenerateMessageByDistance
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GenerateMessageByDistanceQueryHandler : IRequestHandler<GenerateMessageByDistanceQuery, string>
    {
        private readonly IOpenAiClient _openAiClient;

        public GenerateMessageByDistanceQueryHandler(IOpenAiClient openAiClient)
        {
            _openAiClient = openAiClient;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<string> Handle(GenerateMessageByDistanceQuery request, CancellationToken cancellationToken)
        {
            var message = "";

            if (request.Distance <= 1)
            {
                message = "please generate me a message saying you are to close";
            }
            else if (request.Distance > 1 & request.Distance <= 2)
            {
                message = "please generate me a message saying you are getting closer";
            }
            else
            {
                message = "please generate me a message saying you are being detected";
            }

            if (message is null)
            {
                throw new Exception("Unexpected distance received, please adjust paramters");
            }

            return await _openAiClient.GenerateText(message);
        }
    }
}