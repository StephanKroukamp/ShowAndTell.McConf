using Newtonsoft.Json;
using RestSharp;
using ShowAndTell.McConf.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure.Services
{
    public class OpenAIClient : IOpenAiClient
    {
        public async Task<string> GenerateText(string prompt)
        {
            try
            {
                //
                string apiKey = "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";

                var client = new RestClient("https://api.openai.com/v1/completions");
                
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + apiKey);
                
                string requestBody = "{\"model\":\"text-davinci-003\",\"prompt\":\"" + prompt + "\",\"max_tokens\":7,\"temperature\":0}";

                request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

                var response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<dynamic>(response.Content);

                var stop = "";
            }
            catch (Exception e)
            {
                throw;
            }

            return "";
        }
    }
}