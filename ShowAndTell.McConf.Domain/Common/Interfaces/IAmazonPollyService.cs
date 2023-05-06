using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Domain.Common.Interfaces
{
    public interface IAmazonPollyService
    {
        Task<Stream> SynthesizeSpeechAsync(string text, string voiceId, string outputFormat);
    }
}
