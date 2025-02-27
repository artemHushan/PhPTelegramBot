﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;

namespace TelegramPhPBot.Core.Handlers
{
    public class AddAppNameHandler:IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/addAppName", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            Console.WriteLine($"[INFO] Запит від {telegramId}: {message}");

            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                if (userRole != "Guest")
                {
                    Console.WriteLine($"[INFO] Додавання AppName для {telegramId}");
                    return await _scriptManager.AddAppName(GetAppName(message), telegramId);
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
        private string GetAppName(string message)
        {
            string trimmedMessage = message.TrimStart(); // Видаляємо зайві пробіли на початку
            return trimmedMessage.Substring(11).TrimStart(); // Видаляємо "/auth" і пробіли після нього
        }

        private readonly PHPScriptManager _scriptManager;
        private readonly UserSessionManager _sessionManager;

        public AddAppNameHandler(PHPScriptManager scriptManager, UserSessionManager sessionManager)
        {
            _scriptManager = scriptManager;
            _sessionManager = sessionManager;
        }
    }
}
