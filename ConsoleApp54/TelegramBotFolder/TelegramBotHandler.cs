using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp54.TelegramBotFolder
{
    public class TelegramBotHandler
    {
        public string Token { get; set; }
        public TelegramBotHandler(string  token) { 
           this.Token = token;
        }


        public async Task BotHandler()
        {
            var botClient = new TelegramBotClient($"{this.Token}");

            using CancellationTokenSource cts = new();


            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }

        public  async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (messageText == "dotnet")
            {
                // Echo received message text
                Message Sendmessage = await botClient.SendTextMessageAsync(
          chatId: chatId,
          text: "Trying *all the parameters* of `sendMessage` method",
          parseMode: ParseMode.MarkdownV2,
          disableNotification: true,
          replyToMessageId: update.Message.MessageId,
         
          cancellationToken: cancellationToken);
            }else if (messageText == "salom")
            {
                // Echo received message text
                Message Sendmessage = await botClient.SendTextMessageAsync(
          chatId: chatId,
          text: "Trying *all the parameters* of `sendMessage` method",
          parseMode: ParseMode.MarkdownV2,
          disableNotification: true,
          replyToMessageId: update.Message.MessageId,
        
          cancellationToken: cancellationToken);
            }else if(messageText == "kurs")
            {

                // using Telegram.Bot.Types.ReplyMarkups;

                InlineKeyboardMarkup inlineKeyboard = new(new[]
                {
    // first row
    new []
    {
        InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "11"),
        InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "12"),
    },
    // second row
    new []
    {
        InlineKeyboardButton.WithCallbackData(text: "2.1", callbackData: "21"),
        InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22"),
    },
});

            

                // Echo received message text
                Message Sendmessage = await botClient.SendTextMessageAsync(
          chatId: chatId,
          text: "Trying *all the parameters* of `sendMessage` method",
          parseMode: ParseMode.MarkdownV2,
          disableNotification: true,
          replyToMessageId: update.Message.MessageId,
          replyMarkup: inlineKeyboard,
          cancellationToken: cancellationToken);
            }

        }


        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}
