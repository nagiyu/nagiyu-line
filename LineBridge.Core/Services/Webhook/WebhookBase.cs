using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Primitives;

using Common.Utilities;

using LineBridge.Common.Enums.Webhook;
using LineBridge.Common.Interfaces.Webhook;
using LineBridge.Common.Models.Webhook;
using LineBridge.Common.Models.Webhook.Events;
using LineBridge.Common.Models.Webhook.Events.Message;
using LineBridge.Common.Models.Webhook.Events.Message.Objects;

namespace LineBridge.Core.Services.Webhook
{
    public abstract class WebhookBase : IWebhookBase
    {
        /// <summary>
        /// イベントのタイプを表す識別子
        /// </summary>
        protected EventEnums.EventType eventType;

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
        /// チャンネルシークレットを取得する
        /// </summary>
        /// <returns>チャンネルシークレット</returns>
        protected abstract string GetChannelSecret();

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
        /// <param name="headers">ヘッダー</param>
        /// <param name="requestBody">リクエストボディ</param>
        public async Task HandleWebhookEvent(IDictionary<string, StringValues> headers, string requestBody)
        {
#if !DEBUG
            var xLineSignature = headers["X-Line-Signature"];

            if (!ValidateLineSignature(requestBody, xLineSignature))
            {
                throw new UnauthorizedAccessException("Invalid X-Line-Signature");
            }
#endif

            var request = JsonHelper.Deserialize<WebhookRequest<EventBase>>(requestBody);

            // Event でループする
            for (int index = 0; index < request.Events.Count; index++)
            {
                eventType = request.Events[index].Type;
                timestamp = request.Events[index].Timestamp;
                source = request.Events[index].Source;

                if (request.Events[index].Type == EventEnums.EventType.Message)
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

            if (messageEvent.Message.Type == MessageEventEnums.MessageObjectType.Text)
            {
                var textMessage = JsonHelper.Deserialize<WebhookRequest<MessageEvent<TextObject>>>(requestBody).Events[index].Message;
                await HandleTextMessageEvent(textMessage);
            }
            else
            {
                await HandleUndefinedEvent();
            }
        }

        /// <summary>
        /// LINE Webhook リクエストの署名を検証するメソッド
        /// </summary>
        /// <param name="requestBody">リクエストボディ</param>
        /// <param name="xLineSignature">LINE の署名ヘッダー</param>
        /// <returns>true: 署名が一致, false: 署名が不一致</returns>
        private bool ValidateLineSignature(string requestBody, string xLineSignature)
        {
            var channelSecret = GetChannelSecret();

            // シークレットキーとリクエストボディを使って HMAC-SHA256 を計算
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(channelSecret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody));
                var computedSignature = Convert.ToBase64String(hash);

                // 署名を比較
                return computedSignature == xLineSignature;
            }
        }
    }
}
