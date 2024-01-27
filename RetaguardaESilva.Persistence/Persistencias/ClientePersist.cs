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
    public class ClientePersist : IClientePersist
    {
        private readonly RetaguardaESilvaContext _context;
        public ClientePersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync(int empresaId)
        {
            return await _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId && c.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(c => c.EmpresaId).ToListAsync();
            
        }

        public async Task<Cliente> GetClienteByIdAsync(int empresaId, int clienteId)
        {
            return await _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId && c.Id == clienteId && c.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(c => c.EmpresaId).FirstOrDefaultAsync();        }
    }
}
