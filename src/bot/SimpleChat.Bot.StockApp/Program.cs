using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleChat.Bot.Domain.Interfaces;
using SimpleChat.Bot.IoC.Configurations;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddBotConfiguration()
    );

var app = builder.Build();
app.UseBotConfiguration();
app.Run();
