using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;

namespace TelegramPhPBot.Core.Handlers
{
    public class DefaultMessageHandler : INonPersonalizedMessageHandler
    {
        public bool CanHandle(string message) => true; // Викликається, якщо жоден інший обробник не спрацював

        public string Handle(string message) => "Не зрозумів вашого повідомлення. 🤔";
    }
}
