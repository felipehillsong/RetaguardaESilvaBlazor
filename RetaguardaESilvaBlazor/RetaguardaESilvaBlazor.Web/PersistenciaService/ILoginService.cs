using RetaguardaESilvaBlazor.Web.Models.LoginViewModel;

namespace RetaguardaESilvaBlazor.Web.PersistenciaService
{
    public interface ILoginService
    {
        Task<UsuarioViewModel> Login(LoginViewModel login);
    }
}
