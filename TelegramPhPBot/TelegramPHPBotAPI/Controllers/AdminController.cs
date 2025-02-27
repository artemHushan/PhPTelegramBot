using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramPHPBotAPI.Models;
using TelegramPHPBotAPI.Services;

namespace TelegramPHPBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // Запит для перевірки авторизації користувача
        [HttpGet("lastPHPScripts")]
        public IActionResult GetLast10PhPScripts(long telegramId)
        {
            List<UploadPhPScript> listPhPScripts = _adminService.GetLast10PhPScripts();
            return Ok(listPhPScripts);
        }

        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            List<ResponseUser> users = _adminService.GetUserList();
            return Ok(users);
        }

        [HttpPost("changeRole/{telegramId}")]
        public IActionResult ChangeUserRole(long telegramId,[FromBody] string newRole)
        {
            var response = _adminService.ChangeUserRole(telegramId, newRole);
            return Ok(response);
        }

        private readonly  AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
    }
}
