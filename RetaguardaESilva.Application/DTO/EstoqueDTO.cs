using Microsoft.EntityFrameworkCore.Query.Internal;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class EstoqueDTO
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataCadastroEstoque { get; set; }
        public bool StatusExclusao { get; set; }
    }

    public class EstoqueViewModelDTO
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public int FornecedorId { get; set; }
        public string FornecedorNome { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public int EnderecoProdutoId { get; set; }
        public DateTime DataCadastroEstoque { get; set; }

        public EstoqueViewModelDTO(int estoqueId, int empresaId, string empresaNome, int fornecedorId, string fornecedorNome, int produtoId, string produtoNome, int quantidade, int enderecoProdutoId, DateTime dataCadastroEstoque)
        {
            this.Id = estoqueId;
            this.EmpresaId = empresaId;
            this.EmpresaNome = empresaNome;
            this.FornecedorId = fornecedorId;
            this.FornecedorNome = fornecedorNome;
            this.ProdutoId = produtoId;
            this.ProdutoNome = produtoNome;
            this.Quantidade = quantidade;
            this.EnderecoProdutoId = enderecoProdutoId;
            this.DataCadastroEstoque = dataCadastroEstoque;
        }
    }

    public class EstoqueViewModelUpdateDTO : EstoqueViewModelDTO
    {
        public EstoqueViewModelUpdateDTO(int estoqueId, int empresaId, string empresaNome, int fornecedorId, string fornecedorNome, int produtoId, string produtoNome, int quantidade, int enderecoProdutoId, DateTime dataCadastroEstoque) : base(estoqueId, empresaId, empresaNome, fornecedorId, fornecedorNome, produtoId, produtoNome, quantidade, enderecoProdutoId, dataCadastroEstoque)
        {
        }
    }

    public class EstoqueRelatorioDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public bool StatusExclusao { get; set; }
        public DateTime? DataCadastroEstoque { get; set; }

        public EstoqueRelatorioDTO(int id, int produtoId, string produtoNome, int quantidade, bool statusExclusao, DateTime? dataCadastroEstoque)
        {
            this.Id = id;
            this.ProdutoId = produtoId;
            this.ProdutoNome = produtoNome;
            this.Quantidade = quantidade;
            this.StatusExclusao = statusExclusao;
            this.DataCadastroEstoque = dataCadastroEstoque;
        }
    }
}
