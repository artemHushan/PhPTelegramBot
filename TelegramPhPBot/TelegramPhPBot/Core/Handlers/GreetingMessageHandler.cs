using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;

namespace TelegramPhPBot.Core.Handlers
{
    public class GreetingMessageHandler : IMessageHandler
    {
        public bool CanHandle(string message) => message.ToLower().Contains("привіт");

        public async Task<string> Handle(string message,long telegramId)
        {
            Console.WriteLine(telegramId);
            return "Привіт, я ваш бот для створення PHP скриптів";
        }
    }
}
