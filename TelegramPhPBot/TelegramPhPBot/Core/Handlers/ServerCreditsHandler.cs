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
    public class ServerCreditsHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/uploadServer", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Користувач {telegramId} має роль: {userRole}");

                if (userRole != "Guest")
                {
                    Console.WriteLine($"[INFO] Користувач {telegramId} додає серверні кредити");
                    string answer = await _scriptManager.AddServerCredits(telegramId, GetServerCredits(message));
                    return answer;
                }
                else
                {
                    Console.WriteLine($"[WARNING] Доступ заборонено для {telegramId}, роль: Guest");
                    return "Ваша роль: Guest. Цей функціонал недоступний для вашої ролі";
                }
            }
            else
            {
                Console.WriteLine($"[WARNING] Неавторизований доступ від {telegramId}");
                return "Ви не авторизований, будь ласка авторизуйтесь за допомогою команди: '/auth' {ваш пароль}";
            }
        }


        private ServerCredits GetServerCredits(string message)
        {
            var commandParts = message.Split(' ');

            ServerCredits credits = new ServerCredits();
           
            if (commandParts.Length == 4)
            {
                string host = commandParts[1];
                string login = commandParts[2];
                string password = commandParts[3];
                credits.Host = host;
                credits.Password = password;
                credits.Login = login;
            }
            return credits;
        }

        private readonly PHPScriptManager _scriptManager;
        private readonly UserSessionManager _sessionManager;
        public ServerCreditsHandler(PHPScriptManager scriptManager, UserSessionManager sessionManager)
        {
            _scriptManager = scriptManager;
            _sessionManager = sessionManager;
        }
    }
}
