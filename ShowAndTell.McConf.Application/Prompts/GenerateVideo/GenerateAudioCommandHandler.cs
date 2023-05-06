using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ShowAndTell.McConf.Domain;
using ShowAndTell.McConf.Domain.Common.Interfaces;
using Amazon.Polly.Model;
using Amazon.Polly;
using System.IO;

namespace ShowAndTell.McConf.Application.Prompts.GenerateVideo
{
    public class GenerateAudioCommandHandler : IRequestHandler<GenerateAudioCommand, GenerateVideoResponse>
    {

        public async Task<GenerateVideoResponse> Handle(GenerateAudioCommand request, CancellationToken cancellationToken)
        {
            //var speechStream = await _pollyService.SynthesizeSpeechAsync(request.Script, request.VoiceId, "mp3");

            // Use the speech stream to generate a video using a video generation library

            return new GenerateVideoResponse();
        }
    }
}
