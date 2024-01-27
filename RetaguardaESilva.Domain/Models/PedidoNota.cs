using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Models
{
    public class PedidoNota
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public double CodigoProduto { get; set; }
        public int ProdutoId { get; set; }
        public int EmpresaId { get; set; }
        public int TransportadorId { get; set; }
        public int UsuarioId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateTime? DataCadastroPedidoNota { get; set; }
        public int Status { get; set; }
    }
}
