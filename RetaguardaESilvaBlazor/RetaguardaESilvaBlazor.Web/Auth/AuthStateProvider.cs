using Microsoft.AspNetCore.Components.Authorization;
using RetaguardaESilvaBlazor.Web.Models.LoginViewModel;
using System.Net.Http;
using System.Security.Claims;

namespace RetaguardaESilvaBlazor.Web.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var usuario = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim("Nome", "Felipe"),
                    new Claim("Email", "felipesilva@gmail.com"),
                    new Claim("Id", 11.ToString())
                }, "autenticado");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuario)));
        }
    }
}
