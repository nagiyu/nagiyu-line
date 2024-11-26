using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace Line.Controllers
{
    public class LineController : Controller
    {
        private readonly string outputPath;
        private readonly string channelAccessToken;
        private readonly HttpClient httpClient;

        public LineController(IOptions<LineSettings> options, HttpClient httpClient)
        {
            outputPath = options.Value.OutputPath;
            channelAccessToken = options.Value.ChannelAccessToken;
            this.httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] WebhookRequest request)
        {
            // リクエストをログに追記する
            System.IO.File.AppendAllText(outputPath, JsonConvert.SerializeObject(request) + "\n");

            if (request?.Events == null || request.Events.Count == 0)
            {
                return Ok();
            }

            // リクエストの type が message でない場合は無視する
            var messageEvent = request.Events[0];
            if (messageEvent.Type != "message")
            {
                return Ok();
            }

            // message の type が text でない場合は無視する
            var message = messageEvent.Message;
            if (message.Type != "text")
            {
                return Ok();
            }

            // replyToken を取得する
            var replyToken = messageEvent.ReplyToken;

            var url = "https://api.line.me/v2/bot/message/reply";

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);

            var payload = new ReplyRequest
            {
                ReplyToken = replyToken,
                Messages = new List<ReplyMessage>
                {
                    new ReplyMessage
                    {
                        Type = "text",
                        Text = message.Text
                    }
                }
            };

            // payload の JSON のキーをキャメルケースにする
            var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return Ok("メッセージが送信されました。");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                System.IO.File.AppendAllText(outputPath, $"Error: {errorMessage}\n");
                return BadRequest($"エラーが発生しました: {errorMessage}");
            }
        }
    }
}
