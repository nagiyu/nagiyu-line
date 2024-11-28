using System.Collections.Generic;

using Newtonsoft.Json;

using Line.Models.WebhookEvents;

namespace Line.Models
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
