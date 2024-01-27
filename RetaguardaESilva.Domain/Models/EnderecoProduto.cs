using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Models
{
    public class EnderecoProduto
    {
        public int Id { get; set; }
        public string NomeEndereco { get; set; }
        public int ProdutoId { get; set; }
        public int EstoqueId { get; set; }
        public int EmpresaId { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastroEnderecoProduto { get; set; }
    }
}
