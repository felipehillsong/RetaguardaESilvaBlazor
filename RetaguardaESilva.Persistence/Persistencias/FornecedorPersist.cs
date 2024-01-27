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
    public class FornecedorPersist : IFornecedorPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public FornecedorPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }        

        public async Task<IEnumerable<Fornecedor>> GetAllFornecedoresAsync(int empresaId)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Id).ToListAsync();
        }
        public async Task<Fornecedor> GetFornecedorByIdAsync(int empresaId, int fornecedorId)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.Id == fornecedorId && f.EmpresaId == empresaId && f.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Id).FirstOrDefaultAsync();
        }
    }
}
