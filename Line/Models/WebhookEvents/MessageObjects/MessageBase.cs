using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Line.Models.WebhookEvents.MessageObjects
{
    /// <summary>
    /// メッセージタイプの Enum
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MessageType
    {
        /// <summary>
        /// テキスト
        /// </summary>
        Text,

        /// <summary>
        /// 画像
        /// </summary>
        Image,

        /// <summary>
        /// 動画
        /// </summary>
        Video,

        /// <summary>
        /// 音声
        /// </summary>
        Audio,

        /// <summary>
        /// ファイル
        /// </summary>
        File,

        /// <summary>
        /// 位置情報
        /// </summary>
        Location,

        /// <summary>
        /// スタンプ
        /// </summary>
        Sticker
    }

    /// <summary>
    /// 画像ファイルの提供元タイプ
    /// </summary>
    public enum ContentProviderType
    {
        /// <summary>
        /// LINEユーザー
        /// </summary>
        Line,

        /// <summary>
        /// 外部
        /// </summary>
        External
    }

    public class MessageBase
    {
        /// <summary>
        /// メッセージタイプ
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// コンテントの提供元
    /// </summary>
    public class ContentProvider
    {
        /// <summary>
        /// ファイルの提供元
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ファイルのURL
        /// </summary>
        public string OriginalContentUrl { get; set; }

        /// <summary>
        /// プレビュー画像のURL
        /// </summary>
        public string PreviewImageUrl { get; set; }
    }
}
