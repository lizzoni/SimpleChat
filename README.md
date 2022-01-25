# Simple Chat

Simple browser-based chat application using .NET.
This application should allow several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

## Features
- Register Users
- Login/Logout
- Create Rooms
- Send messages in many rooms
- Save messages in database
- Send command (/stock={stock_code})
- Dedcoupled Bot for stock quote

## ToDo
- More unit tests
- Integration tests
- Manager user (edit, remove)
- Manager rooms (edit, remove)
- Make the look more pleasant
 
## Techs
- .NET 6.0
- Authentication JwtBearer
- SignalR (Message Broker)
- Blazor WebAssembly (Frontend)
- SQL Server (Local)
- Entity Framework Core

## External Libraries
- Flunt
- xUnit
- FluentAssertions
- NSubstitute

## Setup

In terminal, enter in solution folder (/src)
```
[SimpleChat\src\]
dotnet restore
```

To create database
```
[SimpleChat\src\server\SimpleChat.Server.BlazorWebHost\]
dotnet ef database update --project ..\SimpleChat.Server.Infrastructure\
```

## Run

![run](https://github.com/lizzoni/SimpleChat/blob/master/images/setup.png?raw=true)

To run SimpleChat
```
[SimpleChat\src\server\SimpleChat.Server.BlazorWebHost\]
dotnet run
```

To run Bot (it´s necessary register an user, and config Bot in appsettings.json file)
```
{
  "LoginSettings": {
    "URL": "https://localhost:7299/",
    "Email": "bot@test.com",
    "Password": "@Test123",
    "Rooms": ["Business"]
  }
}
```

```
[SimpleChat\src\bot\SimpleChat.Server.StockBOt\]
dotnet run
```

## Images
### Login
![Login](https://github.com/lizzoni/SimpleChat/blob/master/images/login.png?raw=true)

### Register
![Register](https://github.com/lizzoni/SimpleChat/blob/master/images/register.png?raw=true)

### Create Room
![Create Room](https://github.com/lizzoni/SimpleChat/blob/master/images/create_room.png?raw=true)

### Room
![Room](https://github.com/lizzoni/SimpleChat/blob/master/images/room.png?raw=true)
