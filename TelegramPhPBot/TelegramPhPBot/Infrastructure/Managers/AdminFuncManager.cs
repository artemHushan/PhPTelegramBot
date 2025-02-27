using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Infrastructure.Models;
using TelegramPhPBot.Infrastructure.Services;

namespace TelegramPhPBot.Infrastructure.Managers
{
    public class AdminFuncManager
    {
        public async Task<List<PhPScript>> GetLast10Scripts()
        {
           List<PhPScript> scripts = await _adminService.GetLast10Scripts();
            return scripts;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> scripts = await _adminService.GetUsers();
            return scripts;
        }

        public async Task<string> ChangeUserRole(long telegramId, string role)
        {
            APIResponse answer = await _adminService.ChangeUserRole(telegramId, role);
            return answer.Message;
        }
        private readonly AdminService _adminService;

        public AdminFuncManager()
        {
            var httpClient = new HttpClient();
            _adminService = new AdminService(httpClient);
        }
    }
}
