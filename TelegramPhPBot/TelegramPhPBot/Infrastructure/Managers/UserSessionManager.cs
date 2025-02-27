using Microsoft.AspNetCore.Http.Timeouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Infrastructure.Models;
using TelegramPhPBot.Infrastructure.Services;

namespace TelegramPhPBot.Infrastructure.Managers
{
    public class UserSessionManager
    {

        public async Task<bool> IsAuthorized(long telegramId)
        {
            bool isAuthorized = await _apiService.CheckAuthorizationAsync(telegramId);
            return isAuthorized;
        }

        public async Task <string> AuthorizeUser(long telegramId, string userPassword)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _apiService.AuthorizeUserAsync(telegramId, userPassword);
            return apiResponse.Message;
        }

        public async Task<string> LogoutUser(long telegramId)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _apiService.LogoutUser(telegramId);
            return apiResponse.Message;
        }

        public async Task<string> CheckUserRole(long telegramId)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _apiService.CheckUserRole(telegramId);
            return apiResponse.Message;
        }

        private readonly APIService _apiService;

        public UserSessionManager()
        {
             var httpClient = new HttpClient();
            _apiService = new APIService(httpClient);
        }
    }
}
