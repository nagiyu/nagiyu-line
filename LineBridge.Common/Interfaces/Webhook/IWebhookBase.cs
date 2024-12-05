using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBridge.Common.Interfaces.Webhook
{
    public interface IWebhookBase
    {
        /// <summary>
        /// Webhook イベントを処理する
        /// </summary>
        /// <param name="requestBody">リクエストボディ</param>
        Task HandleWebhookEvent(string requestBody);
    }
}
