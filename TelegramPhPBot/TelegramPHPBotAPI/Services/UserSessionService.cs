using Microsoft.EntityFrameworkCore;
using TelegramPHPBotAPI.Models;

namespace TelegramPHPBotAPI.Services
{
    public class UserSessionService
    {
        public bool IsAuthorized(long telegramId)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
            return user?.IsAuthorized ?? false;
        }

        public Response AuthorizeUser(long telegramId, string userPassword)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);

            if (user != null)
            {
                if (user.IsAuthorized)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Ви вже авторизовані!"
                    };
                }

                if (user.password == userPassword)
                {
                    user.IsAuthorized = true;
                    _context.SaveChanges();
                    return new Response
                    {
                        Success = true,
                        Message = "Вітаємо! Ви успішно авторизовані!"
                    };
                }
                else
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Невірний пароль!"
                    };
                }
            }
            else
            {
                // Створення нового користувача
                var newUser = new User
                {
                    telegram_id = telegramId,
                    password = userPassword,
                    IsAuthorized = true,
                    role = "User"
                };

                _context.users.Add(newUser);

                _context.SaveChanges();

                var new_user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
                var newCurrentScript = new CurrentPhpScript
                {
                    user_id = new_user.user_id
                };

                var newServerCredits = new ServerCredit
                {
                    user_id = new_user.user_id
                };

                _context.server_credits.Add(newServerCredits);

                _context.current_php_scripts.Add(newCurrentScript);

                _context.SaveChanges();


                return new Response
                {
                    Success = true,
                    Message = "Вітаємо! Ви успішно зареєстровані та авторизовані!"
                };
            }
        }

        public Response LogoutUser(long telegramId)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);

            if (user == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Користувача з таким Telegram ID не знайдено."
                };
            }

            if (!user.IsAuthorized)
            {
                return new Response
                {
                    Success = false,
                    Message = "Ви вже не авторизовані!"
                };
            }

            user.IsAuthorized = false;
            _context.SaveChanges();

            return new Response
            {
                Success = true,
                Message = "Вітаємо! Ви успішно вийшли з аккаунту."
            };
        }

        public Response GetUserRole(long telegramId)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);

            if (user == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Користувача з таким Telegram ID не знайдено."
                };
            }

            return new Response
            {
                Success = true,
                Message = user.role
            };
        }

        private readonly TelegramPHPBotContext _context;
     
        public UserSessionService(TelegramPHPBotContext context)
        {
            _context = context;
        }
    }
}
