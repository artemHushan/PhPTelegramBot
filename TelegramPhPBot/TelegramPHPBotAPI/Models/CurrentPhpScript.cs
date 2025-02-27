using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TelegramPHPBotAPI.Models
{
    public class CurrentPhpScript
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int script_id { get; set; }

        
        [MaxLength(100)]
        public string? AppName { get; set; }

      
        [MaxLength(100)]
        public string? AppBundle { get; set; }

       
        [MaxLength(255)]
        public string? Secret { get; set; }

        
        [MaxLength(255)]
        public string? Secret_Key_Param { get; set; }

        [Required]
        public int user_id { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }
        public CurrentPhpScript()
        {

        }

    }
}
