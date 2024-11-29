using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Utilities;

using LineBotProcessor.Interfaces;
using LineBotProcessor.Models.Webhook.WebhookEvents;
using LineBotProcessor.Models.Reply;
using LineBotProcessor.Models.Webhook;
using LineBotProcessor.Models.Webhook.WebhookEvents.MessageObjects;

namespace LineBotProcessor.Services
{
    /// <summary>
    /// メッセージの種類ごとに処理
    /// </summary>
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IApiHandler apiHandler;

        public MessageProcessor(IApiHandler apiHandler)
        {
            this.apiHandler = apiHandler;
        }

        public async Task ProcessMessageAsync(string requestBody)
        {
            var request = JsonHelper.Deserialize<WebhookRequest<WebhookEventBase>>(requestBody);

            // Event でループする
            for (int index = 0; index < request.Events.Count; index++)
            {
                if (request.Events[index].Type == "message")
                {
                    var messageEvent = JsonHelper.Deserialize<WebhookRequest<MessageEvent<MessageBase>>>(requestBody).Events[index];

                    var replyToken = messageEvent.ReplyToken;

                    if (messageEvent.Message.Type == "text")
                    {
                        var textMessage = JsonHelper.Deserialize<WebhookRequest<MessageEvent<TextMessage>>>(requestBody).Events[index].Message;

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

                        await apiHandler.SendReplyAsync(payload);
                    }
                    else if (messageEvent.Message.Type == "image")
                    {
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

                        await apiHandler.SendReplyAsync(payload);
                    }
                }
            };
        }
    }
}
