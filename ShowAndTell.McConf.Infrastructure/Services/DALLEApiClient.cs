using Newtonsoft.Json;
using RestSharp;
using ShowAndTell.McConf.Application.Common.Interfaces;
using ShowAndTell.McConf.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure.Services
{
    public class DALLEApiClient : IDALLEApiClient
    {
        private RestClient _client;

        public async Task<Video> GenerateVideo(Script script)
        {
            var apiKey = "";

            _client = new RestClient("https://api.openai.com/v1/");
            _client.AddDefaultHeader("Authorization", $"Bearer {apiKey}");

            var request = new RestRequest("dalle-mini/video/generate", Method.Post);
            request.AddJsonBody(new
            {
                prompt = script.Text,
                size = $"{script.Width}x{script.Height}",
                fps = script.FPS
            });

            var response = await _client.ExecuteAsync(request);
            var content = JsonConvert.DeserializeObject<Video>(response.Content);
            return content;
        }
    }
}
