using System.Security.Claims;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Repository.Data;

namespace SimpleChat.Server.Repository.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public string GetUserName(Guid userId)
    {
        return _context.UserClaims.Where(x => x.UserId == userId.ToString() && x.ClaimType == ClaimTypes.Name).Select(x => x.ClaimValue).FirstOrDefault() ?? string.Empty;
    }
}
