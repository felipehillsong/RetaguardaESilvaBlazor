using RetaguardaESilvaBlazor.Web.Models.LoginViewModel;
using RetaguardaESilvaBlazor.Web.PersistenciaService;
using System.Net;
using System.Net.Http.Json;

namespace RetaguardaESilvaBlazor.Web.ContratosServices
{
    public class LoginService : ILoginService
    {
        public HttpClient _httpClient;
        public ILogger<LoginService> _logger;

        public LoginService(HttpClient httpClient, ILogger<LoginService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UsuarioViewModel> Login(LoginViewModel login)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Login", login);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    else
                    {
                        var usuario = await response.Content.ReadFromJsonAsync<UsuarioViewModel>();
                        if (usuario != null)
                        {
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
