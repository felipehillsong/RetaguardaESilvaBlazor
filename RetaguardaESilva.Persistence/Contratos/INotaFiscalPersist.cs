using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface INotaFiscalPersist
    {
        Task<IEnumerable<NotaFiscal>> GetAllNotaFiscalAsync(int empresaId);
        Task<NotaFiscal> GetNotaFiscalByIdAsync(int empresaId, int notaFiscalId);
        Task<NotaFiscal> GetNotaFiscalPedidoByIdAsync(int empresaId, int pedidoId);
    }
}
