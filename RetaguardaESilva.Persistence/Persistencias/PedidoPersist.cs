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
    public class PedidoPersist : IPedidoPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public PedidoPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync(int empresaId)
        {
            return await _context.Pedido.AsNoTracking().Where(p => p.EmpresaId == empresaId).OrderBy(p => p.EmpresaId).ToListAsync();
            
        }

        public async Task<Pedido> GetPedidoByIdAsync(int empresaId, int pedidoId)
        {
            return await _context.Pedido.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.Id == pedidoId).OrderBy(c => c.EmpresaId).FirstOrDefaultAsync();
        }
    }
}
