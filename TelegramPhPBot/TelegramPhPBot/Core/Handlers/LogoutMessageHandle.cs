using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;

namespace TelegramPhPBot.Core.Handlers
{
    public class LogoutMessageHandle:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/logout", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            Console.WriteLine($"[INFO] Користувач {telegramId} намагається вийти з системи");

            string answer = await _sessionManager.LogoutUser(telegramId);

            Console.WriteLine($"[INFO] Користувач {telegramId} успішно вийшов з системи");

            return answer;
        }

        private readonly UserSessionManager _sessionManager;

        public LogoutMessageHandle(UserSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
    }
}
