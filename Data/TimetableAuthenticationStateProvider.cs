using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
namespace TimetableHelper.Data
{

    public class TimetableAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TimetableAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                // Ensure there's always a valid ClaimsPrincipal to prevent null issues
                user = new ClaimsPrincipal(new ClaimsIdentity());
            }

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
