/*****************************
 * Project for C# Telegram Bot
 *****************************/

using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot_API;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Запущен бот " + Bot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        Bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
    
    private static readonly ITelegramBotClient Bot = new TelegramBotClient("5760458922:AAGTY-GgaoysGehTt0LrHY1qz-Jg5R7i46A");
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    private static object? Update => null; // Инкапсулирование Update
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message?.Text != null && message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Приветствую тебя, дорогой _user!" +
                                                                   "\nДанный бот поможет тебе с какчественным перерводом" +
                                                                   "\nДля дальнейших действий прошу ввести команду </help>", cancellationToken: cancellationToken);
                return;
            }

            var firstMessage = update.Message;
            if (firstMessage?.Text != null && firstMessage.Text.ToLower() == "/help")
            {
                if (message != null)
                    await botClient.SendTextMessageAsync(message.Chat, "Вы обратились за помощью." +
                                                                       "\nВам доступен следующий список команд:" +
                                                                       "\n" +
                                                                       "\n", cancellationToken: cancellationToken);
                return;
            }

            var secondMessage = update.Message;
            if (secondMessage is { Text: "Привет" })
            {
                if (message != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!",
                        cancellationToken: cancellationToken);
                }
            }

            var thirdMessage = update.Message;
            if (thirdMessage is { Text: "Как дела?" })
            {
                if (message != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Нормально, а у тебя как?",
                        cancellationToken: cancellationToken);
                }
            }

            var extraMessages= update.Message;
            if (extraMessages is {Text: "Какой у меня ид?"})
            {
                if (message != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Твой ид {update.Id}",
                        cancellationToken: cancellationToken);   
                }
            }
                
            var thanksMessages= update.Message;
            if (thanksMessages is {Text: "Спасибо"})
            {
                if (message != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Не за что, дружок)",
                        cancellationToken: cancellationToken);   
                }
            }
        }
            
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Update));
        return Task.CompletedTask;
    }
}