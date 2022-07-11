using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Auth
{
    public sealed class AuthProviderAuthenticationService : IAuthenticationService
    {
        //private readonly IAuthenticationService _inner;
        private readonly IAuthenticationSchemeProvider _schemes;
        private readonly ISystemClock _clock;

        public AuthProviderAuthenticationService(IAuthenticationSchemeProvider schemes, ISystemClock clock)
        {
            //_inner = inner;
            this._schemes = schemes;
            this._clock = clock;
        }

        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            return null;//_inner.AuthenticateAsync(context, scheme);
        }

        public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
