using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LineBotProcessor.Models.Reply;

namespace LineBotProcessor.Interfaces
{
    /// <summary>
    /// API 通信サービスのインターフェース
    /// </summary>
    public interface IApiHandler
    {
        Task SendReplyAsync(ReplyRequest request);
    }
}
