using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TelegramPHPBotAPI.Models;

namespace TelegramPHPBotAPI.Services
{
    public class PHPScriptService
    {
        public Response AddAppName(string appName,long telegramId)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);

            if (user == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Користувача не знайдено"
                };
            }

            var currentScript = _context.current_php_scripts.FirstOrDefault(c => c.user_id == user.user_id);

            if (currentScript == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Не знайдено поточний скрипт для цього користувача."
                };
            }

            // Перевірка на null значення перед присвоєнням
            if (string.IsNullOrEmpty(currentScript.AppName))
            {
                currentScript.AppName = appName;
                _context.SaveChanges();
                return new Response
                {
                    Success = true,
                    Message = $"Вітаємо! Ви успішно задали AppName: {appName}"
                };
            }
            else
            {
                currentScript.AppName = appName;
                _context.SaveChanges();
                return new Response
                {
                    Success = true,
                    Message = $"Вітаємо! Ви змінили AppName на {appName}"
                };
            }

        }

        public Response AddAppBundle(string appBundle, long telegramId)
        {
            // Перевіряємо, чи існує користувач за заданим telegramId
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
            if (user == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Користувача не знайдено."
                };
            }

            // Отримуємо поточний скрипт для цього користувача
            var currentScript = _context.current_php_scripts.FirstOrDefault(c => c.user_id == user.user_id);
            if (currentScript == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Не знайдено поточний скрипт для цього користувача."
                };
            }

            // Перевіряємо, чи поле AppBundle раніше було пустим
            bool wasEmpty = string.IsNullOrEmpty(currentScript.AppBundle);

            // Присвоюємо нове значення
            currentScript.AppBundle = appBundle;
            _context.SaveChanges();

            // Повертаємо відповідний респонс залежно від того, чи було поле пустим
            if (wasEmpty)
            {
                return new Response
                {
                    Success = true,
                    Message = $"Вітаємо! Ви задали AppBundle зі значенням: {appBundle}"
                };
            }
            else
            {
                return new Response
                {
                    Success = true,
                    Message = $"Вітаємо! Ви змінили AppBundle на {appBundle}"
                };
            }
        }


        public SecretResponse GenerateSecretKeyAndSecretKeyParam(long telegramId)
        {
            // Знаходимо користувача за telegramId
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
            if (user == null)
            {
                return new SecretResponse
                {
                    Success = false,
                    Message = "Користувача не знайдено."
                };
            }

            // Знаходимо поточний скрипт для цього користувача
            var currentScript = _context.current_php_scripts.FirstOrDefault(c => c.user_id == user.user_id);
            if (currentScript == null)
            {
                return new SecretResponse
                {
                    Success = false,
                    Message = "Поточний скрипт для користувача не знайдено."
                };
            }

            // Перевіряємо, чи значення вже існували (тобто не null чи пусті)
            bool alreadyGenerated = !string.IsNullOrEmpty(currentScript.Secret) &&
                                    !string.IsNullOrEmpty(currentScript.Secret_Key_Param);

            // Генеруємо нові значення для Secret та Secret_Key_Param
            currentScript.Secret = _secretGeneratorService.GenerateSecret();
            currentScript.Secret_Key_Param = _secretGeneratorService.GenerateSecretKeyParam();

            // Зберігаємо зміни в базі даних
            _context.SaveChanges();

            // Визначаємо повідомлення залежно від того, чи вже були задані значення
            string message = alreadyGenerated
                 ? "Ви перегенерували Secret та SecretKeyParam на нові значення"
                 : "Вітаю, ви успішно згенерували Secret та SecretKeyParam";

            return new SecretResponse
            {
                Secret = currentScript.Secret,
                SecretKeyParam = currentScript.Secret_Key_Param,
                Success = true,
                Message = message
            };
        }


        public Response AddServerCredits(ServerCreditsRequest serverCredits, long telegramId)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
            var currentServerCredits = _context.server_credits.FirstOrDefault(c => c.user_id == user.user_id);
            currentServerCredits.host = serverCredits.Host;
            currentServerCredits.login = serverCredits.Login;
            currentServerCredits.password = serverCredits.Password;
            _context.SaveChanges();
            return new Response
            {
                Success = true,
                Message = "Вітаю ви успішно змінили або додали креди для серверу"
            };
        }
        private readonly TelegramPHPBotContext _context;

        private readonly SecretGeneratorService _secretGeneratorService;

        public PHPScriptService(TelegramPHPBotContext context, SecretGeneratorService secretGeneratorService)
        {
            _context = context;
            _secretGeneratorService = secretGeneratorService;
        }
    }
}
