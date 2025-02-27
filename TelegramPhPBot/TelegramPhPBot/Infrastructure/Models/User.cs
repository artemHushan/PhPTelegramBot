using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramPhPBot.Infrastructure.Models
{
    public class User
    {
        public long TelegramId { get; set; }

        public string Role { get; set; }
    }
}
