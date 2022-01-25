using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace SimpleChat.Server.Infrastructure.UnitTests.Mocks;

public static class IdentityMock
{
    public class SignInManager : SignInManager<IdentityUser>
    {
        public SignInManager()
            : base(new UserManager(),
                Substitute.For<IHttpContextAccessor>(),
                Substitute.For<IUserClaimsPrincipalFactory<IdentityUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<ILogger<SignInManager<IdentityUser>>>(),
                Substitute.For<IAuthenticationSchemeProvider>(),
                Substitute.For<IUserConfirmation<IdentityUser>>())
        { }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(string.IsNullOrEmpty(userName) 
                ? SignInResult.Failed 
                : SignInResult.Success);
        }
    }
    
    public class UserManager : UserManager<IdentityUser>
    {
        public UserManager()
            : base(Substitute.For<IUserStore<IdentityUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<IdentityUser>>(),
                Array.Empty<IUserValidator<IdentityUser>>(),
                Array.Empty<IPasswordValidator<IdentityUser>>(),
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<IdentityUser>>>())
        { }

        public override Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return Task.FromResult(string.IsNullOrEmpty(user.Email) 
                ? IdentityResult.Failed(new IdentityError { Code = "code", Description = "email"}) 
                : IdentityResult.Success);
        }

        public override Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> AddClaimAsync(IdentityUser user, Claim claim)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(new IdentityUser() { Email = email });;
        }

        public override Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            IList<Claim> claims = new List<Claim> { new(ClaimTypes.Name, "test") };
            return Task.FromResult(claims);
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

    }
}
