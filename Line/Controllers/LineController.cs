using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

using Common.Utilities;

using LineBridge.Interfaces.Webhook;

namespace Line.Controllers
{
    public class LineController : Controller
    {
        private readonly INagiyuWebhook nagiyuWebhook;
        private readonly IGyaruWebhook gyaruWebhook;

        public LineController(INagiyuWebhook nagiyuWebhook, IGyaruWebhook gyaruWebhook)
        {
            this.nagiyuWebhook = nagiyuWebhook;
            this.gyaruWebhook = gyaruWebhook;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage()
        {
            var headers = GetHeaders();
            var requestBody = await GetRequestBody();

            try
            {
                await nagiyuWebhook.HandleWebhookEvent(headers, requestBody);
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SendGyaruMessage()
        {
            var headers = GetHeaders();
            var requestBody = await GetRequestBody();

            try
            {
                await gyaruWebhook.HandleWebhookEvent(headers, requestBody);
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return StatusCode(500);
            }

            return Ok();
        }

        private Dictionary<string, StringValues> GetHeaders()
        {
            var headers = Request.Headers;
            return headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private async Task<string> GetRequestBody()
        {
            using var reader = new StreamReader(Request.Body);
            return await reader.ReadToEndAsync();
        }
    }
}
