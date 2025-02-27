using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramPhPBot.Infrastructure.Models;

namespace TelegramPhPBot.Infrastructure.Services
{
    public class APIService
    {
        private readonly HttpClient _httpClient;

        public APIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CheckAuthorizationAsync(long telegramId)
        {
            string url = $"http://localhost:5012/api/userSession/isAuthorized/{telegramId}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AuthResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result?.IsAuthorized ?? false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
                return false;
            }
        }

        public async Task<APIResponse> AuthorizeUserAsync(long telegramId, string password)
        {
            string url = $"http://localhost:5012/api/userSession/authorize/{telegramId}";
            var content = new StringContent(JsonSerializer.Serialize(password), Encoding.UTF8, "application/json");

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
        public async Task<APIResponse> LogoutUser(long telegramId)
        {
            string url = $"http://localhost:5012/api/userSession/logout/{telegramId}";
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, null);
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

        public async Task<APIResponse> CheckUserRole(long telegramId)
        {
            string url = $"http://localhost:5012/api/userSession/role/{telegramId}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
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
    }
    }
