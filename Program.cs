using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using SkillFactoryTElegramBotDZ.Configuration;
using SkillFactoryTElegramBotDZ.Controllers;
using SkillFactoryTElegramBotDZ.Services;

namespace SkillFactoryTElegramBotDZ
{
    class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }
        static AppSetting BuildAppSettings()
        {
            return new AppSetting()
            {
               
                BotToken = "5901844586:AAE5GrPS09W5DpA5jIOg0i255kZlOGv07Yk",
            };
        }
        static void ConfigureServices(IServiceCollection services)
        {
            AppSetting appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
            services.AddSingleton<IMyMethod, MyMethod>();
        }
    }
    
}