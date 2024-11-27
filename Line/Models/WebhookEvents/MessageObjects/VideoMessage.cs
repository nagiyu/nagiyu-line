using System.ComponentModel.DataAnnotations;

namespace Line.Models.WebhookEvents.MessageObjects
{
    /// <summary>
    /// 送信元から送られた動画を含むメッセージオブジェクト
    /// </summary>
    public class VideoMessage : MessageBase
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// メッセージの引用トークン
        /// </summary>
        public string QuoteToken { get; set; }

        /// <summary>
        /// 動画ファイルの長さ
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// 動画ファイルの提供元
        /// </summary>
        public ContentProvider ContentProvider { get; set; }
    }
}
