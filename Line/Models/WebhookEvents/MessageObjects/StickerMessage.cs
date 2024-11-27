using System.Collections.Generic;

namespace Line.Models.WebhookEvents.MessageObjects
{
    /// <summary>
    /// スタンプのリソースタイプ
    /// </summary>
    public enum StickerResourceType
    {
        /// <summary>
        /// 静止画スタンプ
        /// </summary>
        STATIC,

        /// <summary>
        /// アニメーションスタンプ
        /// </summary>
        ANIMATION,

        /// <summary>
        /// サウンドスタンプ
        /// </summary>
        SOUND,

        /// <summary>
        /// アニメーション＋サウンドスタンプ
        /// </summary>
        ANIMATION_SOUND,

        /// <summary>
        /// ポップアップスタンプまたはエフェクトスタンプ
        /// </summary>
        POPUP,

        /// <summary>
        /// ポップアップ＋サウンドスタンプまたはエフェクト＋サウンドスタンプ
        /// </summary>
        POPUP_SOUND,

        /// <summary>
        /// カスタムスタンプ
        /// </summary>
        CUSTOM,

        /// <summary>
        /// メッセージスタンプ
        /// </summary>
        MESSAGE,

        /// <summary>
        /// カスタムスタンプ（廃止）
        /// </summary>
        NAME_TEXT,

        /// <summary>
        /// メッセージスタンプ（廃止）
        /// </summary>
        PER_STICKER_TEXT
    }

    /// <summary>
    /// 送信元から送られたスタンプデータを含むメッセージオブジェクト
    /// </summary>
    public class StickerMessage : MessageBase
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
        /// パッケージID
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        /// スタンプID
        /// </summary>
        public string StickerId { get; set; }

        /// <summary>
        /// スタンプのリソースタイプ
        /// </summary>
        public StickerResourceType StickerResourceType { get; set; }

        /// <summary>
        /// スタンプを表すキーワード
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// ユーザーが入力した任意のテキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 引用されたメッセージのメッセージID
        /// </summary>
        public string QuotedMessageId { get; set; }
    }
}
