using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;

namespace TelegramPhPBot.Core.Handlers
{
    internal class AddAppBundleHandler:IMessageHandler
    {
       
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/addAppBundle", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            Console.WriteLine($"[INFO] Запит від {telegramId}: {message}");

            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                if (userRole != "Guest")
                {
                    Console.WriteLine($"[INFO] Додавання AppBundle для {telegramId}");
                    return await _scriptManager.AddAppBundle(GetAppBundle(message), telegramId);
                }
                else
                {
                    Console.WriteLine($"[WARNING] Доступ заборонено для {telegramId}");
                    return "Ваша роль: Guest. Цей Функціонал недоступний для вашої ролі";
                }
            }
            else
            {
                Console.WriteLine($"[WARNING] Неавторизований доступ {telegramId}");
                return "Ви не авторизований, будь ласка авторизуйтесь за допомогою команди: '/auth' {ваш пароль}";
            }
        }



        private string GetAppBundle(string message)
            {
                string trimmedMessage = message.TrimStart(); 

                return trimmedMessage.Substring(13).TrimStart(); 
            }

            private readonly PHPScriptManager _scriptManager;

            private readonly UserSessionManager _sessionManager;

            public AddAppBundleHandler(PHPScriptManager scriptManager, UserSessionManager sessionManager)
            {
                _scriptManager = scriptManager;
                _sessionManager = sessionManager;
            }
        }
    }

