using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.ViewModels
{
    public class FuncionariosUsuariosViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
        public string? Complemento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime? DataCadastroFuncionario { get; set; }
        public bool Ativo { get; set; }
        public int EmpresaId { get; set; }

        public FuncionariosUsuariosViewModel(int id, string nome, string endereco, string bairro, string numero, string municipio, string uf, string pais, string cep, string complemento, string telefone, string email, string cpf, DateTime? dataCadastroFuncionario, bool ativo, int empresaId)
        {
            this.Id = id;
            this.Nome = nome;
            this.Endereco = endereco;
            this.Bairro = bairro;
            this.Numero = numero;
            this.Municipio = municipio;
            this.UF = uf;
            this.Pais = pais;
            this.CEP = cep;
            this.Complemento = complemento;
            this.Telefone = telefone;
            this.Email = email;
            this.CPF = cpf;
            this.DataCadastroFuncionario = dataCadastroFuncionario;
            this.Ativo = ativo;
            this.EmpresaId = empresaId;
        }
    }
}
