using RetaguardaESilva.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalDTO> AddNotaFiscal(NotaFiscalDTO model);
        Task<NotaFiscalDTO> CancelarNotaFiscal(int empresaId, int notafiscalId);
        Task<IEnumerable<NotasFiscaisDTO>> GetAllNotaFiscalAsync(int empresaId);
        Task<NotaFiscalIdDTO> GetNotaFiscalByIdAsync(int empresaId, int notaFiscalId, bool? notaFiscalEmissao, bool? exclusao);
        Task<NotaFiscalDTO> GetNotaFiscalPedidoByIdAsync(int empresaId, int pedidoId);
    }
}
