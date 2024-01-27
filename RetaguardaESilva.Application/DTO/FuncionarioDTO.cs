using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
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
    }

    public class FuncionarioCreateDTO : FuncionarioDTO
    {

    }

    public class FuncionarioUpdateDTO : FuncionarioDTO
    {

    }
}
