﻿using IRON_PROGRAMMER_BOT_Common.Configuration;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Webhook.Controllers
{
    [ApiController]
    public class BotController(IUpdateHandler updateHandler, ITelegramBotClient botClient) : Controller
    {
        [HttpPost(BotConfiguration.UpdateRoute)]
        public async Task<IActionResult> PostAsync([FromBody] Update update)
        {
            try
            {
                await updateHandler.HandleUpdateAsync(botClient, update, CancellationToken.None);
            }
            catch (Exception ex)
            {
                await updateHandler.HandlePollingErrorAsync(botClient, ex, CancellationToken.None);
            }
            return Ok();
        }

        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}
