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
    public class GetScriptsHandler: IMessageHandler
    {
        public bool CanHandle(string message) => message.TrimStart().StartsWith("/getLastScript", StringComparison.OrdinalIgnoreCase);

        public async Task<string> Handle(string message, long telegramId)
        {
            string answer;
            if (await _sessionManager.IsAuthorized(telegramId))
            {
                string userRole = await _sessionManager.CheckUserRole(telegramId);
                Console.WriteLine($"[INFO] Користувач {telegramId} має роль: {userRole}");

                if (userRole == "Admin")
                {
                    Console.WriteLine($"[INFO] Адмін {telegramId} отримує останні 10 скриптів");
                    List<PhPScript> phPScripts = await _adminManager.GetLast10Scripts();
                    answer = ToJsonFormattedString(phPScripts);
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
        public string ToJsonFormattedString(List<PhPScript> scripts)
        {
            var sb = new StringBuilder();

            foreach (var script in scripts)
            {
                sb.AppendLine("{");
                sb.AppendLine($"  \"php_script_id\": {script.PhpScriptId},");
                sb.AppendLine($"  \"user_id\": {script.UserId},");
                sb.AppendLine($"  \"app_name\": \"{script.AppName}\",");
                sb.AppendLine($"  \"app_bundle\": \"{script.AppBundle}\",");
                sb.AppendLine($"  \"secret\": \"{script.Secret}\",");
                sb.AppendLine($"  \"secret_key_param\": \"{script.SecretKeyParam}\",");
                sb.AppendLine($"  \"host\": \"{script.Host}\",");
                sb.AppendLine($"  \"login\": \"{script.Login}\",");
                sb.AppendLine($"  \"password\": \"{script.Password}\"");
                sb.AppendLine("},");
            }

            // Видалити останню кому
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 3, 2); // Видаляємо останню кому і пробіл
            }

            return "[" + sb.ToString() + "]";
        }


        private readonly AdminFuncManager _adminManager;
        private readonly UserSessionManager _sessionManager;
        public GetScriptsHandler(AdminFuncManager adminManager, UserSessionManager sessionManager)
        {
            _adminManager = adminManager;
            _sessionManager = sessionManager;
        }
    }
}
