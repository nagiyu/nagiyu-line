using System.Threading.Tasks;
using CommonKit.Services;
using Microsoft.AspNetCore.Mvc;

namespace Line.Controllers
{
    public class TestController : Controller
    {
        private readonly LogService logService;

        public TestController(LogService logService)
        {
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
    }
}
