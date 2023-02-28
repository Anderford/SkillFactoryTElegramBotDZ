using Telegram.Bot;
using Telegram.Bot.Types;

namespace SkillFactoryTElegramBotDZ.Services
{
    public class MyMethod : IMyMethod
    {
        private readonly ITelegramBotClient _telegramBotClient;
        public MyMethod(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public void Process(string functiontype, Message message, CancellationToken ct)
        {
            if (functiontype == "Word")
            {
                _telegramBotClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
            }
            else if (functiontype == "Number")
            {
                try
                {
                    int[] values = message.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                    int sum = values.Sum();
                    _telegramBotClient.SendTextMessageAsync(message.From.Id, $"Сумма чисел: {sum}", cancellationToken: ct);
                }
                catch (Exception ex) when (ex is FormatException)
                {
                    _telegramBotClient.SendTextMessageAsync(message.From.Id, $"Введите числа через пробел", cancellationToken: ct);
                }
            }
        }
    }
}
