using System.ComponentModel.DataAnnotations;

namespace RetaguardaESilvaBlazor.Web.Models.LoginViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um endereço de email válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
    }
}
