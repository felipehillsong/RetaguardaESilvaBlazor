using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IPedidoPersist
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync(int empresaId);
        Task<Pedido> GetPedidoByIdAsync(int empresaId, int pedidoId);
    }
}