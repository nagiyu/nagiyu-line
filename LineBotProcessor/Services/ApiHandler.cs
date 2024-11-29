using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Common.Utilities;

using LineBotProcessor.Interfaces;
using LineBotProcessor.Models.Reply;

namespace LineBotProcessor.Services
{
    /// <summary>
    /// LINE API との通信を
    /// </summary>
    public class ApiHandler : IApiHandler
    {
        private readonly HttpClient httpClient;

        public ApiHandler(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task SendReplyAsync(ReplyRequest request)
        {
            var channelAccessToken = AppSettings.GetSetting("LineSettings:ChannelAccessToken");

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);

            var json = JsonHelper.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.line.me/v2/bot/message/reply", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                LogHelper.WriteLog($"Error: {error}");

                throw new Exception($"LINE API Error: {error}");
            }
        }
    }
}
