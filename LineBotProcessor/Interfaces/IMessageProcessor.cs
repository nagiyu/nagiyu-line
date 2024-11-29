using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBotProcessor.Interfaces
{
    /// <summary>
    /// メッセージ処理サービスのインターフェース
    /// </summary>
    public interface IMessageProcessor
    {
        Task ProcessMessageAsync(string requestBody);

        Task ProcessGyaruMessageAsync(string requestBody);
    }
}
