using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBAccessor.Models
{
    public class LineMessage
    {
        /// <summary>
        /// ID
        /// </summary>
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        /// <summary>
        /// 送信元ユーザーID
        /// </summary>
        [DynamoDBProperty]
        public string UserId { get; set; }

        /// <summary>
        /// グループID
        /// </summary>
        [DynamoDBProperty]
        public string GroupId { get; set; }

        /// <summary>
        /// トークルームID
        /// </summary>
        [DynamoDBProperty]
        public string RoomId { get; set; }

        /// <summary>
        /// イベント発生時刻 (UNIXタイム)
        /// </summary>
        [DynamoDBProperty]
        public long EventTimestamp { get; set; }

        /// <summary>
        /// イベントタイプ
        /// </summary>
        [DynamoDBProperty]
        public string EventType { get; set; }

        /// <summary>
        /// メッセージID
        /// </summary>
        [DynamoDBProperty]
        public string MessageId { get; set; }

        /// <summary>
        /// メッセージテキスト
        /// </summary>
        [DynamoDBProperty]
        public string MessageText { get; set; }

        /// <summary>
        /// 返却テキスト
        /// </summary>
        [DynamoDBProperty]
        public string ReplyText { get; set; }
    }
}
