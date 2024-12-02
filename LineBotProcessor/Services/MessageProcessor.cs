﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Utilities;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;

using LineBotProcessor.Interfaces;
using LineBotProcessor.Models.Webhook.WebhookEvents;
using LineBotProcessor.Models.Reply;
using LineBotProcessor.Models.Webhook;
using LineBotProcessor.Models.Webhook.WebhookEvents.MessageObjects;

using OpenAIConnect.Interfaces;
using OpenAIConnect.Models.Request;

namespace LineBotProcessor.Services
{
    /// <summary>
    /// メッセージの種類ごとに処理
    /// </summary>
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IApiHandler apiHandler;
        private readonly IDynamoDbService dynamoDbService;
        private readonly IOpenAIClient openAIClient;

        public MessageProcessor(IApiHandler apiHandler, IDynamoDbService dynamoDbService, IOpenAIClient openAIClient)
        {
            this.apiHandler = apiHandler;
            this.dynamoDbService = dynamoDbService;
            this.openAIClient = openAIClient;
        }

        public async Task ProcessMessageAsync(string requestBody)
        {
            var request = JsonHelper.Deserialize<WebhookRequest<WebhookEventBase>>(requestBody);

            // Event でループする
            for (int index = 0; index < request.Events.Count; index++)
            {
                var source = request.Events[index].Source;
                var eventType = request.Events[index].Type;

                if (request.Events[index].Type == "message")
                {
                    var messageEvent = JsonHelper.Deserialize<WebhookRequest<MessageEvent<MessageBase>>>(requestBody).Events[index];

                    var replyToken = messageEvent.ReplyToken;

                    var messageCount = await dynamoDbService.GetTodayLineMessageCountAsync(source.UserId);
                    if (messageCount >= int.Parse(AppSettings.GetSetting("LineSettings:MaxMessageCount:Nagiyu")))
                    {
                        var payload = new ReplyRequest
                        {
                            ReplyToken = replyToken,
                            Messages = new List<ReplyMessage>
                            {
                                new ReplyMessage
                                {
                                    Type = "text",
                                    Text = "今日の会話上限に達しました。また明日ご利用ください！"
                                }
                            }
                        };

                        await apiHandler.SendReplyAsync(payload);

                        continue;
                    }

                    if (messageEvent.Message.Type == "text")
                    {
                        var textMessage = JsonHelper.Deserialize<WebhookRequest<MessageEvent<TextMessage>>>(requestBody).Events[index].Message;

                        var pastMessages = await dynamoDbService.GetLineMessageByUserIDAsync(source.UserId);

                        var prompts = new List<RequestMessage>
                        {
                            new RequestMessage
                            {
                                Role = "system",
                                Content = AppSettings.GetSetting("SystemPrompts:Nagiyu")
                            }
                        };

                        foreach (var pastMessage in pastMessages)
                        {
                            prompts.Add(new RequestMessage
                            {
                                Role = "user",
                                Content = pastMessage.MessageText
                            });

                            prompts.Add(new RequestMessage
                            {
                                Role = "assistant",
                                Content = pastMessage.ReplyText
                            });
                        }

                        prompts.Add(new RequestMessage
                        {
                            Role = "user",
                            Content = textMessage.Text
                        });

                        var response = textMessage.Text != "リセット" 
                            ? await openAIClient.SendRequestAsync(prompts) 
                            : "Reset completed!!!";

                        await dynamoDbService.AddLineMessageAsync(new LineMessage
                        {
                            Id = Guid.NewGuid(),
                            UserId = source.UserId,
                            GroupId = source.GroupId,
                            RoomId = source.RoomId,
                            EventTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                            EventType = eventType,
                            MessageId = textMessage.Id,
                            MessageText = textMessage.Text,
                            ReplyText = response
                        });

                        var payload = new ReplyRequest
                        {
                            ReplyToken = replyToken,
                            Messages = new List<ReplyMessage>
                            {
                                new ReplyMessage
                                {
                                    Type = "text",
                                    Text = response
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

        public async Task ProcessGyaruMessageAsync(string requestBody)
        {
            var request = JsonHelper.Deserialize<WebhookRequest<WebhookEventBase>>(requestBody);

            // Event でループする
            for (int index = 0; index < request.Events.Count; index++)
            {
                var source = request.Events[index].Source;
                var eventType = request.Events[index].Type;

                if (request.Events[index].Type == "message")
                {
                    var messageEvent = JsonHelper.Deserialize<WebhookRequest<MessageEvent<MessageBase>>>(requestBody).Events[index];

                    var replyToken = messageEvent.ReplyToken;

                    var messageCount = await dynamoDbService.GetTodayLineMessageCountAsync(source.UserId);
                    if (messageCount >= int.Parse(AppSettings.GetSetting("LineSettings:MaxMessageCount:Gyaru")))
                    {
                        var payload = new ReplyRequest
                        {
                            ReplyToken = replyToken,
                            Messages = new List<ReplyMessage>
                            {
                                new ReplyMessage
                                {
                                    Type = "text",
                                    Text = "今日のトークはもうMAXいっちゃったわ💦また明日話そ〜ねん💕"
                                }
                            }
                        };

                        await apiHandler.SendGyaruReplyAsync(payload);

                        continue;
                    }

                    if (messageEvent.Message.Type == "text")
                    {
                        var textMessage = JsonHelper.Deserialize<WebhookRequest<MessageEvent<TextMessage>>>(requestBody).Events[index].Message;

                        var pastMessages = await dynamoDbService.GetLineMessageByUserIDAsync(source.UserId);

                        var prompts = new List<RequestMessage> 
                        {
                            new RequestMessage
                            {
                                Role = "system",
                                Content = AppSettings.GetSetting("SystemPrompts:Gyaru")
                            }
                        };

                        foreach (var pastMessage in pastMessages)
                        {
                            prompts.Add(new RequestMessage
                            {
                                Role = "user",
                                Content = pastMessage.MessageText
                            });

                            prompts.Add(new RequestMessage
                            {
                                Role = "assistant",
                                Content = pastMessage.ReplyText
                            });
                        }

                        prompts.Add(new RequestMessage
                        {
                            Role = "user",
                            Content = textMessage.Text
                        });

                        var response = textMessage.Text != "リセット"
                            ? await openAIClient.SendRequestAsync(prompts)
                            : "Reset completed!!!";

                        await dynamoDbService.AddLineMessageAsync(new LineMessage
                        {
                            Id = Guid.NewGuid(),
                            UserId = source.UserId,
                            GroupId = source.GroupId,
                            RoomId = source.RoomId,
                            EventTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                            EventType = eventType,
                            MessageId = textMessage.Id,
                            MessageText = textMessage.Text,
                            ReplyText = response
                        });

                        var payload = new ReplyRequest
                        {
                            ReplyToken = replyToken,
                            Messages = new List<ReplyMessage>
                            {
                                new ReplyMessage
                                {
                                    Type = "text",
                                    Text = response
                                }
                            }
                        };

                        await apiHandler.SendGyaruReplyAsync(payload);
                    }
                    else
                    {
                        var payload = new ReplyRequest
                        {
                            ReplyToken = replyToken,
                            Messages = new List<ReplyMessage>
                            {
                                new ReplyMessage
                                {
                                    Type = "text",
                                    Text = "テキスト以外は受け付けてないんだ。ごめんね！"
                                }
                            }
                        };

                        await apiHandler.SendGyaruReplyAsync(payload);
                    }
                }
            };
        }
    }
}
