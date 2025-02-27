using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramPhPBot.Core.UseCase;

namespace TelegramPhPBot.Infrastructure.Services
{
    internal class TelegramBotService
    {
        public void Start()
        {
            _cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Отримуємо всі оновлення
            };
            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandlePollingErrorAsync,
                receiverOptions,
                _cts.Token
            );

            Console.WriteLine("✅ Бот запущено...");
        }

        public void Stop()
        {
            _cts.Cancel();
            Console.WriteLine("⛔ Бот зупинено.");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message?.Text is not null)
            {
                long telegramId = update.Message.Chat.Id;
                string response = await _handleMessageUseCase.HandleMessage(update.Message.Text, telegramId);
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, response, cancellationToken: cancellationToken);
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"⚠ Помилка: {exception.Message}");
            return Task.CompletedTask;
        }

        private readonly ITelegramBotClient _botClient;

        private readonly HandleMessageUseCase _handleMessageUseCase;

        private CancellationTokenSource _cts;

        public TelegramBotService(string token, HandleMessageUseCase handleMessageUseCase)
        {
            _botClient = new TelegramBotClient(token);
            _handleMessageUseCase = handleMessageUseCase;
        }
    }
}
