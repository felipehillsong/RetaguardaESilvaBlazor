using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IFornecedorService
    {
        Task<FornecedorCreateDTO> AddFornecedor(FornecedorCreateDTO model);
        Task<FornecedorUpdateDTO> UpdateFornecedor(FornecedorUpdateDTO model);
        Task<bool> DeleteFornecedor(int empresaId, int fornecedorId);
        Task<IEnumerable<FornecedorDTO>> GetAllFornecedoresAsync(int empresaId);
        Task<FornecedorDTO> GetFornecedorByIdAsync(int empresaId, int fornecedorId);
    }
}
