using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using SkillFactoryTElegramBotDZ.Services;

namespace SkillFactoryTElegramBotDZ.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly IMyMethod _myMethod;

        public TextMessageController(ITelegramBotClient telegramBotClient,IStorage memoryStorage, IMyMethod myMethod)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _myMethod = myMethod;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string doMethod = _memoryStorage.GetSession(message.Chat.Id).WorkingModeType; // Здесь получим тип функции из сессии пользователя
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Строки" , $"Word"),
                        InlineKeyboardButton.WithCallbackData($"Числа" , $"Number")
                    });

                    // передаем кнопки вместе с сообщением
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Выберите операцию {Environment.NewLine}"
                        + $"{Environment.NewLine}Узнать количество символов в строке или получить сумму введеных чисел.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    _myMethod.Process(doMethod, message, ct);
                    break;
            }
        }
    }
}
