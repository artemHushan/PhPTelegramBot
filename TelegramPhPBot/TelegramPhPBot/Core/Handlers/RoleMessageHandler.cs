using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;

namespace TelegramPhPBot.Core.Handlers
{
    public class RoleMessageHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/role", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            string answer;

            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Користувач {telegramId} має роль: {userRole}");
                answer = "Ваша роль: " + userRole;
            }
            else
            {
                Console.WriteLine($"[WARNING] Користувач {telegramId} не авторизований");
                answer = "Ваша роль: Гість. Для початку роботи авторизуйтесь";
            }

            return answer;
        }


        private readonly UserSessionManager _sessionManager;

        public RoleMessageHandler(UserSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
    }
}
