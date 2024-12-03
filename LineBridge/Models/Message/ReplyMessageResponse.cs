using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBridge.Models.Message
{
    /// <summary>
    /// 応答メッセージのレスポンス
    /// </summary>
    public class ReplyMessageResponse
    {
        /// <summary>
        /// 送信したメッセージの配列
        /// </summary>
        public List<SentMessage> SentMessages { get; set; }
    }

    /// <summary>
    /// 送信したメッセージ
    /// </summary>
    public class SentMessage
    {
        /// <summary>
        /// 送信したメッセージのID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// メッセージの引用トークン
        /// </summary>
        public string QuoteToken { get; set; }
    }
}
