using System.Collections.Generic;

using Line.Models.WebhookEvents;
using Line.Models.WebhookEvents.MessageObjects;

namespace Line.Models
{
    /// <summary>
    /// Webhookイベントのリクエストボディ
    /// </summary>
    public class WebhookRequest<T> where T : MessageBase
    {
        /// <summary>
        /// Webhookイベントを受信すべきボットのユーザーID
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Webhookイベントオブジェクトの配列
        /// </summary>
        public List<MessageEvent<T>> Events { get; set; }
    }
}
