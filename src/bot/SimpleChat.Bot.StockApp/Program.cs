using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Core.Domain.Models;

Console.WriteLine("I'm a BOT!");

var userLogin = new UserLogin
{
    Email = "teste@teste.com",
    Password = "@Teste123"
};

var client = new HttpClient();

Console.WriteLine("Logging in...");
var result = await client.PostAsJsonAsync("https://localhost:7299/api/auth/login", userLogin);
var response = await result.Content.ReadFromJsonAsync<UserLoginResponse>();
Console.WriteLine("Token: " + response.Token);

var accessToken = response.Token;
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7299/chathub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(accessToken.Replace("\"", ""))!;
    })
    .Build();

const string roomName = "Minha Sala";

connection.On<string, string>(roomName, (user, message) => { Console.WriteLine($"{roomName}-{user}: {message}"); });

Console.WriteLine("Starting connection...");
await connection.StartAsync();

Console.ReadLine();
await connection.SendAsync("SendMessage", roomName, "Hello!");
//await connection.InvokeAsync<object>("Texto", roomName, "Bot", "Hello!");
Console.ReadLine();

Console.WriteLine("Stopping connection...");
await connection.StopAsync();
