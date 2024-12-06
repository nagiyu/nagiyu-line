using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBridge.Common.Models.Webhook.Events.Message.Objects
{
    /// <summary>
    /// 送信元から送られたテキストを含むメッセージオブジェクト
    /// </summary>
    public class TextObject : ObjectBase
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
        /// メッセージのテキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 1個以上のLINE絵文字オブジェクトの配列
        /// </summary>
        public List<EmojiObject> Emojis { get; set; }

        /// <summary>
        /// メンションの情報を含むオブジェクト
        /// </summary>
        public MentionObject Mention { get; set; }

        /// <summary>
        /// 引用されたメッセージのメッセージID
        /// </summary>
        public string QuotedMessageId { get; set; }
    }

    /// <summary>
    /// LINE絵文字オブジェクト
    /// </summary>
    public class EmojiObject
    {
        /// <summary>
        /// プロパティ内の絵文字の開始位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// LINE絵文字の文字列の長さ
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// LINE絵文字の集合を示すプロダクトID
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// プロダクトID内のLINE絵文字のID
        /// </summary>
        public string EmojiId { get; set; }
    }

    /// <summary>
    /// メンションの情報を含むオブジェクト
    /// </summary>
    public class MentionObject
    {
        /// <summary>
        /// 1個以上のメンションオブジェクトの配列
        /// </summary>
        public List<MentioneeObject> Mentionees { get; set; }
    }

    /// <summary>
    /// メンションオブジェクト
    /// </summary>
    public class MentioneeObject
    {
        /// <summary>
        /// メンションの開始位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// メンションの長さ
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// メンションの対象
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// メンションされたユーザーまたはボットのユーザーID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Webhookイベントを受信したボット（destination）に対するメンションかどうか
        /// </summary>
        public bool? IsSelf { get; set; }
    }
}
