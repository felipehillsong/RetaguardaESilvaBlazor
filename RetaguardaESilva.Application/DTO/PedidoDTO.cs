using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int TransportadorId { get; set; }
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateTime? DataCadastroPedido { get; set; }
        public List<ProdutoPedidoDTO> Produtos { get; set; }
    }

    public class PedidoCreateDTO : PedidoDTO
    {
        public int Status { get; set; }
    }

    public class PedidoUpdateDTO : PedidoCreateDTO
    {
        public bool EnviarEmailPosFinalizar { get; set; }
    }

    public class PedidoRetornoDTO : PedidoDTO
    {
        public string ClienteNome { get; set; }
        public string TransportadorNome { get; set; }
        public string StatusPedido { get; set; }
        public PedidoRetornoDTO()
        {

        }

        public PedidoRetornoDTO(int id, string clienteNome, decimal precoTotal, DateTime? dataCadastroPedido, string statusPedido)
        {
            this.Id = id;
            this.ClienteNome = clienteNome;
            this.PrecoTotal = precoTotal;
            this.DataCadastroPedido = dataCadastroPedido;
            this.StatusPedido = statusPedido;
        }

        public PedidoRetornoDTO(int id, int clienteId, string clienteNome, int transportadorId, string transportadorNome, int empresaId, int usuarioId, decimal precoTotal, DateTime? dataCadastroPedido, string statusPedido, List<ProdutoPedidoDTO> produtos)
        {
            this.Id = id;
            this.ClienteId = clienteId;
            this.ClienteNome = clienteNome;
            this.TransportadorId = transportadorId;
            this.TransportadorNome = transportadorNome;
            this.EmpresaId = empresaId;
            this.UsuarioId = usuarioId;
            this.PrecoTotal = precoTotal;
            this.DataCadastroPedido = dataCadastroPedido;
            this.StatusPedido = statusPedido;
            this.Produtos = produtos;
        }
    }
}