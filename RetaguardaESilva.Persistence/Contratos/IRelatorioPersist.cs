using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IRelatorioPersist
    {
        Task<IEnumerable<Cliente>> GetAllClientesAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Cliente>> GetAllClientesAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Cliente>> GetAllClientesInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Cliente>> GetAllClientesExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Fornecedor>> GetAllFornecedoresAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Fornecedor>> GetAllFornecedoresAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Fornecedor>> GetAllFornecedoresInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Fornecedor>> GetAllFornecedoresExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosAllAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosAtivoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosInativoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosExcluidoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosAllAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosAtivoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosInativoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosExcluidoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosAllAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosAtivoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosInativoAsync(int empresaId);
        Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosExcluidoAsync(int empresaId);
        Task<IEnumerable<Funcionario>> GetAllFuncionariosAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Funcionario>> GetAllFuncionariosAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Funcionario>> GetAllFuncionariosInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Funcionario>> GetAllFuncionariosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Transportador>> GetAllTransportadoresAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Transportador>> GetAllTransportadoresAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Transportador>> GetAllTransportadoresInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Transportador>> GetAllTransportadoresExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<UsuarioViewModel>> GetAllUsuariosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Empresa>> GetAllEmpresasAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Empresa>> GetAllEmpresasAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Empresa>> GetAllEmpresasInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Empresa>> GetAllEmpresasExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Produto>> GetAllProdutosAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Produto>> GetAllProdutosAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Produto>> GetAllProdutosInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        Task<IEnumerable<Produto>> GetAllProdutosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueAtivoRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueInativoRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueExcluidoRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<PedidoViewModel> GetAllPedidos(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<PedidoViewModel> GetAllPedidosEmAnalise(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<PedidoViewModel> GetAllPedidosConfirmados(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<PedidoViewModel> GetAllPedidosCancelados(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<NotaFiscalViewModel> GetAllNotasFiscais(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<NotaFiscalViewModel> GetAllNotasFiscaisAprovadas(int empresaId, DateTime dataInicio, DateTime dataFinal);
        IEnumerable<NotaFiscalViewModel> GetAllNotasFiscaisCanceladas(int empresaId, DateTime dataInicio, DateTime dataFinal);
    }
}