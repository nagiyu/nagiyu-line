namespace LineBotProcessor.Models.Webhook.WebhookEvents.MessageObjects
{
    /// <summary>
    /// 送信元から送られた画像を含むメッセージオブジェクト
    /// </summary>
    public class ImageMessage : MessageBase
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
        /// 画像ファイルの提供元
        /// </summary>
        public ContentProvider ContentProvider { get; set; }

        /// <summary>
        /// 画像セット
        /// </summary>
        public ImageSet ImageSet { get; set; }
    }

    /// <summary>
    /// 画像セット
    /// </summary>
    public class ImageSet
    {
        /// <summary>
        /// 画像セットID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 同時に送信した画像セットのインデックス
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// 同時に送信した画像の総数
        /// </summary>
        public int? Total { get; set; }
    }
}
