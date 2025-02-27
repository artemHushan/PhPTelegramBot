using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;

namespace TelegramPhPBot.Core.Handlers
{
    public  class AuthMessageHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/auth", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            Console.WriteLine($"[INFO] Запит авторизації від {telegramId}: {message}");

            if (await _sessionManager.IsAuthorized(telegramId))
            {
                Console.WriteLine($"[INFO] Користувач {telegramId} вже авторизований");
                return ($"Ви вже авторизовані, ваша роль: {_sessionManager.CheckUserRole(telegramId)} \n" +
                        "Для переходу на інший акаунт вийдіть з цього за допомогою команди \n" +
                        "\"📌 `/logout`");
            }
            else
            {
                Console.WriteLine($"[INFO] Авторизація користувача {telegramId}");
                string answer = await _sessionManager.AuthorizeUser(telegramId, GetUserPassword(message));
                return answer;
            }
        }


        private string GetUserPassword(string message)
        {
            string trimmedMessage = message.TrimStart(); // Видаляємо зайві пробіли на початку
            return trimmedMessage.Substring(5).TrimStart(); // Видаляємо "/auth" і пробіли після нього
        }

        private readonly UserSessionManager _sessionManager;

        public AuthMessageHandler(UserSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
    }
}
