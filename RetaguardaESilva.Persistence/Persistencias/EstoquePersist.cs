using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;
using RetaguardaESilva.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetaguardaESilva.Domain.Enumeradores;

namespace RetaguardaESilva.Persistence.Persistencias
{
    public class EstoquePersist : IEstoquePersist
    {
        private readonly RetaguardaESilvaContext _context;
        public EstoquePersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }       

        public async Task<IEnumerable<Estoque>> GetAllEstoqueClienteAsync(int empresaId)
        {
            return await _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId && e.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<IEnumerable<Estoque>> GetAllEstoqueRelatorioAsync(int empresaId)
        {
            return await _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId).OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<Estoque> GetEstoqueByProdutoIdAsync(int empresaId, int produtoId)
        {            
            return await _context.Estoque.AsNoTracking().Where(e => e.ProdutoId == produtoId && e.EmpresaId == empresaId && e.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).FirstOrDefaultAsync();
        }

        public async Task<Estoque> GetEstoqueByIdAsync(int empresaId, int estoqueId)
        {
            return await _context.Estoque.AsNoTracking().Where(e => e.Id == estoqueId && e.EmpresaId == empresaId && e.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).FirstOrDefaultAsync();
        }

        public async Task<EnderecoProduto> GetEnderecoProdutoDeleteByIdAsync(int empresaId, int estoqueId)
        {
            return await _context.EnderecoProduto.AsNoTracking().Where(ep => ep.EstoqueId == estoqueId && ep.EmpresaId == empresaId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EnderecoProduto>> GetAllEnderecosProdutosAsync(int empresaId)
        {
            return await _context.EnderecoProduto.AsNoTracking().Where(ep => ep.EmpresaId == empresaId).OrderBy(ep => ep.Id).ToListAsync();
        }

        public async Task<EnderecoProduto> GetEnderecoProdutoByIdAsync(int empresaId, int enderecoProdutoId)
        {
            return await _context.EnderecoProduto.AsNoTracking().Where(ep => ep.Id == enderecoProdutoId && ep.EmpresaId == empresaId).FirstOrDefaultAsync();
        }
    }
}
