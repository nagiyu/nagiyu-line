using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LineBridge.Common.Models.Webhook.Events;
using LineBridge.Common.Models.Webhook.Events.Message.Objects;

namespace LineBridge.Common.Models.Webhook.Events.Message
{
    public class MessageEvent<T> : EventBase where T : ObjectBase
    {
        /// <summary>
        /// 応答メッセージを送る際に使用する応答トークン
        /// </summary>
        public string ReplyToken { get; set; }

        /// <summary>
        /// メッセージの内容を含むオブジェクト
        /// </summary>
        public T Message { get; set; }
    }
}
