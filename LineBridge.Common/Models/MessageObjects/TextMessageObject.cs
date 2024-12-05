using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LineBridge.Common.Enums.Message.ObjectEnums;

namespace LineBridge.Common.Models.MessageObjects
{
    /// <summary>
    /// テキストメッセージ
    /// </summary>
    public class TextMessageObject : ObjectBase
    {
        /// <summary>
        /// メッセージタイプ
        /// </summary>
        public EventType Type { get; set; } = EventType.Text;

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
