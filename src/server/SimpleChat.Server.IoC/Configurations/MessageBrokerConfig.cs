using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Server.Repository.Hubs;

namespace SimpleChat.Server.IoC.Configurations;

public static class MessageBrokerConfig
{
    public static IServiceCollection AddMessageBrokerConfiguration(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    public static IApplicationBuilder UseMessageBrokerConfiguration(this WebApplication app)
    {
        app.MapHub<ChatHub>("/chathub");
        return app;
    }
}
