using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Infrastructure.UnitTests.Mocks;

public static class LoginSettingsMock
{
    public static LoginSettings Get()
    {
        return new LoginSettings
        {
            Audience = "audience",
            Expires = 36,
            Issuer = "issuer",
            Secret = "!@OIKSQ(*!NHJSD*&#@$KLJ!"
        };
    }
}
