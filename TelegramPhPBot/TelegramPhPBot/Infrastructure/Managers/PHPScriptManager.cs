using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Infrastructure.Models;
using TelegramPhPBot.Infrastructure.Services;

namespace TelegramPhPBot.Infrastructure.Managers
{
    public class PHPScriptManager
    {
        public async Task<string> AddAppName(string appName, long telegramId)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _phpApiService.AddAppName(telegramId, appName);
            return apiResponse.Message;
        }

        public async Task<string> AddAppBundle(string appBundle, long telegramId)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _phpApiService.AddAppBundle(telegramId, appBundle);
            return apiResponse.Message;
        }

        public async Task<APISecretResponse> GenerateSecretUser(long telegramId)
        {
            APISecretResponse apiResponse = new APISecretResponse();
            apiResponse = await _phpApiService.GenerateSecretUser(telegramId);
            return apiResponse;
        }

        public async Task<string> AddServerCredits(long telegramId, ServerCredits serverCredits)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _phpApiService.AddServerCredits(telegramId, serverCredits);
            return apiResponse.Message;
        }

        public async Task<string> UploadScript(long telegramId, string directoryPath)
        {
            APIResponse apiResponse = new APIResponse();
            apiResponse = await _phpApiService.UploadScript(telegramId, directoryPath);
            return apiResponse.Message;
        }
        private readonly PHPApiService _phpApiService;

        public PHPScriptManager()
        {
            var httpClient = new HttpClient();
            _phpApiService = new PHPApiService(httpClient);
        }
    }
}
