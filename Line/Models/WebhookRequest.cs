using Line.Models.WebhookEvents;
using System.Collections.Generic;

namespace Line.Models
{
    /// <summary>
    /// Webhookイベントのリクエストボディ
    /// </summary>
    public class WebhookRequest
    {
        /// <summary>
        /// Webhookイベントを受信すべきボットのユーザーID
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Webhookイベントオブジェクトの配列
        /// </summary>
        public List<WebhookEventBase> Events { get; set; }
    }
}
