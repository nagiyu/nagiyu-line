using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Common.Utilities;
using LineBridge.Models.Message;
using LineBridge.Models.MessageObjects;
using LineBridge.Interfaces.Message;

namespace LineBridge.Services.Message
{
    /// <summary>
    /// 応答メッセージ
    /// </summary>
    public class ReplyMessage : IReplyMessage
    {
        private const string URL = "https://api.line.me/v2/bot/message/reply";

        private readonly HttpClient httpClient;

        public ReplyMessage(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// 応答メッセージを送る
        /// </summary>
        /// <param name="channelAccessToken">アクセストークン</param>
        /// <param name="request">リクエスト</param>
        /// <returns>レスポンス</returns>
        public async Task<ReplyMessageResponse> SendReplyMessage<T>(string channelAccessToken, ReplyMessageRequest<T> request) where T : ObjectBase
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);

            var json = JsonHelper.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

#if DEBUG
            System.IO.File.AppendAllText("Debug.log", $"{DateTime.Now} {json}\n");

            return new ReplyMessageResponse
            {
                SentMessages = new List<SentMessage>
                {
                    new SentMessage
                    {
                        Id = 0,
                        QuoteToken = ""
                    }
                }
            };
#else
            var response = await httpClient.PostAsync(URL, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new Exception($"LINE API Error: {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonHelper.Deserialize<ReplyMessageResponse>(responseJson);
#endif
        }
    }
}
