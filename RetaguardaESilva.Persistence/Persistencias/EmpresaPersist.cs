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
    public class EmpresaPersist : IEmpresaPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public EmpresaPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetAllEmpresasAsync()
        {
            return await _context.Empresa.AsNoTracking().ToListAsync();
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int empresaId)
        {
            return await _context.Empresa.AsNoTracking().Where(e => e.Id == empresaId).FirstOrDefaultAsync();
        }        
    }
}
