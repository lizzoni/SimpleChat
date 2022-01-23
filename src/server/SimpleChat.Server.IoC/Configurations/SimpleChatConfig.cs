using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimpleChat.Server.IoC.Configurations;

public static class SimpleChatConfig
{
    public static IServiceCollection AddSimpleChatConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityConfiguration(configuration);
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddMessageBrokerConfiguration();
        services.RegisterServices();

        return services;
    }

    public static IApplicationBuilder UseSimpleChatConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();
        app.UseIdentityConfiguration();
        app.MapControllers();
        app.UseMessageBrokerConfiguration();
        app.MapFallbackToFile("index.html");
        return app;
    }
}
