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
    public class PedidoNotaPersist : IPedidoNotaPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public PedidoNotaPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoNota>> GetAllPedidosNotaAsync(int empresaId, int pedidoId)
        {
            return await _context.PedidoNota.AsNoTracking().Where(pn => pn.EmpresaId == empresaId && pn.PedidoId == pedidoId).OrderBy(p => p.EmpresaId).ToListAsync();
        }

        public async Task<PedidoNota> GetPedidosNotaByIdAsync(int empresaId, int pedidoId, int produtoId)
        {
            return await _context.PedidoNota.AsNoTracking().Where(pn => pn.EmpresaId == empresaId && pn.PedidoId == pedidoId && pn.ProdutoId == produtoId).OrderBy(pn => pn.Id).FirstOrDefaultAsync();
        }

        public async Task<PedidoNota> GetPedidosNotaByIdStatusAsync(int empresaId, int pedidoId, int status)
        {
            return await _context.PedidoNota.AsNoTracking().Where(pn => pn.EmpresaId == empresaId && pn.PedidoId == pedidoId && pn.Status == status).OrderBy(pn => pn.Id).FirstOrDefaultAsync();
        }
    }
}
