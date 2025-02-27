using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramPhPBot.Infrastructure.Models
{
    public class APISecretResponse:APIResponse
    {
        public string? Secret { get; set; }
        public string? SecretKeyParam { get; set; }
    }
}
