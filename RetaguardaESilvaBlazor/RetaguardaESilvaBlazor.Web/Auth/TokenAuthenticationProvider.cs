using Microsoft.AspNetCore.Components.Authorization;

namespace RetaguardaESilvaBlazor.Web.Auth
{
    public class TokenAuthenticationProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
