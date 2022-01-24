using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Application.Services;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Repository.Notifications;
using SimpleChat.Server.Repository.Repositories;

namespace SimpleChat.Server.IoC.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IRoomMessageRepository, RoomMessageRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IMessageService, MessageService>();
        return services;
    }
}
