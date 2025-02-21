using IRON_PROGRAMMER_BOT_ConsoleApp.Configuration;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_webhook.Controllers
{
    [ApiController]
    public class BotController : Controller
    {
        [HttpPost(BotConfiguration.UpdateRoute)]
        public IActionResult Post([FromBody] Update update)
        {
            return View();
        }
    }
}
