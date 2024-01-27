using Microsoft.AspNetCore.Mvc;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;

namespace RetaguardaESilva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }
        // GET: api/Relatorio
        [HttpGet]
        public async Task<ActionResult> GetRelatorio(int empresaId, int codigoRelatorio, string? dataInicio, string? dataFinal)
        {
            try
            {
                switch (codigoRelatorio)
                {
                    case (int)TipoRelatorio.TodosClientesAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosClientesAtivosInativosExcluidos = await _relatorioService.GetClientesAllAsync(empresaId, dataInicio, dataFinal);
                            if (todosClientesAtivosInativosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosClientesAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosClientesAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosClientesAtivos = await _relatorioService.GetClientesAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (todosClientesAtivos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosClientesAtivos);
                        }
                    case (int)TipoRelatorio.TodosClientesInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosClientesInativos = await _relatorioService.GetClientesInativoAsync(empresaId, dataInicio, dataFinal);
                            if (todosClientesInativos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosClientesInativos);
                        }
                    case (int)TipoRelatorio.TodosClientesExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosClientesExcluidos = await _relatorioService.GetClientesExcluidoAsync(empresaId, dataInicio, dataFinal);
                            if (todosClientesExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosClientesExcluidos);
                        }
                    case (int)TipoRelatorio.TodosFornecedoresAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFornecedoresAtivosInativosExcluidos = await _relatorioService.GetFornecedoresAllAsync(empresaId, dataInicio, dataFinal);
                            if (todosFornecedoresAtivosInativosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFornecedoresAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosFornecedoresAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFornecedoresAtivos = await _relatorioService.GetFornecedoresAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (todosFornecedoresAtivos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFornecedoresAtivos);
                        }
                    case (int)TipoRelatorio.TodosFornecedoresInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFornecedoresInativos = await _relatorioService.GetFornecedoresInativoAsync(empresaId, dataInicio, dataFinal);
                            if (todosFornecedoresInativos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFornecedoresInativos);
                        }
                    case (int)TipoRelatorio.TodosFornecedoresExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFornecedoresExcluidos = await _relatorioService.GetFornecedoresExcluidoAsync(empresaId, dataInicio, dataFinal);
                            if (todosFornecedoresExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFornecedoresExcluidos);
                        }
                    case (int)TipoRelatorio.TodosFornecedoresAtivosProdutosAtivoInativoExcluidos:
                        var todosFornecedoresProdutosAtivoInativoExcluidos = await _relatorioService.GetFornecedoresProdutosAllAsync(empresaId);
                        if (todosFornecedoresProdutosAtivoInativoExcluidos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresProdutosAtivoInativoExcluidos);
                    case (int)TipoRelatorio.TodosFornecedoresAtivosProdutosAtivos:
                        var todosFornecedoresProdutosAtivos = await _relatorioService.GetFornecedoresProdutosAtivoAsync(empresaId);
                        if (todosFornecedoresProdutosAtivos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresProdutosAtivos);
                    case (int)TipoRelatorio.TodosFornecedoresAtivosProdutosInativos:
                        var todosFornecedoresProdutosInativos = await _relatorioService.GetFornecedoresProdutosInativoAsync(empresaId);
                        if (todosFornecedoresProdutosInativos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresProdutosInativos);
                    case (int)TipoRelatorio.TodosFornecedoresAtivosProdutosExcluidos:
                        var todosFornecedoresProdutosExcluidos = await _relatorioService.GetFornecedoresProdutosExcluidoAsync(empresaId);
                        if (todosFornecedoresProdutosExcluidos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresProdutosExcluidos);
                    case (int)TipoRelatorio.TodosFornecedoresInativosProdutosAtivoInativoExcluidos:
                        var todosFornecedoresInativosProdutosInativosInativoExcluidos = await _relatorioService.GetFornecedoresInativosProdutosAllAsync(empresaId);
                        if (todosFornecedoresInativosProdutosInativosInativoExcluidos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresInativosProdutosInativosInativoExcluidos);
                    case (int)TipoRelatorio.TodosFornecedoresInativosProdutosAtivos:
                        var todosFornecedoresInativosProdutosAtivos = await _relatorioService.GetFornecedoresInativosProdutosAtivoAsync(empresaId);
                        if (todosFornecedoresInativosProdutosAtivos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresInativosProdutosAtivos);
                    case (int)TipoRelatorio.TodosFornecedoresInativosProdutosInativos:
                        var todosFornecedoresInativosProdutosInativos = await _relatorioService.GetFornecedoresInativosProdutosInativoAsync(empresaId);
                        if (todosFornecedoresInativosProdutosInativos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresInativosProdutosInativos);
                    case (int)TipoRelatorio.TodosFornecedoresInativosProdutosExcluidos:
                        var todosFornecedoresInativosProdutosExcluidos = await _relatorioService.GetFornecedoresInativosProdutosExcluidoAsync(empresaId);
                        if (todosFornecedoresInativosProdutosExcluidos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresInativosProdutosExcluidos);
                    case (int)TipoRelatorio.TodosFornecedoresExcluidosProdutosAtivoInativoExcluidos:
                        var todosFornecedoresExcluidosProdutosInativosInativoExcluidos = await _relatorioService.GetFornecedoresExcluidosProdutosAllAsync(empresaId);
                        if (todosFornecedoresExcluidosProdutosInativosInativoExcluidos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresExcluidosProdutosInativosInativoExcluidos);
                    case (int)TipoRelatorio.TodosFornecedoresExcluidosProdutosAtivos:
                        var todosFornecedoresExcluidosProdutosAtivos = await _relatorioService.GetFornecedoresExcluidosProdutosAtivoAsync(empresaId);
                        if (todosFornecedoresExcluidosProdutosAtivos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresExcluidosProdutosAtivos);
                    case (int)TipoRelatorio.TodosFornecedoresExcluidosProdutosInativos:
                        var todosFornecedoresExcluidosProdutosInativos = await _relatorioService.GetFornecedoresExcluidosProdutosInativoAsync(empresaId);
                        if (todosFornecedoresExcluidosProdutosInativos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresExcluidosProdutosInativos);
                    case (int)TipoRelatorio.TodosFornecedoresExcluidosProdutosExcluidos:
                        var todosFornecedoresExcluidosProdutosExcluidos = await _relatorioService.GetFornecedoresExcluidosProdutosExcluidoAsync(empresaId);
                        if (todosFornecedoresExcluidosProdutosExcluidos == null)
                        {
                            return NotFound();
                        }
                        return Ok(todosFornecedoresExcluidosProdutosExcluidos);
                    case (int)TipoRelatorio.TodosFuncionariosAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFuncionariosAtivosInativosExcluidos = await _relatorioService.GetFuncionariosAllAsync(empresaId, dataInicio, dataFinal);
                            if (todosFuncionariosAtivosInativosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFuncionariosAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosFuncionariosAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFuncionariosAtivos = await _relatorioService.GetFuncionariosAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (todosFuncionariosAtivos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFuncionariosAtivos);
                        }
                    case (int)TipoRelatorio.TodosFuncionariosInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFuncionariosInativos = await _relatorioService.GetFuncionariosInativoAsync(empresaId, dataInicio, dataFinal);
                            if (todosFuncionariosInativos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFuncionariosInativos);
                        }
                    case (int)TipoRelatorio.TodosFuncionariosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosFuncionariosExcluidos = await _relatorioService.GetFuncionariosExcluidoAsync(empresaId, dataInicio, dataFinal);
                            if (todosFuncionariosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosFuncionariosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosTransportadoresAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosTransportadoresAtivosInativosExcluidos = await _relatorioService.GetTransportadoresAllAsync(empresaId, dataInicio, dataFinal);
                            if (todosTransportadoresAtivosInativosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosTransportadoresAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosTransportadoresAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosTransportadoresAtivos = await _relatorioService.GetTransportadoresAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (todosTransportadoresAtivos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosTransportadoresAtivos);
                        }
                    case (int)TipoRelatorio.TodosTransportadoresInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosTransportadoresInativos = await _relatorioService.GetTransportadoresInativoAsync(empresaId, dataInicio, dataFinal);
                            if (todosTransportadoresInativos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosTransportadoresInativos);
                        }
                    case (int)TipoRelatorio.TodosTransportadoresExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosTransportadoresExcluidos = await _relatorioService.GetTransportadoresExcluidoAsync(empresaId, dataInicio, dataFinal);
                            if (todosTransportadoresExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosTransportadoresExcluidos);
                        }
                    case (int)TipoRelatorio.TodosUsuarios:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosUsuarios = await _relatorioService.GetUsuarioAsync(empresaId, dataInicio, dataFinal);
                            if (todosUsuarios == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosUsuarios);
                        }
                    case (int)TipoRelatorio.TodosEmpresasAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEmpresasAtivosInativosExcluidos = await _relatorioService.GetEmpresasAllAsync(empresaId, dataInicio, dataFinal);
                            if (todosEmpresasAtivosInativosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosEmpresasAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosEmpresasAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEmpresasAtivos = await _relatorioService.GetEmpresasAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (todosEmpresasAtivos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosEmpresasAtivos);
                        }
                    case (int)TipoRelatorio.TodosEmpresasInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEmpresasInativos = await _relatorioService.GetEmpresasInativoAsync(empresaId, dataInicio, dataFinal);
                            if (todosEmpresasInativos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosEmpresasInativos);
                        }
                    case (int)TipoRelatorio.TodosEmpresasExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEmpresasExcluidos = await _relatorioService.GetEmpresasExcluidoAsync(empresaId, dataInicio, dataFinal);
                            if (todosEmpresasExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosEmpresasExcluidos);
                        }
                    case (int)TipoRelatorio.TodosProdutosAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosProdutosAtivosInativosExcluidos = await _relatorioService.GetProdutosAllAsync(empresaId, dataInicio, dataFinal);
                            if (todosProdutosAtivosInativosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosProdutosAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosProdutosAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosProdutosAtivos = await _relatorioService.GetProdutosAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (todosProdutosAtivos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosProdutosAtivos);
                        }
                    case (int)TipoRelatorio.TodosProdutosInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosProdutosInativos = await _relatorioService.GetProdutosInativoAsync(empresaId, dataInicio, dataFinal);
                            if (todosProdutosInativos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosProdutosInativos);
                        }
                    case (int)TipoRelatorio.TodosProdutosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosProdutosExcluidos = await _relatorioService.GetProdutosExcluidoAsync(empresaId, dataInicio, dataFinal);
                            if (todosProdutosExcluidos == null)
                            {
                                return NotFound();
                            }
                            return Ok(todosProdutosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosEstoquesAtivosInativosExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEstoquesAtivosInativosExcluidos = await _relatorioService.GetAllEstoquesAsync(empresaId, dataInicio, dataFinal);
                            if (!todosEstoquesAtivosInativosExcluidos.Any())
                            {
                                return NotFound();
                            }
                            return Ok(todosEstoquesAtivosInativosExcluidos);
                        }
                    case (int)TipoRelatorio.TodosEstoquesAtivos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var TodosEstoquesAtivos = await _relatorioService.GetAllEstoquesAtivoAsync(empresaId, dataInicio, dataFinal);
                            if (!TodosEstoquesAtivos.Any())
                            {
                                return NotFound();
                            }
                            return Ok(TodosEstoquesAtivos);
                        }
                    case (int)TipoRelatorio.TodosEstoquesInativos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEstoquesInativos = await _relatorioService.GetAllEstoquesInativosAsync(empresaId, dataInicio, dataFinal);
                            if (!todosEstoquesInativos.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosEstoquesInativos);
                        }
                    case (int)TipoRelatorio.TodosEstoquesExcluidos:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosEstoquesExcluidos = await _relatorioService.GetAllEstoquesExcluidosAsync(empresaId, dataInicio, dataFinal);
                            if (!todosEstoquesExcluidos.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosEstoquesExcluidos);
                        }
                    case (int)TipoRelatorio.TodosPedidosConfirmadosCancelados:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosPedidosConfirmadosCancelados = await _relatorioService.GetAllPedidosAsync(empresaId, dataInicio, dataFinal);
                            if (!todosPedidosConfirmadosCancelados.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosPedidosConfirmadosCancelados);
                        }
                    case (int)TipoRelatorio.TodosPedidosEmAnalise:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosPedidosEmAnalise = await _relatorioService.GetAllPedidosEmAnaliseAsync(empresaId, dataInicio, dataFinal);
                            if (!todosPedidosEmAnalise.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosPedidosEmAnalise);
                        }
                    case (int)TipoRelatorio.TodosPedidosConfirmados:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosPedidosConfirmados = await _relatorioService.GetAllPedidosConfirmadosAsync(empresaId, dataInicio, dataFinal);
                            if (!todosPedidosConfirmados.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosPedidosConfirmados);
                        }
                    case (int)TipoRelatorio.TodosPedidosCancelados:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosPedidosCancelados = await _relatorioService.GetAllPedidosCanceladosAsync(empresaId, dataInicio, dataFinal);
                            if (!todosPedidosCancelados.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosPedidosCancelados);
                        }
                    case (int)TipoRelatorio.TodosNotasFiscais:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosNotasFiscais = await _relatorioService.GetAllNotasFiscaisAsync(empresaId, dataInicio, dataFinal);
                            if (!todosNotasFiscais.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosNotasFiscais);
                        }
                    case (int)TipoRelatorio.TodosNotasFiscaisAprovados:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosNotasFiscaisAprovados = await _relatorioService.GetAllNotasFiscaisAprovadasAsync(empresaId, dataInicio, dataFinal);
                            if (!todosNotasFiscaisAprovados.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosNotasFiscaisAprovados);
                        }
                    case (int)TipoRelatorio.TodosNotasFiscaisCancelados:
                        if (dataInicio == null || dataFinal == null)
                        {
                            return NotFound(MensagemDeErro.SemDataRelatorio);
                        }
                        else
                        {
                            var todosNotasFiscaisCancelados = await _relatorioService.GetAllNotasFiscaisCanceladasAsync(empresaId, dataInicio, dataFinal);
                            if (!todosNotasFiscaisCancelados.Any())
                            {
                                return NotFound(MensagemDeErro.RelatorioSemRegistro);
                            }
                            return Ok(todosNotasFiscaisCancelados);
                        }
                    default:
                    return NotFound(MensagemDeErro.RelatorioNaoEncontrado);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }
    }
}
