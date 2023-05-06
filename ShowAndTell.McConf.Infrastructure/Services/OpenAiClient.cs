using Newtonsoft.Json;
using RestSharp;
using ShowAndTell.McConf.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure.Services
{
    public class OpenAIClient : IOpenAiClient
    {
        public async Task<string> GenerateText(string prompt, string apiKey)
        {
            var client = new RestClient("https://api.openai.com/v1/completions");

            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + apiKey);

            string requestBody = "{\"model\":\"text-davinci-003\",\"prompt\":\"" + prompt + "\",\"max_tokens\":1000,\"temperature\":0}";

            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<GptResponse>(response.Content);

            var stringy = JsonConvert.SerializeObject(result);

            if (result is null)
            {
                throw new Exception("Failed to deserialize GPT response");
            }

            var firstChoice = result.Choices.First();

            if (firstChoice is null)
            {
                throw new Exception("No Choices returned from GPT");
            }

            if (string.IsNullOrWhiteSpace(firstChoice.Text))
            {
                throw new Exception("First choice does not have any text");
            }

            return firstChoice.Text;
        }
    }
}