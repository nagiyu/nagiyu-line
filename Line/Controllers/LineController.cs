using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommonKit.Services;
using LineBridge.Interfaces.Webhook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Line.Controllers
{
    public class LineController : Controller
    {
        private readonly INagiyuWebhook nagiyuWebhook;
        private readonly IGyaruWebhook gyaruWebhook;
        private readonly LogService logService;

        public LineController(INagiyuWebhook nagiyuWebhook, IGyaruWebhook gyaruWebhook, LogService logService)
        {
            this.nagiyuWebhook = nagiyuWebhook;
            this.gyaruWebhook = gyaruWebhook;
            this.logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            await logService.WriteLogAsync("Test Get Method.");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostTest()
        {
            await logService.WriteLogAsync("Test Post Method.");
            return Ok();
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
                await logService.WriteLogAsync(ex.Message);
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
                await logService.WriteLogAsync(ex.Message);
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
