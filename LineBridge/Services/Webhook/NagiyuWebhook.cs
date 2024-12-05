using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonKit.Utilities;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;

using OpenAIConnect.Common.Enums;
using OpenAIConnect.Common.Interfaces;
using OpenAIConnect.Common.Models.Request;

using LineBridge.Common.Enums.Message;
using LineBridge.Common.Interfaces.Message;
using LineBridge.Common.Models.Message;
using LineBridge.Common.Models.MessageObjects;
using LineBridge.Common.Models.Webhook.Events.Message.Objects;

using LineBridge.Core.Services.Webhook;

using LineBridge.Consts;
using LineBridge.Interfaces.Webhook;

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
        /// チャンネルシークレットを取得する
        /// </summary>
        /// <returns>チャンネルシークレット</returns>
        protected override string GetChannelSecret()
        {
            return AppSettings.GetSetting("LineSettings:ChannelSecret:Nagiyu");
        }

        /// <summary>
        /// トークの最大件数をチェックする
        /// </summary>
        /// <returns>true: 最大件数に達した, false: 最大件数に達していない</returns>
        protected override async Task<bool> CheckMaxTalkCount()
        {
            var messageCount = await dynamoDbService.GetTodayLineMessageCountAsync(source.UserId, new List<string> { LineConsts.RESET_MESSAGE });

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
                        Type = ObjectEnums.EventType.Text,
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
            var pastMessages = await dynamoDbService.GetLineMessageByUserIDAsync(source.UserId, new List<string> { LineConsts.RESET_MESSAGE });

            var prompts = new List<RequestMessage>
            {
                new RequestMessage
                {
                    Role = OpenAIEnums.Role.System,
                    Content = AppSettings.GetSetting("SystemPrompts:Nagiyu")
                }
            };

            foreach (var pastMessage in pastMessages)
            {
                prompts.Add(new RequestMessage
                {
                    Role = OpenAIEnums.Role.User,
                    Content = pastMessage.MessageText
                });

                prompts.Add(new RequestMessage
                {
                    Role = OpenAIEnums.Role.Assistant,
                    Content = pastMessage.ReplyText
                });
            }

            prompts.Add(new RequestMessage
            {
                Role = OpenAIEnums.Role.User,
                Content = textObject.Text
            });

            var response = textObject.Text != LineConsts.RESET_MESSAGE
                ? await openAIClient.SendRequestAsync(prompts)
                : "Reset completed!!!";

            await dynamoDbService.AddLineMessageAsync(new LineMessage
            {
                Id = Guid.NewGuid(),
                UserId = source.UserId,
                GroupId = source.GroupId,
                RoomId = source.RoomId,
                EventTimestamp = timestamp,
                EventType = eventType.ToString(),
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
                        Type = ObjectEnums.EventType.Text,
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
                        Type = ObjectEnums.EventType.Text,
                        Text = "処理できないメッセージです。すみません！"
                    }
                }
            };

            await replyMessage.SendReplyMessage(accessToken, request);
        }
    }
}
