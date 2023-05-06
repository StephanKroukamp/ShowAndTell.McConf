using System;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;

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
            
            if (request.Distance <= 50 && request.Distance > 30) // green
            {
                message = "please generate me a message saying you are being detected by our device!";
            }
            else if (request.Distance <= 30 && request.Distance > 15) // yellow
            {
                message = "please generate me a message saying you are getting closer to our device, do not come closer!";
            }
            else if (request.Distance <= 15) // red
            {
                message = "please generate me a message saying you are to close to our device, we did warn you!";
            }

            if (message is null)
            {
                throw new Exception("Unexpected distance received, please adjust parameters");
            }

            message += ", please take this text and generate a unique message with different wording.";

            return await _openAiClient.GenerateText(message, request.ApiKey);
        }
    }
}