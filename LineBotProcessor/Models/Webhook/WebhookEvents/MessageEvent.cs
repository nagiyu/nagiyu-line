using LineBotProcessor.Models.Webhook.WebhookEvents.MessageObjects;

namespace LineBotProcessor.Models.Webhook.WebhookEvents
{
    /// <summary>
    /// メッセージイベント
    /// </summary>
    public class MessageEvent<T> : WebhookEventBase where T : MessageBase
    {
        /// <summary>
        /// 応答メッセージを送る際に使用する応答トークン
        /// </summary>
        public string ReplyToken { get; set; }

        /// <summary>
        /// メッセージの内容を含むオブジェクト
        /// </summary>
        public T Message { get; set; }
    }
}
