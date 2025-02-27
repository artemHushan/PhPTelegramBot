using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;

namespace TelegramPhPBot.Core.Handlers
{
    public class UploadScriptHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/upload", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            if (await _userSessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _userSessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Користувач {telegramId} має роль: {userRole}");

                if (userRole != "Guest")
                {
                    string directoryPath = GetDirectoryPath(message);
                    Console.WriteLine($"[INFO] Користувач {telegramId} завантажує скрипт з шляху: {directoryPath}");

                    string answer = await _phpScriptManager.UploadScript(telegramId, directoryPath);
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
        private string GetDirectoryPath(string message)
        {
            string TrimmedMessage = message.TrimStart(); 
            return TrimmedMessage.Substring(7).TrimStart(); 
        }
        private readonly PHPScriptManager _phpScriptManager;
        private readonly UserSessionManager _userSessionManager;
        public UploadScriptHandler(PHPScriptManager phpScriptManager, UserSessionManager userSessionManager)
        {
            _phpScriptManager = phpScriptManager;
            _userSessionManager = userSessionManager;
        }
    }
}
