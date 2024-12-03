using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

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
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            try
            {
                await nagiyuWebhook.HandleWebhookEvent(requestBody);
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
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            try
            {
                await gyaruWebhook.HandleWebhookEvent(requestBody);
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
