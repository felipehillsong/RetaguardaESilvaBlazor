using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IFornecedorPersist
    {
        Task<IEnumerable<Fornecedor>> GetAllFornecedoresAsync(int empresaId);
        Task<Fornecedor> GetFornecedorByIdAsync(int empresaId, int fornecedorId);
    }
}