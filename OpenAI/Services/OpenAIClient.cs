﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using CommonKit.Utilities;

using SettingsManager.Services;

using OpenAIConnect.Common.Consts;
using OpenAIConnect.Common.Interfaces;
using OpenAIConnect.Common.Models.Request;
using OpenAIConnect.Common.Models.Response;

namespace OpenAIConnect.Services
{
    public class OpenAIClient : IOpenAIClient
    {
        /// <summary>
        /// AppSettingsService
        /// </summary>
        private readonly AppSettingsService appSettingsService;

        private readonly HttpClient httpClient;
        private readonly string baseUrl = "https://api.openai.com";

        public OpenAIClient(AppSettingsService appSettingsService, HttpClient httpClient)
        {
            this.appSettingsService = appSettingsService;
            this.httpClient = httpClient;
        }

        public async Task<string> SendRequestAsync(List<RequestMessage> prompts)
        {
            var requestBody = new OpenAIRequest
            {
                Model = OpenAIConsts.Model.GPT_4O_MINI,
                Messages = prompts
            };

            var apiKey = await appSettingsService.GetValueByKeyAsync("OpenAI:APIKey");

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
