using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IClientePersist
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync(int empresaId);
        Task<Cliente> GetClienteByIdAsync(int empresaId, int clienteId);        
    }
}