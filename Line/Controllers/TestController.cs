using System.Threading.Tasks;
using CommonKit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Line.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly LogService logService;

        public TestController(ILogger<TestController> logger, LogService logService)
        {
            _logger = logger;
            this.logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            try
            {
                await logService.WriteLogAsync("Test Get Method.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error writing log in GetTest method.");
                return StatusCode(500, "Internal server error while writing log.");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostTest()
        {
            try
            {
                await logService.WriteLogAsync("Test Post Method.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error writing log in GetTest method.");
                return StatusCode(500, "Internal server error while writing log.");
            }

            return Ok();
        }
    }
}
