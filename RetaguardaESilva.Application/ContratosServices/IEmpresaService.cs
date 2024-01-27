using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IEmpresaService
    {
        Task<EmpresaCreateDTO> AddEmpresa(EmpresaCreateDTO model);
        Task<EmpresaUpdateDTO> UpdateEmpresa(EmpresaUpdateDTO model);
        Task<bool> DeleteEmpresa(int id);
        Task<IEnumerable<EmpresaDTO>> GetAllEmpresasAsync();
        Task<EmpresaDTO> GetEmpresaByIdAsync(int id);
    }
}
