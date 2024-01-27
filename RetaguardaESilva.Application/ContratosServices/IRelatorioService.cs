using RetaguardaESilva.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IRelatorioService
    {
        Task<IEnumerable<ClienteDTO>> GetClientesAllAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ClienteDTO>> GetClientesAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ClienteDTO>> GetClientesInativoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ClienteDTO>> GetClientesExcluidoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FornecedorDTO>> GetFornecedoresAllAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FornecedorDTO>> GetFornecedoresAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FornecedorDTO>> GetFornecedoresInativoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FornecedorDTO>> GetFornecedoresExcluidoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosAllAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosAtivoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosInativoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosExcluidoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosAllAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosAtivoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosInativoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosExcluidoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosAllAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosAtivoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosInativoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosExcluidoAsync(int empresaId);
        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAllAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosInativoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosExcluidoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<TransportadorDTO>> GetTransportadoresAllAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<TransportadorDTO>> GetTransportadoresAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<TransportadorDTO>> GetTransportadoresInativoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<TransportadorDTO>> GetTransportadoresExcluidoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<UsuarioDTO>> GetUsuarioAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EmpresaDTO>> GetEmpresasAllAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EmpresaDTO>> GetEmpresasAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EmpresaDTO>> GetEmpresasInativoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EmpresaDTO>> GetEmpresasExcluidoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ProdutoDTO>> GetProdutosAllAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ProdutoDTO>> GetProdutosAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ProdutoDTO>> GetProdutosInativoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<ProdutoDTO>> GetProdutosExcluidoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesAtivoAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesInativosAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesExcluidosAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosEmAnaliseAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosConfirmadosAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosCanceladosAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<NotasFiscaisDTO>> GetAllNotasFiscaisAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<NotasFiscaisDTO>> GetAllNotasFiscaisAprovadasAsync(int empresaId, string dataIncio, string dataFinal);
        Task<IEnumerable<NotasFiscaisDTO>> GetAllNotasFiscaisCanceladasAsync(int empresaId, string dataIncio, string dataFinal);
    }
}
