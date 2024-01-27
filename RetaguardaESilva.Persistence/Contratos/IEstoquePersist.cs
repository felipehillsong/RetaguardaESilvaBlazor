using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IEstoquePersist
    {   
        Task<IEnumerable<Estoque>> GetAllEstoqueClienteAsync(int empresaId);
        Task<IEnumerable<Estoque>> GetAllEstoqueRelatorioAsync(int empresaId);
        Task<Estoque> GetEstoqueByProdutoIdAsync(int empresaId, int produtoId);
        Task<Estoque> GetEstoqueByIdAsync(int empresaId, int estoqueId);
        Task<IEnumerable<EnderecoProduto>> GetAllEnderecosProdutosAsync(int empresaId);
        Task<EnderecoProduto> GetEnderecoProdutoByIdAsync(int empresaId, int enderecoProdutoId);
        Task<EnderecoProduto> GetEnderecoProdutoDeleteByIdAsync(int empresaId, int estoqueId);
    }
}