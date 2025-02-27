using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramPhPBot.Infrastructure.Models;

namespace TelegramPhPBot.Infrastructure.Services
{
    public class AdminService
    {
        public async Task<List<PhPScript>> GetLast10Scripts()
        {
            string url = $"http://localhost:5012/api/admin/lastPHPScripts";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<PhPScript>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return null;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            string url = $"http://localhost:5012/api/admin/getUsers";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<User>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return null;
            }
        }

        public async Task<APIResponse> ChangeUserRole(long telegramId, string newRole)
        {
            string url = $"http://localhost:5012/api/admin/changeRole/{telegramId}";
            var content = new StringContent(JsonSerializer.Serialize(newRole), Encoding.UTF8, "application/json");

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

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
