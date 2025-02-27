using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TelegramPHPBotAPI.Models
{
    public class UploadPhPScript
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int php_script_id { get; set; }

        [Required]
        public int user_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string app_name { get; set; }

        [Required]
        [MaxLength(100)]
        public string app_bundle { get; set; }

        [Required]
        [MaxLength(255)]
        public string secret { get; set; }

        [Required]
        [MaxLength(255)]
        public string secret_key_param { get; set; }

        [Required]
        [MaxLength(255)]
        public string host { get; set; }

        [Required]
        [MaxLength(100)]
        public string login { get; set; }

        [Required]
        [MaxLength(255)]
        public string password { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

        public UploadPhPScript()
        {

        }
    }
}
