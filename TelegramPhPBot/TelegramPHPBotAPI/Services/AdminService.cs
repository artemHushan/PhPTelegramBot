using TelegramPHPBotAPI.Models;

namespace TelegramPHPBotAPI.Services
{
    public class AdminService
    {
       public List<UploadPhPScript> GetLast10PhPScripts()
        {
            List<UploadPhPScript> lastTenRecords = _context.php_scripts
                 .OrderByDescending(record => record.php_script_id) // якщо Id є автоінкрементним
                 .Take(10)
                 .ToList();
            return lastTenRecords;
        }
        public List<ResponseUser> GetUserList()
        {
            List<ResponseUser> usersList = new List<ResponseUser>();
            foreach (var user in _context.users)
            {
                usersList.Add(new ResponseUser
                {
                    TelegramId = user.telegram_id,
                    Role = user.role,
                });
            }
            return usersList;
        }

        public Response ChangeUserRole(long telegramId, string newRole)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
                string oldRole = user.role;
                user.role = newRole;
                _context.SaveChanges();
                return new Response
                {
                    Success = true,
                    Message = $"Ви змінили роль користувача з id:{telegramId} з {oldRole} на {user.role}."
                };
          
        }
        private readonly TelegramPHPBotContext _context;
        public AdminService(TelegramPHPBotContext context)
        {
            _context = context;
        }
    }
}
