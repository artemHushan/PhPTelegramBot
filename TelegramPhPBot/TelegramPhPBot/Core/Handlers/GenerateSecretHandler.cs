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
    public class GenerateSecretHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/generate", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Користувач {telegramId} має роль: {userRole}");

                if (userRole != "Guest")
                {
                    Console.WriteLine($"[INFO] Генерація секретних даних для користувача {telegramId}");
                    APISecretResponse response = await _scriptManager.GenerateSecretUser(telegramId);
                    string answer = response.Message + "\n" + $"Secret = {response.Secret}" + "\n" + $"Secret-Key-Param = {response.SecretKeyParam}";
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
                return "Ви не авторизовані, будь ласка авторизуйтесь за допомогою команди: '/auth' {ваш пароль}";
            }
        }



        private readonly PHPScriptManager _scriptManager;
        private readonly UserSessionManager _sessionManager;
        public GenerateSecretHandler(PHPScriptManager scriptManager, UserSessionManager sessionManager)
        {
            _scriptManager = scriptManager;
            _sessionManager = sessionManager;
        }
    }
}
