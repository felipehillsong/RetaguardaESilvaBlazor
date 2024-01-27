using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class NotaFiscalDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public int EmpresaId { get; set; }
        public int TransportadorId { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal PrecoTotal { get; set; }
        public string ChaveAcesso { get; set; }
        public DateTime? DataCadastroNotaFiscal { get; set; }
        public int Status { get; set; }
    }

    public class NotasFiscaisDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string NomeCliente { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateTime? DataCadastroNotaFiscal { get; set; }
        public string StatusNota { get; set; }
        public int Status { get; set; }

        public NotasFiscaisDTO()
        {

        }

        public NotasFiscaisDTO(int id, int pedidoId, string nomeCliente, int quantidadeItens, decimal precoTotal, DateTime? dataCadastroNotaFiscal, string statusNota, int status)
        {
            this.Id = id;
            this.PedidoId = pedidoId;
            this.NomeCliente = nomeCliente;
            this.QuantidadeItens = quantidadeItens;
            this.PrecoTotal = precoTotal;
            this.DataCadastroNotaFiscal = dataCadastroNotaFiscal;
            this.StatusNota = statusNota;
            this.Status = status;
        }
    }

    public class NotaFiscalIdDTO : NotaFiscalDTO
    {
        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public Transportador Transportador { get; set; }
        public List<ProdutoPedidoDTO> Produto { get; set; }
        public string StatusNota { get; set; }
    }
}
