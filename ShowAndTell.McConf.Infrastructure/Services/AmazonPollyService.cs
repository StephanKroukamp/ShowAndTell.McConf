using Amazon.Polly.Model;
using Amazon.Polly;
using ShowAndTell.McConf.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure.Services
{
    public class AmazonPollyService : IAmazonPollyService
    {
        private readonly IAmazonPolly _pollyClient;
        public AmazonPollyService(IAmazonPolly pollyClient)
        {
            _pollyClient = pollyClient;
        }

        public async Task<Stream> SynthesizeSpeechAsync(string text, string voiceId, string outputFormat)
        {
            var request = new SynthesizeSpeechRequest
            {
                OutputFormat = outputFormat,
                Text = text,
                VoiceId = voiceId
            };

            var response = await _pollyClient.SynthesizeSpeechAsync(request);

            return response.AudioStream;
        }
    }
}
