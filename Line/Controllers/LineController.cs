using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Common.Utilities;

using LineBotProcessor.Interfaces;

namespace Line.Controllers
{
    public class LineController : Controller
    {
        private readonly IMessageProcessor messageProcessor;

        public LineController(IMessageProcessor messageProcessor)
        {
            this.messageProcessor = messageProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage()
        {
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            try
            {
                await messageProcessor.ProcessMessageAsync(requestBody);
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
                await messageProcessor.ProcessGyaruMessageAsync(requestBody);
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
