namespace TelegramStickerPorter;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter();

        services.AddCorsAccessor();

        services.AddControllers()
                .AddInjectWithUnifyResult();
        services.AddHttpRemote();
        services.AddSchedule(options =>
        {
            options.LogEnabled = true;
            options.AddJob(App.EffectiveTypes.ScanToBuilders());
        });
        services.AddTelegram();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseInject(string.Empty);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}