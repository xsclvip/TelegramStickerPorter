namespace TelegramStickerPorter;

public static class AddTelegramServices
{
    public static IServiceCollection AddTelegram(this IServiceCollection services)
    {
        ConfigureWTelegramLogging();
        
        services.AddSingleton<TelegramBotClientManager>();
        services.AddSingleton<TelegramBotBackgroundService>();
        services.AddHostedService(sp => sp.GetRequiredService<TelegramBotBackgroundService>());
        services.AddSingleton<MessageService>();
        services.AddSingleton<StickerService>();

        return services;
    }
    
    private static void ConfigureWTelegramLogging()
    {
        var logDirectory = Path.Combine(AppContext.BaseDirectory, "log");
        Directory.CreateDirectory(logDirectory);

        var logFilePath = Path.Combine(logDirectory, "TelegramBot.log");
        var logWriter = new StreamWriter(logFilePath, true, Encoding.UTF8) { AutoFlush = true };
        
        WTelegram.Helpers.Log = (lvl, str) =>
        {
            var logLevel = "TDIWE!"[lvl];
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            lock (logWriter)
            {
                logWriter.WriteLine($"{timestamp} [{logLevel}] {str}");
            }
        };
    }
}