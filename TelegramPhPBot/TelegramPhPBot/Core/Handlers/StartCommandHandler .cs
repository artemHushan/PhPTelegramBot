using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;

namespace TelegramPhPBot.Core.Handlers
{
        public class StartCommandHandler : INonPersonalizedMessageHandler
        {
            public bool CanHandle(string message) => message.Trim().Equals("/start", StringComparison.OrdinalIgnoreCase);

            public string Handle(string message)
            {
            return "👋 Вітаю! Я ваш бот для завантаження PHP-скриптів на віддалений сервер через SFTP.\n\n" +
           "Щоб почати роботу, вам потрібно авторизуватися. Напишіть команду:\n" +
           "📌 `/auth ваш_пароль`\n\n" +
           "Якщо у вас немає доступу, зверніться до адміністратора.";
        }
        }
}
