using Microsoft.EntityFrameworkCore;
using SimpleChat.Server.Repository.Data;

namespace SimpleChat.Server.Infrastructure.UnitTests.Mocks;

public static class ApplicationDbContextMock
{
    public static ApplicationDbContext Get()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb") 
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
