using ConsoleApp54.TelegramBotFolder;
using Telegram.Bot;


namespace ConsoleApp54;

public class Program
{
    static async Task Main(string[] args)
    {
        const string token = "6963507776:AAGytlWgOC5Y4WYBRp7utDo1StTAlwm8vvc";

        TelegramBotHandler handler = new TelegramBotHandler(token);

        try
        {
            await handler.BotHandler();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
       


    }
}