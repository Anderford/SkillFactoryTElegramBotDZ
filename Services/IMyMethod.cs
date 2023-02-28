using Telegram.Bot.Types;

namespace SkillFactoryTElegramBotDZ.Services
{
    public interface IMyMethod
    {
        void Process(string param, Message message, CancellationToken ct);
    }
}