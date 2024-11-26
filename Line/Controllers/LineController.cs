using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace Line.Controllers
{
    public class LineController : Controller
    {
        private readonly string channelAccessToken;

        public LineController(IOptions<LineSettings> options)
        {
            channelAccessToken = options.Value.ChannelAccessToken;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userId, string message)
        {
            var url = "https://api.line.me/v2/bot/message/push";
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);

            var payload = new
            {
                to = userId,
                messages = new[]
                {
                    new
                    {
                        type = "text",
                        text = message
                    }
                }
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return Ok("メッセージが送信されました。");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return BadRequest($"エラーが発生しました: {errorMessage}");
            }
        }
    }
}
