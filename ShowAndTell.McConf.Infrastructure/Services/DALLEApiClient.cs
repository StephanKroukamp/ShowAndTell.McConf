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

        public async Task<Video> GenerateVideo(Script script, string apiKey)
        {
            _client = new RestClient("https://api.openai.com/v1/");
            _client.AddDefaultHeader("Authorization", $"Bearer {apiKey}");

            var request = new RestRequest("images/generations", Method.Post);
            request.AddJsonBody(new
            {
                prompt = script.Text,
                size = $"{script.Width}x{script.Height}",
                n = 1,
                response_format = "url"
            });

            var response = await _client.ExecuteAsync(request);
            var content = JsonConvert.DeserializeObject<VideoResponse>(response.Content);
            return content.Data.FirstOrDefault();
        }
    }
}
