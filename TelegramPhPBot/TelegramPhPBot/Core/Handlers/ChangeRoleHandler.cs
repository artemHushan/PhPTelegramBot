using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;
using TelegramPhPBot.Infrastructure.Models;

namespace TelegramPhPBot.Core.Handlers
{
    internal class ChangeRoleHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/changeRole", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            Console.WriteLine($"[INFO] Запит від {telegramId}: {message}");
            string answer;

            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Роль користувача {telegramId}: {userRole}");

                if (userRole == "Admin")
                {
                    User currentUser = GetUserInfo(message);
                    Console.WriteLine($"[INFO] Адмін {telegramId} змінює роль користувача {currentUser.TelegramId} на {currentUser.Role}");
                    answer = await _adminManager.ChangeUserRole(currentUser.TelegramId, currentUser.Role);
                }
                else
                {
                    Console.WriteLine($"[WARNING] Доступ заборонено для {telegramId}");
                    answer = "Ваша роль: User. Цей функціонал недоступний для вашої ролі";
                }
            }
            else
            {
                Console.WriteLine($"[WARNING] Неавторизований доступ від {telegramId}");
                answer = "Ваша роль: Guest. Для початку роботи авторизуйтесь";
            }
            return answer;
        }

        private User GetUserInfo(string message)
        {
            var commandParts = message.Split(' ');

            User currentUser = new User();

            if (commandParts.Length == 3)
            {
                string telegramId = commandParts[1];
                string role = commandParts[2];
                currentUser.TelegramId = long.Parse(telegramId);
                currentUser.Role = role;
            }
            return currentUser;
        }

        private readonly AdminFuncManager _adminManager;

        private readonly UserSessionManager _sessionManager;
        public ChangeRoleHandler(AdminFuncManager adminManager, UserSessionManager sessionManager)
        {
            _adminManager = adminManager;
            _sessionManager = sessionManager;
        }
    }
}
