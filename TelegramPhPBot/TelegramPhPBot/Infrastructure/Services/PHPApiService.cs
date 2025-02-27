using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramPhPBot.Infrastructure.Models;

namespace TelegramPhPBot.Infrastructure.Services
{
    public class PHPApiService
    {

        public async Task<APIResponse> AddAppName(long telegramId, string appName)
        {
            string url = $"http://localhost:5012/api/phpscript/addAppName/{telegramId}";
            var content = new StringContent(JsonSerializer.Serialize(appName), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<APIResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return new APIResponse { Success = false, Message = "Помилка підключення до сервера" };
            }
        }

        public async Task<APIResponse> AddAppBundle(long telegramId, string appDundle)
        {
            string url = $"http://localhost:5012/api/phpscript/addAppBundle/{telegramId}";
            var content = new StringContent(JsonSerializer.Serialize(appDundle), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<APIResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return new APIResponse { Success = false, Message = "Помилка підключення до сервера" };
            }
        }

        public async Task<APISecretResponse> GenerateSecretUser(long telegramId)
        {
            string url = $"http://localhost:5012/api/phpscript/generateSecret/{telegramId}";
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, null);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<APISecretResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return new APISecretResponse { Success = false, Message = "Помилка підключення до сервера" };
            }
        }

        public async Task<APIResponse> AddServerCredits(long telegramId, ServerCredits credits)
        {
            string url = $"http://localhost:5012/api/phpscript/addServerCredits/{telegramId}";
            var content = new StringContent(JsonSerializer.Serialize(credits), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<APIResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return new APIResponse { Success = false, Message = "Помилка підключення до сервера" };
            }
        }

        public async Task<APIResponse> UploadScript(long telegramId, string directoryPath)
        {
            string url = $"http://localhost:5012/api/phpscript/upload/{telegramId}";
            var content = new StringContent(JsonSerializer.Serialize(directoryPath), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<APIResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return new APIResponse { Success = false, Message = "Помилка підключення до сервера" };
            }
        }

        private readonly HttpClient _httpClient;

        public PHPApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
