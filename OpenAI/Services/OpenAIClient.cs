using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Common.Utilities;

using OpenAIConnect.Interfaces;
using OpenAIConnect.Models.Request;
using OpenAIConnect.Models.Response;

namespace OpenAIConnect.Services
{
    public class OpenAIClient : IOpenAIClient
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl = "https://api.openai.com";

        public OpenAIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> SendRequestAsync(string prompt)
        {
            var requestBody = new OpenAIRequest
            {
                Model = "gpt-4o-mini",
                Messages = new List<RequestMessage>
                {
                    new RequestMessage
                    {
                        Role = "system",
                        Content = "You are a helpful assistant."
                    },
                    new RequestMessage
                    {
                        Role = "user",
                        Content = prompt
                    }
                }
            };

            var apiKey = AppSettings.GetSetting("OpenAI:APIKey");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var json = JsonHelper.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{baseUrl}/v1/chat/completions", content);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseJson = JsonHelper.Deserialize<ChatCompletionResponse>(responseBody);

            return responseJson.Choices[0].Message.Content;
        }
    }
}
