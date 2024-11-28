using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Line.Models;
using Line.Models.WebhookEvents;
using Line.Models.WebhookEvents.MessageObjects;

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
        public async Task<IActionResult> SendMessage()
        {
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync(); // JSON文字列ゲット✨

            // リクエストをログに追記する
            System.IO.File.AppendAllText(outputPath, JsonConvert.SerializeObject(requestBody) + "\n");

            // Snake CaseやCamel Caseのキーを自動変換
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy() // Camel Case対応（例: name, age）
                }
            };

            var request = JsonConvert.DeserializeObject<WebhookRequest<WebhookEventBase>>(requestBody, settings);

            // Event でループする
            for (int index = 0; index < request.Events.Count; index++)
            {
                if (request.Events[index].Type == "message")
                {
                    var messageEvent = JsonConvert.DeserializeObject<WebhookRequest<MessageEvent<MessageBase>>>(requestBody, settings).Events[index];

                    var replyToken = messageEvent.ReplyToken;

                    if (messageEvent.Message.Type == "text")
                    {
                        var textMessage = JsonConvert.DeserializeObject<WebhookRequest<MessageEvent<TextMessage>>>(requestBody, settings).Events[index].Message;

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
                                    Text = textMessage.Text
                                }
                            }
                        };

                        // payload の JSON のキーをキャメルケースにする
                        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

#if !DEBUG
                        var url = "https://api.line.me/v2/bot/message/reply";
                        var response = await httpClient.PostAsync(url, content);

                        if (!response.IsSuccessStatusCode)
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            System.IO.File.AppendAllText(outputPath, $"Error: {errorMessage}\n");
                        }
#endif
                    }
                    else if (messageEvent.Message.Type == "image")
                    {
                        var imageMessage = JsonConvert.DeserializeObject<WebhookRequest<MessageEvent<ImageMessage>>>(requestBody, settings).Events[index].Message;

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
                                    Text = "画像は受け付けてないお"
                                }
                            }
                        };

                        // payload の JSON のキーをキャメルケースにする
                        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

#if !DEBUG
                        var url = "https://api.line.me/v2/bot/message/reply";
                        var response = await httpClient.PostAsync(url, content);

                        if (!response.IsSuccessStatusCode)
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            System.IO.File.AppendAllText(outputPath, $"Error: {errorMessage}\n");
                        }
#endif
                    }
                }
            };

            return Ok();
        }
    }
}
