using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class PermissaoDTO
    {
        public int Id { get; set; }
        public bool VisualizarCliente { get; set; }
        public bool ClienteCadastro { get; set; }
        public bool ClienteEditar { get; set; }
        public bool ClienteDetalhe { get; set; }
        public bool ClienteExcluir { get; set; }
        public bool VisualizarEmpresa { get; set; }
        public bool EmpresaCadastro { get; set; }
        public bool EmpresaEditar { get; set; }
        public bool EmpresaDetalhe { get; set; }
        public bool EmpresaExcluir { get; set; }
        public bool VisualizarEstoque { get; set; }
        public bool EstoqueEditar { get; set; }
        public bool EstoqueDetalhe { get; set; }
        public bool EstoqueExcluir { get; set; }
        public bool VisualizarEnderecoProduto { get; set; }
        public bool EnderecoProdutoCadastro { get; set; }
        public bool EnderecoProdutoEditar { get; set; }
        public bool EnderecoProdutoDetalhe { get; set; }
        public bool EnderecoProdutoExcluir { get; set; }
        public bool VisualizarFornecedor { get; set; }
        public bool FornecedorCadastro { get; set; }
        public bool FornecedorEditar { get; set; }
        public bool FornecedorDetalhe { get; set; }
        public bool FornecedorExcluir { get; set; }
        public bool VisualizarFuncionario { get; set; }
        public bool FuncionarioCadastro { get; set; }
        public bool FuncionarioEditar { get; set; }
        public bool FuncionarioDetalhe { get; set; }
        public bool FuncionarioExcluir { get; set; }
        public bool VisualizarProduto { get; set; }
        public bool ProdutoCadastro { get; set; }
        public bool ProdutoEditar { get; set; }
        public bool ProdutoDetalhe { get; set; }
        public bool ProdutoExcluir { get; set; }
        public bool GerarRelatorio { get; set; }
        public bool VisualizarTransportador { get; set; }
        public bool TransportadorCadastro { get; set; }
        public bool TransportadorEditar { get; set; }
        public bool TransportadorDetalhe { get; set; }
        public bool TransportadorExcluir { get; set; }
        public bool VisualizarUsuario { get; set; }
        public bool UsuarioCadastro { get; set; }
        public bool UsuarioEditar { get; set; }
        public bool UsuarioPermissoes { get; set; }
        public bool UsuarioExcluir { get; set; }
        public bool VisualizarPedido { get; set; }
        public bool PedidoCadastro { get; set; }
        public bool PedidoEditar { get; set; }
        public bool PedidoDetalhe { get; set; }
        public bool PedidoExcluir { get; set; }
        public bool VisualizarNotaFiscal { get; set; }
        public bool NotaFiscalCadastro { get; set; }
        public bool NotaFiscalGerarPDF { get; set; }
        public bool NotaFiscalCancelar { get; set; }
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
    }
}
