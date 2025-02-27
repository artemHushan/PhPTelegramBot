using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramPHPBotAPI.Models;
using TelegramPHPBotAPI.Services;

namespace TelegramPHPBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PHPScriptController : ControllerBase
    {
        // Запит для зміни або додавання AppName
        [HttpPost("addAppName/{telegramId}")]
        public IActionResult AddAppName(long telegramId, [FromBody] string appName)
        {
            var response = _phpScriptService.AddAppName(appName, telegramId);
            return Ok(response);
        }

        // Запит для зміни або додавання AppBundle
        [HttpPost("addAppBundle/{telegramId}")]
        public IActionResult AddAppBundle(long telegramId, [FromBody] string appBundle)
        {
            var response = _phpScriptService.AddAppBundle(appBundle, telegramId);
            return Ok(response);
        }

        // Запит для генерації Secret і SecretKeyParam
        [HttpPost("generateSecret/{telegramId}")]
        public IActionResult GenerateSecret(long telegramId)
        {
            var response = _phpScriptService.GenerateSecretKeyAndSecretKeyParam(telegramId);
            return Ok(response);
        }

        // Запит для додавання або зміни даних серверу
        [HttpPost("addServerCredits/{telegramId}")]
        public IActionResult AddServerCredits(long telegramId, [FromBody] ServerCreditsRequest request)
        {
            var response = _phpScriptService.AddServerCredits(request, telegramId);
            return Ok(response);
        }

        [HttpPost("upload/{telegramId}")]
        public async Task<IActionResult> UploadPhpScriptAsync(long telegramId, [FromBody] string directoryPath)
        {
            try
            {
                // Викликаємо сервіс для завантаження PHP скрипта
                Response response =await _uploadPhpScriptService.UploadPhpScript(telegramId, directoryPath);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    Success = false,
                    Message = $"Сталася помилка при завантаженні скрипта: {ex.Message}"
                });
            }
        }
        private readonly PHPScriptService _phpScriptService;
        private readonly UploadPHPScriptService _uploadPhpScriptService;

        public PHPScriptController(PHPScriptService phpScriptService, UploadPHPScriptService uploadPhpScriptService)
        {
            _phpScriptService = phpScriptService;
            _uploadPhpScriptService = uploadPhpScriptService;
        }
    }
}
