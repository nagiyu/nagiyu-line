using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Primitives;

namespace LineBridge.Common.Interfaces.Webhook
{
    public interface IWebhookBase
    {
        /// <summary>
        /// Webhook イベントを処理する
        /// </summary>
        /// <param name="headers">ヘッダー</param>
        /// <param name="requestBody">リクエストボディ</param>
        Task HandleWebhookEvent(IDictionary<string, StringValues> headers, string requestBody);
    }
}
