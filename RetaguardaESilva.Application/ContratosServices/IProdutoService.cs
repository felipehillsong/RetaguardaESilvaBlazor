using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IProdutoService
    {
        Task<ProdutoCreateDTO> AddProduto(ProdutoCreateDTO model);
        Task<ProdutoUpdateDTO> UpdateProduto(ProdutoUpdateDTO model);
        Task<bool> DeleteProduto(int empresaId, int produtoId);
        Task<IEnumerable<ProdutoDTO>> GetAllProdutosAsync(int empresaId);
        Task<IEnumerable<FornecedorDTO>> GetAllFornecedoresAsync(int empresaId);
        Task<ProdutoDTO> GetProdutoByIdAsync(int empresaId, int produtoId);    
    }
}
