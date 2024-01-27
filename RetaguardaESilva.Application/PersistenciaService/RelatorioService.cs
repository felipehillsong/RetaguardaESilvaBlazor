using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Application.Helpers;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Domain.ViewModels;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Persistencias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IUsuarioPersist _usuarioPersist;
        private readonly IEstoquePersist _estoquePersist;
        private readonly IRelatorioPersist _relatorioPersist;
        private readonly IMapper _mapper;

        public RelatorioService(IGeralPersist geralPersist, IEstoquePersist estoquePersist, IValidacoesPersist validacoesPersist, IRelatorioPersist relatorioPersist, IUsuarioPersist usuarioPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _usuarioPersist = usuarioPersist;
            _estoquePersist = estoquePersist;
            _relatorioPersist = relatorioPersist;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClienteDTO>> GetClientesAllAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var clientes = await _relatorioPersist.GetAllClientesAtivosInativosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (clientes == null)
                    {
                        throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                    }
                    else if (clientes.Count() == 0)
                    {
                        throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                    }
                    else
                    {
                        var resultadoClientes = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
                        return resultadoClientes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   

        public async Task<IEnumerable<ClienteDTO>> GetClientesAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var clientes = await _relatorioPersist.GetAllClientesAtivosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (clientes == null)
                {
                    throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                }
                else if (clientes.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoClientes = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
                    return resultadoClientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ClienteDTO>> GetClientesInativoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var clientes = await _relatorioPersist.GetAllClientesInativosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (clientes == null)
                {
                    throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                }
                else if (clientes.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoClientes = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
                    return resultadoClientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ClienteDTO>> GetClientesExcluidoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var clientes = await _relatorioPersist.GetAllClientesExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (clientes == null)
                {
                    throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                }
                else if (clientes.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.ClienteRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoClientes = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
                    return resultadoClientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorDTO>> GetFornecedoresAllAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var fornecedores = await _relatorioPersist.GetAllFornecedoresAtivosInativosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (fornecedores == null)
                    {
                        throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                    }
                    else if (fornecedores.Count() == 0)
                    {
                        throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                    }
                    else
                    {
                        var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);
                        return resultadoFornecedores;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorDTO>> GetFornecedoresAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var fornecedores = await _relatorioPersist.GetAllFornecedoresAtivosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (fornecedores == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                }
                else if (fornecedores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorDTO>> GetFornecedoresInativoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var fornecedores = await _relatorioPersist.GetAllFornecedoresInativosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (fornecedores == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                }
                else if (fornecedores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorDTO>> GetFornecedoresExcluidoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var fornecedores = await _relatorioPersist.GetAllFornecedoresExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (fornecedores == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                }
                else if (fornecedores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosAllAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresProdutosAllAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosAtivoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresProdutosAtivoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosInativoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresProdutosInativoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresProdutosExcluidoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresProdutosExcluidoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosAllAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresInativosProdutosAllAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosAtivoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresInativosProdutosAtivoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosInativoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresInativosProdutosInativoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresInativosProdutosExcluidoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresInativosProdutosExcluidoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosAllAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresExcluidosProdutosAllAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosAtivoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresExcluidosProdutosAtivoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosInativoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresExcluidosProdutosInativoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<FornecedorProdutoRelatorioDTO>> GetFornecedoresExcluidosProdutosExcluidoAsync(int empresaId)
        {
            try
            {
                var fornecedoresProdutos = await _relatorioPersist.GetFornecedoresExcluidosProdutosExcluidoAsync(empresaId);
                if (fornecedoresProdutos == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else if (fornecedoresProdutos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorProdutoNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedores = _mapper.Map<IEnumerable<FornecedorProdutoRelatorioDTO>>(fornecedoresProdutos);
                    return resultadoFornecedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAllAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var funcionarios = await _relatorioPersist.GetAllFuncionariosAtivosInativosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (funcionarios == null)
                    {
                        throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                    }
                    else if (funcionarios.Count() == 0)
                    {
                        throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                    }
                    else
                    {
                        var resultadoFuncionarios = _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
                        return resultadoFuncionarios;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var funcionarios = await _relatorioPersist.GetAllFuncionariosAtivosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (funcionarios == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                }
                else if (funcionarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoFuncionarios = _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
                    return resultadoFuncionarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosInativoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var funcionarios = await _relatorioPersist.GetAllFuncionariosInativosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (funcionarios == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                }
                else if (funcionarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoFuncionarios = _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
                    return resultadoFuncionarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosExcluidoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var funcionarios = await _relatorioPersist.GetAllFuncionariosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (funcionarios == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                }
                else if (funcionarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FuncionarioRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoFuncionarios = _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
                    return resultadoFuncionarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransportadorDTO>> GetTransportadoresAllAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var transportadores = await _relatorioPersist.GetAllTransportadoresAtivosInativosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (transportadores == null)
                    {
                        throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                    }
                    else if (transportadores.Count() == 0)
                    {
                        throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                    }
                    else
                    {
                        var resultadoTransportadores = _mapper.Map<IEnumerable<TransportadorDTO>>(transportadores);
                        return resultadoTransportadores;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransportadorDTO>> GetTransportadoresAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var transportadores = await _relatorioPersist.GetAllFuncionariosAtivosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (transportadores == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                }
                else if (transportadores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoTransportadores = _mapper.Map<IEnumerable<TransportadorDTO>>(transportadores);
                    return resultadoTransportadores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransportadorDTO>> GetTransportadoresInativoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var transportadores = await _relatorioPersist.GetAllTransportadoresInativosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (transportadores == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                }
                else if (transportadores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoTransportadores = _mapper.Map<IEnumerable<TransportadorDTO>>(transportadores);
                    return resultadoTransportadores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransportadorDTO>> GetTransportadoresExcluidoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var transportadores = await _relatorioPersist.GetAllTransportadoresExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (transportadores == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                }
                else if (transportadores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.TransportadorRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoTransportadores = _mapper.Map<IEnumerable<TransportadorDTO>>(transportadores);
                    return resultadoTransportadores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuarioAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var usuarios = await _relatorioPersist.GetAllUsuariosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (usuarios == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioRelatorioNaoEncontrado);
                }
                else if (usuarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.UsuarioRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoUsuarios = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
                    foreach (var item in resultadoUsuarios)
                    {
                        var permissoes = _validacoesPersist.PermissaoUsuarioId(item.EmpresaId, item.Id);
                        var permissoesDTO = _mapper.Map<List<PermissaoDTO>>(permissoes);
                        item.Permissoes = permissoesDTO;
                    }
                    return resultadoUsuarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EmpresaDTO>> GetEmpresasAllAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var empresas = await _relatorioPersist.GetAllEmpresasAtivosInativosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (empresas == null)
                    {
                        throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                    }
                    else if (empresas.Count() == 0)
                    {
                        throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                    }
                    else
                    {
                        var resultadoEmpresas = _mapper.Map<IEnumerable<EmpresaDTO>>(empresas);
                        return resultadoEmpresas;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EmpresaDTO>> GetEmpresasAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var empresas = await _relatorioPersist.GetAllEmpresasAtivosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (empresas == null)
                {
                    throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                }
                else if (empresas.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoEmpresas = _mapper.Map<IEnumerable<EmpresaDTO>>(empresas);
                    return resultadoEmpresas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EmpresaDTO>> GetEmpresasInativoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var empresas = await _relatorioPersist.GetAllEmpresasInativosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (empresas == null)
                {
                    throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                }
                else if (empresas.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoEmpresas = _mapper.Map<IEnumerable<EmpresaDTO>>(empresas);
                    return resultadoEmpresas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EmpresaDTO>> GetEmpresasExcluidoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var empresas = await _relatorioPersist.GetAllEmpresasExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (empresas == null)
                {
                    throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                }
                else if (empresas.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.EmpresaRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoEmpresas = _mapper.Map<IEnumerable<EmpresaDTO>>(empresas);
                    return resultadoEmpresas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosAllAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var produtos = await _relatorioPersist.GetAllProdutosAtivosInativosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (produtos == null)
                    {
                        throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                    }
                    else if (produtos.Count() == 0)
                    {
                        throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                    }
                    else
                    {
                        var resultadoProdutos = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
                        return resultadoProdutos;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var produtos = await _relatorioPersist.GetAllProdutosAtivosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (produtos == null)
                {
                    throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                }
                else if (produtos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoProdutos = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
                    return resultadoProdutos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosInativoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var produtos = await _relatorioPersist.GetAllProdutosInativosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (produtos == null)
                {
                    throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                }
                else if (produtos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoProdutos = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
                    return resultadoProdutos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosExcluidoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var produtos = await _relatorioPersist.GetAllProdutosExcluidosAsync(empresaId, dataInicioConvertida, dataFinalConvertida);
                if (produtos.Any())
                {
                    throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                }
                else
                {
                    var resultadoProdutos = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
                    return resultadoProdutos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var estoques = await _estoquePersist.GetAllEstoqueRelatorioAsync(empresaId);
                if (estoques == null)
                {
                    return null;
                }
                else
                {
                    List<EstoqueRelatorioDTO> EstoqueProdutoRetorno = new List<EstoqueRelatorioDTO>();
                    var estoqueProduto = _relatorioPersist.GetAllEstoqueRelatorio(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (estoqueProduto.Any())
                    {
                        foreach (var produtoEstoque in estoqueProduto)
                        {
                            EstoqueProdutoRetorno.Add(new EstoqueRelatorioDTO(produtoEstoque.Id, produtoEstoque.ProdutoId, produtoEstoque.ProdutoNome, produtoEstoque.Quantidade, produtoEstoque.StatusExclusao, produtoEstoque.DataCadastroEstoque));
                        }
                        return EstoqueProdutoRetorno;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesAtivoAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var estoques = await _estoquePersist.GetAllEstoqueRelatorioAsync(empresaId);
                if (estoques == null)
                {
                    return null;
                }
                else
                {
                    List<EstoqueRelatorioDTO> EstoqueProdutoRetorno = new List<EstoqueRelatorioDTO>();
                    var estoqueProduto = _relatorioPersist.GetAllEstoqueAtivoRelatorio(empresaId, dataInicioConvertida, dataFinalConvertida);                    
                    if (estoqueProduto.Any())
                    {
                        foreach (var produtoEstoque in estoqueProduto)
                        {
                            EstoqueProdutoRetorno.Add(new EstoqueRelatorioDTO(produtoEstoque.Id, produtoEstoque.ProdutoId, produtoEstoque.ProdutoNome, produtoEstoque.Quantidade, produtoEstoque.StatusExclusao, produtoEstoque.DataCadastroEstoque));
                        }
                        return EstoqueProdutoRetorno;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesInativosAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var estoques = await _estoquePersist.GetAllEstoqueRelatorioAsync(empresaId);
                if (estoques == null)
                {
                    return null;
                }
                else
                {
                    List<EstoqueRelatorioDTO> EstoqueProdutoRetorno = new List<EstoqueRelatorioDTO>();
                    var estoqueProduto = _relatorioPersist.GetAllEstoqueInativoRelatorio(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (estoqueProduto.Any())
                    {
                        foreach (var produtoEstoque in estoqueProduto)
                        {
                            EstoqueProdutoRetorno.Add(new EstoqueRelatorioDTO(produtoEstoque.Id, produtoEstoque.ProdutoId, produtoEstoque.ProdutoNome, produtoEstoque.Quantidade, produtoEstoque.StatusExclusao, produtoEstoque.DataCadastroEstoque));
                        }
                        return EstoqueProdutoRetorno;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EstoqueRelatorioDTO>> GetAllEstoquesExcluidosAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var estoques = await _estoquePersist.GetAllEstoqueRelatorioAsync(empresaId);
                if (estoques == null)
                {
                    return null;
                }
                else
                {
                    List<EstoqueRelatorioDTO> EstoqueProdutoRetorno = new List<EstoqueRelatorioDTO>();
                    var estoqueProduto = _relatorioPersist.GetAllEstoqueExcluidoRelatorio(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (estoqueProduto.Any())
                    {
                        foreach (var produtoEstoque in estoqueProduto)
                        {
                            EstoqueProdutoRetorno.Add(new EstoqueRelatorioDTO(produtoEstoque.Id, produtoEstoque.ProdutoId, produtoEstoque.ProdutoNome, produtoEstoque.Quantidade, produtoEstoque.StatusExclusao, produtoEstoque.DataCadastroEstoque));
                        }
                        return EstoqueProdutoRetorno;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.ProdutoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var pedidos = _relatorioPersist.GetAllPedidos(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (pedidos.Any())
                    {
                        var resultadoPedidos = _mapper.Map<IEnumerable<PedidoRetornoDTO>>(pedidos);
                        return resultadoPedidos;
                    }                    
                    else
                    {
                        throw new Exception(MensagemDeErro.PedidoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosEmAnaliseAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var pedidos = _relatorioPersist.GetAllPedidosEmAnalise(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (pedidos.Any())
                    {
                        var resultadoPedidos = _mapper.Map<IEnumerable<PedidoRetornoDTO>>(pedidos);
                        return resultadoPedidos;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.PedidoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosConfirmadosAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var pedidos = _relatorioPersist.GetAllPedidosConfirmados(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (pedidos.Any())
                    {
                        var resultadoPedidos = _mapper.Map<IEnumerable<PedidoRetornoDTO>>(pedidos);
                        return resultadoPedidos;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.PedidoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosCanceladosAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    var pedidos = _relatorioPersist.GetAllPedidosCancelados(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (pedidos.Any())
                    {
                        var resultadoPedidos = _mapper.Map<IEnumerable<PedidoRetornoDTO>>(pedidos);
                        return resultadoPedidos;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.PedidoRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<NotasFiscaisDTO>> GetAllNotasFiscaisAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    List<NotasFiscaisDTO> notasFiscaisList = new List<NotasFiscaisDTO>();
                    var notasFiscais = _relatorioPersist.GetAllNotasFiscais(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (notasFiscais.Any())
                    {
                        foreach (var item in notasFiscais)
                        {
                            notasFiscaisList.Add(new NotasFiscaisDTO(item.Id, item.PedidoId, item.NomeCliente, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, item.StatusNota, item.Status));
                        }
                        return notasFiscaisList;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.NotaFiscalRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<NotasFiscaisDTO>> GetAllNotasFiscaisAprovadasAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    List<NotasFiscaisDTO> notasFiscaisList = new List<NotasFiscaisDTO>();
                    var notasFiscais = _relatorioPersist.GetAllNotasFiscaisAprovadas(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (notasFiscais.Any())
                    {
                        foreach (var item in notasFiscais)
                        {
                            notasFiscaisList.Add(new NotasFiscaisDTO(item.Id, item.PedidoId, item.NomeCliente, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, item.StatusNota, item.Status));
                        }
                        return notasFiscaisList;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.NotaFiscalRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<NotasFiscaisDTO>> GetAllNotasFiscaisCanceladasAsync(int empresaId, string dataIncio, string dataFinal)
        {
            try
            {
                DateTime dataInicioConvertida = DateTime.ParseExact(dataIncio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dataFinalConvertida = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dataFinalConvertida < dataInicioConvertida)
                {
                    throw new Exception(MensagemDeErro.DataFinalMaiorFinal);
                }
                else
                {
                    List<NotasFiscaisDTO> notasFiscaisList = new List<NotasFiscaisDTO>();
                    var notasFiscais = _relatorioPersist.GetAllNotasFiscaisCanceladas(empresaId, dataInicioConvertida, dataFinalConvertida);
                    if (notasFiscais.Any())
                    {
                        foreach (var item in notasFiscais)
                        {
                            notasFiscaisList.Add(new NotasFiscaisDTO(item.Id, item.PedidoId, item.NomeCliente, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, item.StatusNota, item.Status));
                        }
                        return notasFiscaisList;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.NotaFiscalRelatorioNaoEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
