using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramPhPBot.Infrastructure.Models
{
    internal class AuthResponse
    {
        public bool Success { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
