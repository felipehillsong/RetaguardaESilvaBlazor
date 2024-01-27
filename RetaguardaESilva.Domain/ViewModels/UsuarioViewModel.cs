using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime? DataCadastroUsuario { get; set; }
        public bool Ativo { get; set; }
        public int FuncionarioId { get; set; }
        public int EmpresaId { get; set; }

        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(int id, string nomeEmpresaa, string nome, string email, string senha, DateTime? dataCadastroUsuario, bool ativo, int funcionarioId, int empresaId)
        {
            this.Id = id;
            this.NomeEmpresa = nomeEmpresaa;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataCadastroUsuario = dataCadastroUsuario;
            this.Ativo = ativo;
            this.FuncionarioId = funcionarioId;
            this.EmpresaId = empresaId;
        }
    }
}
