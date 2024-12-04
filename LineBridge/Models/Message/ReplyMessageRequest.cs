using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LineBridge.Models.MessageObjects;

namespace LineBridge.Models.Message
{
    /// <summary>
    /// 応答メッセージのリクエストボディ
    /// </summary>
    public class ReplyMessageRequest<T> where T : ObjectBase
    {
        /// <summary>
        /// Webhookで受信する応答トークン
        /// </summary>
        public string ReplyToken { get; set; }

        /// <summary>
        /// 送信するメッセージ
        /// </summary>
        public List<T> Messages { get; set; }

        /// <summary>
        /// ユーザーに通知しない
        /// </summary>
        public bool NotificationDisabled { get; set; }
    }
}
