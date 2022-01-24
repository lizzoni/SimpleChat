using Microsoft.Extensions.Options;
using SimpleChat.Bot.Application.Interfaces;
using SimpleChat.Bot.Domain.Configurations;
using SimpleChat.Bot.Domain.Interfaces;

namespace SimpleChat.Bot.Application;

public class Bot : IBot
{
    private readonly LoginSettings _loginSettings;
    private readonly ILoginRepository _loginRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ICommandService _commandService;

    public Bot(IOptions<LoginSettings> loginSettings, ILoginRepository loginRepository, IRoomRepository roomRepository, ICommandService commandService)
    {
        _loginSettings = loginSettings.Value;
        _loginRepository = loginRepository;
        _roomRepository = roomRepository;
        _commandService = commandService;
    }

    public async Task Start()
    {
        Console.WriteLine("Setting up bot...");

        string accessToken;
        do
        {
            accessToken = await _loginRepository.GetToken();
            if (accessToken != string.Empty)
                continue;
            Console.WriteLine("Server offline... Retrying in 5 seconds");
            Thread.Sleep(5000);
        } while (accessToken == string.Empty);
        
        var allRooms = await _roomRepository.GetAllRooms();
        var rooms = allRooms.Where(x => _loginSettings.Rooms.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase)).ToList();
        if (rooms == null || rooms.Count == 0)
        {
            Console.WriteLine("No rooms found");
            return;
        }
        foreach (var roomResponse in rooms)
            await _commandService.AddCommand(_loginSettings.Url, accessToken, roomResponse.Id.ToString());
        Console.WriteLine("Bot is ready!");
    }
}
