using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Persistencias
{
    public class NotaFiscalPersist : INotaFiscalPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public NotaFiscalPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotaFiscal>> GetAllNotaFiscalAsync(int empresaId)
        {
            return await _context.NotaFiscal.AsNoTracking().Where(nf => nf.EmpresaId == empresaId).OrderBy(nf => nf.EmpresaId).ToListAsync();
        }

        public async Task<NotaFiscal> GetNotaFiscalByIdAsync(int empresaId, int notaFiscalId)
        {
            return await _context.NotaFiscal.AsNoTracking().Where(nf => nf.EmpresaId == empresaId && nf.Id == notaFiscalId).OrderBy(nf => nf.Id).FirstOrDefaultAsync();
        }

        public async Task<NotaFiscal> GetNotaFiscalPedidoByIdAsync(int empresaId, int pedidoId)
        {
            return await _context.NotaFiscal.AsNoTracking().Where(nf => nf.EmpresaId == empresaId && nf.PedidoId == pedidoId).OrderBy(nf => nf.PedidoId).FirstOrDefaultAsync();
        }
    }
}
