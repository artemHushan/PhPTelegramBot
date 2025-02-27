using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramPhPBot.Infrastructure.Models
{
    public class PhPScript
    {
        public int PhpScriptId { get; set; }

       
        public int UserId { get; set; }

        
        public string AppName { get; set; }

        
        public string AppBundle { get; set; }

        
        public string Secret { get; set; }

      
        public string SecretKeyParam { get; set; }

        
        public string Host { get; set; }

        
        public string Login { get; set; }

        
        public string Password { get; set; }
    }
}
