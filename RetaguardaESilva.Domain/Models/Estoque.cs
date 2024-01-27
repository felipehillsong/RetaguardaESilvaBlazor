using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataCadastroEstoque { get; set; }
        public bool StatusExclusao { get; set; }
    }
}
