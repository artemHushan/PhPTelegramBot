using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TelegramPHPBotAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }

        [Required]
        public long telegram_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string password { get; set; }

        [Required]
        [MaxLength(50)]
        public string role { get; set; }

        [Required]
        public bool IsAuthorized { get; set; } = false;
        public User() 
        {

        }
    }
}
