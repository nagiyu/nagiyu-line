using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LineBridge.Models.Webhook.Events;

namespace LineBridge.Models.Webhook
{
    /// <summary>
    /// Webhookイベントのリクエストボディ
    /// </summary>
    public class WebhookRequest<T> where T : EventBase
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
