using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface ITransportadorPersist
    {
        Task<IEnumerable<Transportador>> GetAllTransportadoresAsync(int empresaId);
        Task<Transportador> GetTransportadorByIdAsync(int empresaId, int transportadorId);        
    }
}