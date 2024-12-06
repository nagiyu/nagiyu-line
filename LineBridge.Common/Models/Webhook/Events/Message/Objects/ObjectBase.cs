using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LineBridge.Common.Enums.Webhook.MessageEventEnums;

namespace LineBridge.Common.Models.Webhook.Events.Message.Objects
{
    public class ObjectBase
    {
        /// <summary>
        /// メッセージタイプ
        /// </summary>
        public MessageObjectType Type { get; set; }
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
