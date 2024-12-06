using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LineBridge.Common.Enums.Message
{
    public class ObjectEnums
    {
        /// <summary>
        /// イベントタイプ
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter), true)]
        public enum EventType
        {
            /// <summary>
            /// メッセージイベント
            /// </summary>
            Text
        }
    }
}
