using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Utilities;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;

using OpenAIConnect.Interfaces;
using OpenAIConnect.Models.Request;

using LineBridge.Interfaces.Message;
using LineBridge.Interfaces.Webhook;
using LineBridge.Models.Webhook.Events.Message.Objects;
using LineBridge.Models.Message;
using LineBridge.Models.MessageObjects;

using static LineBridge.Enums.Message.ObjectEnums;

namespace LineBridge.Services.Webhook
{
    public class NagiyuWebhook : WebhookBase, INagiyuWebhook
    {
        /// <summary>
        /// DynamoDB サービス
        /// </summary>
        private readonly IDynamoDbService dynamoDbService;

        /// <summary>
        /// OpenAI クライアント
        /// </summary>
        private readonly IOpenAIClient openAIClient;

        /// <summary>
        /// 応答メッセージ
        /// </summary>
        private readonly IReplyMessage replyMessage;

        public NagiyuWebhook(IDynamoDbService dynamoDbService, IOpenAIClient openAIClient, IReplyMessage replyMessage)
        {
            this.dynamoDbService = dynamoDbService;
            this.openAIClient = openAIClient;
            this.replyMessage = replyMessage;
        }

        /// <summary>
        /// トークの最大件数をチェックする
        /// </summary>
        /// <returns>true: 最大件数に達した, false: 最大件数に達していない</returns>
        protected override async Task<bool> CheckMaxTalkCount()
        {
            var messageCount = await dynamoDbService.GetTodayLineMessageCountAsync(source.UserId);

            return messageCount >= AppSettings.GetSetting<int>("LineSettings:MaxMessageCount:Nagiyu");
        }

        /// <summary>
        /// トークが最大件数に達した際のメッセージを送信する
        /// </summary>
        protected override async Task SendMaxTalkCountMessage()
        {
            var accessToken = AppSettings.GetSetting("LineSettings:ChannelAccessToken:Nagiyu");

            var request = new ReplyMessageRequest<TextMessageObject>
            {
                ReplyToken = replyToken,
                Messages = new List<TextMessageObject>
                {
                    new TextMessageObject
                    {
                        Type = EventType.Text,
                        Text = "今日の会話上限に達しました。また明日ご利用ください！"
                    }
                }
            };

            await replyMessage.SendReplyMessage(accessToken, request);
        }

        /// <summary>
        /// テキストメッセージイベントを処理する
        /// </summary>
        /// <param name="textObject">送信元から送られたテキストを含むメッセージオブジェクト</param>
        protected override async Task HandleTextMessageEvent(TextObject textObject)
        {

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
                Content = textObject.Text
            });

            var response = textObject.Text != "リセット"
                ? await openAIClient.SendRequestAsync(prompts)
                : "Reset completed!!!";

            await dynamoDbService.AddLineMessageAsync(new LineMessage
            {
                Id = Guid.NewGuid(),
                UserId = source.UserId,
                GroupId = source.GroupId,
                RoomId = source.RoomId,
                EventTimestamp = timestamp,
                EventType = eventType,
                MessageId = textObject.Id,
                MessageText = textObject.Text,
                ReplyText = response
            });

            var accessToken = AppSettings.GetSetting("LineSettings:ChannelAccessToken:Nagiyu");

            var request = new ReplyMessageRequest<TextMessageObject>
            {
                ReplyToken = replyToken,
                Messages = new List<TextMessageObject>
                {
                    new TextMessageObject
                    {
                        Type = EventType.Text,
                        Text = response
                    }
                }
            };

            await replyMessage.SendReplyMessage(accessToken, request);
        }

        /// <summary>
        /// 未定義のイベントを処理する
        /// </summary>
        protected override async Task HandleUndefinedEvent()
        {
            var accessToken = AppSettings.GetSetting("LineSettings:ChannelAccessToken:Nagiyu");

            var request = new ReplyMessageRequest<TextMessageObject>
            {
                ReplyToken = replyToken,
                Messages = new List<TextMessageObject>
                {
                    new TextMessageObject
                    {
                        Type = EventType.Text,
                        Text = "処理できないメッセージです。すみません！"
                    }
                }
            };

            await replyMessage.SendReplyMessage(accessToken, request);
        }
    }
}
