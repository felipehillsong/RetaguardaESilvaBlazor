using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;
using RetaguardaESilva.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Persistencias
{
    public class ProdutoPersist : IProdutoPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public ProdutoPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync(int empresaId)
        {
            return await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Fornecedor>> GetAllFornecedoresAsync(int empresaId)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo.Equals(true)).OrderBy(p => p.EmpresaId).ToListAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(int empresaId, int produtoId)
        {
            return await _context.Produto.AsNoTracking().Where(p => p.Id == produtoId && p.EmpresaId == empresaId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).OrderBy(p => p.Id).FirstOrDefaultAsync();
        }
    }
}
