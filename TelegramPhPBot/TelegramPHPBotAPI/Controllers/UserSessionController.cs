using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramPHPBotAPI.Models;
using TelegramPHPBotAPI.Services;

namespace TelegramPHPBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionController : ControllerBase
    {
        private readonly UserSessionService _userSessionService;

        // Конструктор для отримання інстансу UserSessionService
        public UserSessionController(UserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        // Запит для перевірки авторизації користувача
        [HttpGet("isAuthorized/{telegramId}")]
        public IActionResult IsAuthorized(long telegramId)
        {
            var isAuthorized = _userSessionService.IsAuthorized(telegramId);
            return Ok(new { success = true, isAuthorized });
        }

        // Запит для авторизації або реєстрації користувача
        [HttpPost("authorize/{telegramId}")]
        public IActionResult Authorize(long telegramId, [FromBody] string password)
        {
            var response = _userSessionService.AuthorizeUser(telegramId, password);
            return Ok(response);
        }

        // Запит для виходу користувача
        [HttpPost("logout/{telegramId}")]
        public IActionResult Logout(long telegramId)
        {
            var response = _userSessionService.LogoutUser(telegramId);
            return Ok(response);
        }

        [HttpGet("role/{telegramId}")]
        public IActionResult GetUserRole(long telegramId)
        {
            var response = _userSessionService.GetUserRole(telegramId);
            if (response.Success)
            {
                return Ok(response);  // Якщо успіх
            }
            else
            {
                return BadRequest(response);  // Якщо помилка
            }
        }
    }
}
