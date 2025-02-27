using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramPhPBot.Core.UseCase;
using TelegramPhPBot.Infrastructure.Services;
using TelegramPhPBot.Infrastructure.Managers;
namespace TelegramPhPBot.Presentation
{
    public class TelegramBot
    {
        private readonly TelegramBotService _botService;

        public TelegramBot(IConfiguration configuration)
        {
            var token = configuration["TelegramBotToken"];
            var handleMessageUseCase = new HandleMessageUseCase( new UserSessionManager() , new PHPScriptManager(), new AdminFuncManager());
            _botService = new TelegramBotService(token, handleMessageUseCase);
        }

        public void Start()
        {
            _botService.Start();
        }
    }
}
