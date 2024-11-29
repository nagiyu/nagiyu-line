using System.Collections.Generic;

using LineBotProcessor.Models.Webhook.WebhookEvents;

namespace LineBotProcessor.Models.Webhook
{
    /// <summary>
    /// Webhookイベントのリクエストボディ
    /// </summary>
    public class WebhookRequest<T> where T : WebhookEventBase
    {
        /// <summary>
        /// Webhookイベントを受信すべきボットのユーザーID
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Webhookイベントオブジェクトの配列
        /// </summary>
        public List<T> Events { get; set; }
    }
}
