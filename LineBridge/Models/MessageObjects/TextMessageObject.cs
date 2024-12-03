using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBridge.Models.MessageObjects
{
    /// <summary>
    /// テキストメッセージ
    /// </summary>
    public class TextMessageObject : ObjectBase
    {
        /// <summary>
        /// メッセージタイプ
        /// </summary>
        public string Type { get; set; } = "text";

        /// <summary>
        /// メッセージのテキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// LINE絵文字オブジェクトの配列
        /// </summary>
        public List<EmojiObject> Emojis { get; set; }
    }

    /// <summary>
    /// LINE絵文字オブジェクト (WIP)
    /// </summary>
    public class EmojiObject
    {
    }
}
