using System.ComponentModel.DataAnnotations;

namespace Line.Models.WebhookEvents.MessageObjects
{
    /// <summary>
    /// 送信元から送られたファイルを含むメッセージオブジェクト
    /// </summary>
    public class FileMessage : MessageBase
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// ファイルサイズ
        /// </summary>
        public long FileSize { get; set; }
    }
}
