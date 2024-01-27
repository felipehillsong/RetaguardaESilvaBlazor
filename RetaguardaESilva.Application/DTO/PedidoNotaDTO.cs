using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class PedidoNotaDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }
        public double CodigoProduto { get; set; }
        public int EmpresaId { get; set; }
        public int TransportadorId { get; set; }
        public int UsuarioId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateTime? DataCadastroPedidoNota { get; set; }
        public int Status { get; set; }
    }

    public class PedidoNotaCreateDTO : PedidoNotaDTO
    {

    }

    public class PedidoNotaUpdateDTO : PedidoNotaDTO
    {

    }
}
