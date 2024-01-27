using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Enumeradores
{
    public enum StatusPedido
    {
        RetornoPedido = 0,
        PedidoEmAnalise = 1,
        PedidoConfirmado = 2,
        PedidoCancelado = 3
    }
}
