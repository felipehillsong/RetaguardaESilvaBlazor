using Microsoft.EntityFrameworkCore;
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
    public class TransportadorPersist : ITransportadorPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public TransportadorPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }     

        public async Task<IEnumerable<Transportador>> GetAllTransportadoresAsync(int empresaId)
        {
            return await _context.Transportador.AsNoTracking().Where(t => t.EmpresaId == empresaId).OrderBy(t => t.Id).ToListAsync();            
        }       

        public async Task<Transportador> GetTransportadorByIdAsync(int empresaId, int transportadorId)
        {
            return await _context.Transportador.AsNoTracking().Where(t => t.Id == transportadorId && t.EmpresaId == empresaId).OrderBy(t => t.Id).FirstOrDefaultAsync();
        }
    }
}
