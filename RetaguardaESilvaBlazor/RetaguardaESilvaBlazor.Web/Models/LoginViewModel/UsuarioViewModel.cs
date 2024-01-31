namespace RetaguardaESilvaBlazor.Web.Models.LoginViewModel
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime? DataCadastroUsuario { get; set; }
        public bool Ativo { get; set; }
        public int FuncionarioId { get; set; }
        public int EmpresaId { get; set; }
        public string NomeEmpresa { get; set; }
        public List<PermissaoViewModel>? Permissoes { get; set; }
    }
}
