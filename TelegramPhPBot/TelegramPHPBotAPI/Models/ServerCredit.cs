using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TelegramPHPBotAPI.Models
{
    public class ServerCredit
    {
        [Key]
        public int server_credits_id { get; set; }  // Primary key

     
        [MaxLength(255)]
        public string? host { get; set; }  // Поле для хоста

     
        [MaxLength(255)]
        public string? login { get; set; }  // Поле для логіна

        [MaxLength(255)]
        public string? password { get; set; }  // Поле для пароля

        [ForeignKey("User")]
        public int user_id { get; set; }  // Зовнішній ключ для User

        public virtual User User { get; set; }  // Навігаційне властивість до User

        public ServerCredit()
        {

        }
    }
}
