using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;
using ShowAndTell.McConf.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Application.Prompts.GenerateVideo
{
    public class GenerateVideoRequest : IRequest<Video>
    {
        public Script Script { get; set; }

        public string ApiKey { get; set; }
    }

    public class GenerateVideoHandler : IRequestHandler<GenerateVideoRequest, Video>
    {
        private readonly IDALLEApiClient _dalleApiClient;

        public GenerateVideoHandler(IDALLEApiClient dalleApiClient)
        {
            _dalleApiClient = dalleApiClient;
        }

        public async Task<Video> Handle(GenerateVideoRequest request, CancellationToken cancellationToken)
        {
            return await _dalleApiClient.GenerateVideo(request.Script, request.ApiKey);
        }
    }
}
