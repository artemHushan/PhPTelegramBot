using Microsoft.Extensions.Configuration;
using TelegramPhPBot.Presentation;

namespace TelegramPhPBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Додає шлях до файлу
                .AddJsonFile("appsettings.json")
                .Build();

            var bot = new TelegramBot(configuration);
            bot.Start();

            System.Console.ReadLine(); // Чекаємо завершення
        }
    }
}