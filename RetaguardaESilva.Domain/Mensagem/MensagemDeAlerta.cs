using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Mensagem
{
    public class MensagemDeAlerta
    {
        public const string ProdutoSemFornecedor = "Produto sem fornecedor";
        public const string ProdutoSemEndereco = "Produto sem endereço";
        public const string PedidoEmAnalise = "Pedido em analise";
        public const string PedidoConfirmado = "Pedido confirmado";
        public const string PedidoCancelado = "Pedido cancelado";
        public const string NotaFiscalAprovada = "Nota fiscal aprovada";
        public const string NotaFiscalCancelada = "Nota fiscal cancelada";
        public const string ClienteExcluido = "Cliente excluído";
        public const string EmailPedidoEmAnalise = "Pedido em análise";
        public const string EmailPedidoExluido = "Pedido cancelado com sucesso!";
        public const string EmailNotaFiscalExluido = "Pedido cancelado com sucesso, a sua nota fiscal é: ";
        public const string EmailPedidoConfirmado = "Pedido confirmado com sucesso!";
        public const string EmailPedidoEmAnaliseCorpo = "Seu pedido encontra-se pendente de autorização de venda. O numero do pedido é: ";
        public const string EmailPedidoEmAnaliseAtualizarCorpo = "Os produtos do seu pedido foram atualizado e pendente de autorização de venda. O numero do pedido é: ";
        public const string EmailPedidoConfirmadoCorpo = "Seu pedido foi confirmado com sucesso. O numero do pedido é: ";
        public const string EmailPedidoProdutos = " Seus produtos são: ";
        public const string EmailPedidoValorTotal = "\n\nO valor total do pedido é: ";
        public const string EmailNotaFiscal = "\nO numero da sua nota fiscal é: ";
    }
}
