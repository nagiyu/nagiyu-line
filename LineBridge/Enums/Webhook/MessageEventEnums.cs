using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LineBridge.Enums.Webhook
{
    public class MessageEventEnums
    {
        /// <summary>
        /// メッセージオブジェクトのタイプ
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter), true)]
        public enum MessageObjectType
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
    }
}
