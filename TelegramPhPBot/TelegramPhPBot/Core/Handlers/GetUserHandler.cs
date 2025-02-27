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
    public class GetUserHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/getUsers", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            string answer = "";
            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Користувач {telegramId} має роль: {userRole}");

                if (userRole == "Admin")
                {
                    Console.WriteLine($"[INFO] Адмін {telegramId} отримує список користувачів");
                    List<User> users = await _adminManager.GetUsers();
                    answer = FormatUserListAsTable(users);
                }
                else
                {
                    Console.WriteLine($"[WARNING] Доступ заборонено для {telegramId}, роль: {userRole}");
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

        public string FormatUserListAsTable(List<User> users)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Телеграм ID      | Роль");
            sb.AppendLine("-----------------|------------");

            foreach (var user in users)
            {
                sb.AppendLine($"{user.TelegramId,-15} | {user.Role}");
            }

            return sb.ToString();
        }

        private readonly AdminFuncManager _adminManager;

        private readonly UserSessionManager _sessionManager;
        public GetUserHandler(AdminFuncManager adminManager, UserSessionManager sessionManager)
        {
            _adminManager = adminManager;
            _sessionManager = sessionManager;
        }
    }
}
