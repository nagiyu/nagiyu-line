using System.ComponentModel.DataAnnotations;

namespace Line.Models.WebhookEvents.MessageObjects
{
    /// <summary>
    /// 送信元から送られた音声を含むメッセージオブジェクト
    /// </summary>
    public class AudioMessage : MessageBase
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 音声ファイルの長さ
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// 音声ファイルの提供元
        /// </summary>
        public ContentProvider ContentProvider { get; set; } // 音声ファイルの提供元情報～！
    }
}
