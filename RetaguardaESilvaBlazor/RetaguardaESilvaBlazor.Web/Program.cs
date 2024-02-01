using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RetaguardaESilvaBlazor.Web;
using RetaguardaESilvaBlazor.Web.Auth;
using RetaguardaESilvaBlazor.Web.ContratosServices;
using RetaguardaESilvaBlazor.Web.PersistenciaService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var url = "https://localhost:7104";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
await builder.Build().RunAsync();
