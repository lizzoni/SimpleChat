using SimpleChat.Server.IoC.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddSimpleChatConfiguration(configuration);

var app = builder.Build();
app.UseSimpleChatConfiguration();
app.Run();

