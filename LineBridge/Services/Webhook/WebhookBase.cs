using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Utilities;

using LineBridge.Interfaces.Webhook;
using LineBridge.Models.Webhook;
using LineBridge.Models.Webhook.Events;
using LineBridge.Models.Webhook.Events.Message;
using LineBridge.Models.Webhook.Events.Message.Objects;

using static LineBridge.Enums.Webhook.MessageEventEnums;

namespace LineBridge.Services.Webhook
{
    public abstract class WebhookBase : IWebhookBase
    {
        /// <summary>
        /// イベントのタイプを表す識別子
        /// </summary>
        protected string eventType;

        /// <summary>
        /// イベント発生時刻 (UNIXタイム・ミリ秒)
        /// </summary>
        protected long timestamp;

        /// <summary>
        /// イベントの送信元情報
        /// </summary>
        protected Source source;

        /// <summary>
        /// 応答メッセージを送る際に使用する応答トークン
        /// </summary>
        protected string replyToken;


        /// <summary>
        /// トークの最大件数をチェックする
        /// </summary>
        /// <returns>true: 最大件数に達した, false: 最大件数に達していない</returns>
        protected abstract Task<bool> CheckMaxTalkCount();

        /// <summary>
        /// トークが最大件数に達した際のメッセージを送信する
        /// </summary>
        protected abstract Task SendMaxTalkCountMessage();

        /// <summary>
        /// テキストメッセージイベントを処理する
        /// </summary>
        /// <param name="textObject">送信元から送られたテキストを含むメッセージオブジェクト</param>
        protected abstract Task HandleTextMessageEvent(TextObject textObject);

        /// <summary>
        /// 未定義のイベントを処理する
        /// </summary>
        protected abstract Task HandleUndefinedEvent();

        /// <summary>
        /// Webhook イベントを処理する
        /// </summary>
        /// <param name="requestBody">リクエストボディ</param>
        public async Task HandleWebhookEvent(string requestBody)
        {
            var request = JsonHelper.Deserialize<WebhookRequest<EventBase>>(requestBody);

            // Event でループする
            for (int index = 0; index < request.Events.Count; index++)
            {
                eventType = request.Events[index].Type;
                timestamp = request.Events[index].Timestamp;
                source = request.Events[index].Source;

                if (request.Events[index].Type == "message")
                {
                    await HandleMessageEvent(requestBody, index);
                }
            }
        }

        /// <summary>
        /// メッセージイベントを処理する
        /// </summary>
        /// <param name="requestBody">リクエストボディ</param>
        /// <param name="index">イベントのインデックス</param>
        protected async Task HandleMessageEvent(string requestBody, int index)
        {
            var messageEvent = JsonHelper.Deserialize<WebhookRequest<MessageEvent<ObjectBase>>>(requestBody).Events[index];

            replyToken = messageEvent.ReplyToken;

            if (CheckMaxTalkCount().Result)
            {
                await SendMaxTalkCountMessage();
                return;
            }

            if (messageEvent.Message.Type == MessageObjectType.Text)
            {
                var textMessage = JsonHelper.Deserialize<WebhookRequest<MessageEvent<TextObject>>>(requestBody).Events[index].Message;
                await HandleTextMessageEvent(textMessage);
            }
            else
            {
                await HandleUndefinedEvent();
            }
        }
    }
}
