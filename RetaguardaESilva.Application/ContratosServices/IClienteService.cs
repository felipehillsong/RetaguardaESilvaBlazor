using RetaguardaESilva.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IClienteService
    {
        Task<ClienteCreateDTO> AddCliente(ClienteCreateDTO model);
        Task<ClienteUpdateDTO> UpdateCliente(ClienteUpdateDTO model);
        Task<bool> DeleteCliente(int empresaId, int clienteId);
        Task<IEnumerable<ClienteDTO>> GetAllClientesAsync(int empresaId);        
        Task<ClienteDTO> GetClienteByIdAsync(int empresaId, int clienteId);
    }
}
