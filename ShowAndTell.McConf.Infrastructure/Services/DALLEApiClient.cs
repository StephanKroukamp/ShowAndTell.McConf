using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure.Services
{
    public class DALLEApiClient
    {
        private readonly RestClient _client;

        public DALLEApiClient(string apiKey)
        {
            _client = new RestClient("https://api.openai.com/v1/");
            _client.AddDefaultHeader("Authorization", $"Bearer {apiKey}");
        }

        public async Task<Video> GenerateVideo(Script script)
        {
            var request = new RestRequest("dalle-mini/video/generate", Method.POST);
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
