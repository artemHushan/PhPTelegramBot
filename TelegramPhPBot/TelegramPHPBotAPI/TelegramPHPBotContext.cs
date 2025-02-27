using Microsoft.EntityFrameworkCore;
using TelegramPHPBotAPI.Models;

namespace TelegramPHPBotAPI
{
    public class TelegramPHPBotContext : DbContext
    {
        public TelegramPHPBotContext(DbContextOptions<TelegramPHPBotContext> options) : base(options)
        {

        }
        public DbSet <User> users { get; set; }
        public DbSet<CurrentPhpScript> current_php_scripts { get; set; }
        public DbSet<ServerCredit> server_credits { get; set; }
        public DbSet<UploadPhPScript> php_scripts { get; set; }
    }
}
