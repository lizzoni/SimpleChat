using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleChat.Bot.Application.Interfaces;
using SimpleChat.Bot.Application.Services;
using SimpleChat.Bot.Domain.Configurations;
using SimpleChat.Bot.Domain.Interfaces;
using SimpleChat.Bot.Infrastructure.Repositories;

namespace SimpleChat.Bot.IoC.Configurations;

public static class BotConfig
{
    public static IServiceCollection AddBotConfiguration(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddEnvironmentVariables()
            .Build();

        services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            })
            .Configure<LoginSettings>(configuration.GetSection("LoginSettings"))
            .AddScoped<HttpClient>()
            .AddScoped<IDownloadCsv, DownloadCsv>()
            .AddScoped<ILoginRepository, LoginRepository>()
            .AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<IMessageBroker, MessageBroker>()
            .AddScoped<IStooqService, StooqService>()
            .AddScoped<ICommandService, StooqCommandService>()
            .AddScoped<IBot, Application.Bot>();

        return services;
    }

    public static IHost UseBotConfiguration(this IHost app)
    {
        app.Services
            .GetService<IBot>()
            ?.Start();
        return app;
    }
}
