using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBridge.Models.Webhook.Events
{
    /// <summary>
    /// Webhookイベントオブジェクトの共通プロパティ
    /// </summary>
    public class EventBase
    {
        /// <summary>
        /// イベントのタイプを表す識別子
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// チャネルの状態
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// イベント発生時刻 (UNIXタイム・ミリ秒)
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// イベントの送信元情報 
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// WebhookイベントID
        /// </summary>
        public string WebhookEventId { get; set; }

        /// <summary>
        /// 送信のコンテキスト
        /// </summary>
        public DeliveryContext DeliveryContext { get; set; }
    }

    /// <summary>
    /// 送信のコンテキスト
    /// </summary>
    public class DeliveryContext
    {
        /// <summary>
        /// Webhookイベントが再送されたものかどうか
        /// </summary>
        public bool IsRedelivery { get; set; }
    }

    /// <summary>
    /// イベントの送信元情報
    /// </summary>
    public class Source
    {
        /// <summary>
        /// イベントの送信元情報を表す識別子
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 送信元ユーザーのID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 送信元グループトークのグループID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 送信元複数人トークのトークルームID
        /// </summary>
        public string RoomId { get; set; }
    }
}
