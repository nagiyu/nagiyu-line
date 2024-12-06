using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LineBridge.Common.Models.Message;
using LineBridge.Common.Models.MessageObjects;

namespace LineBridge.Common.Interfaces.Message
{
    /// <summary>
    /// 応答メッセージ
    /// </summary>
    public interface IReplyMessage
    {
        /// <summary>
        /// 応答メッセージを送る
        /// </summary>
        /// <param name="channelAccessToken">アクセストークン</param>
        /// <param name="request">リクエスト</param>
        /// <returns>レスポンス</returns>
        Task<ReplyMessageResponse> SendReplyMessage<T>(string channelAccessToken, ReplyMessageRequest<T> request) where T : ObjectBase;
    }
}
