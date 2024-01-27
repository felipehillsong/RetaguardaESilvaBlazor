using System;
using System.Collections.Generic;
using System.Linq;
using RetaguardaESilva.Domain.Models;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IProdutoPersist
    {
        Task<IEnumerable<Produto>> GetAllProdutosAsync(int empresaId);
        Task<IEnumerable<Fornecedor>> GetAllFornecedoresAsync(int empresaId);
        Task<Produto> GetProdutoByIdAsync(int empresaId, int produtoId);
    }
}