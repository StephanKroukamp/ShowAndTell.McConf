using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Application.Prompts.GenerateVideo
{
    public class GenerateAudioCommand : IRequest<GenerateVideoResponse>
    {
        public string Script { get; set; } = null!;
        public string VoiceId { get; set; } = null!;
    }

    public class GenerateVideoResponse
    {
    }
}
