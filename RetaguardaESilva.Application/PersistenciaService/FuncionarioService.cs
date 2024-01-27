using AutoMapper;
using Newtonsoft.Json;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Application.Helpers;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IFuncionarioPersist _funcionarioPersist;
        private readonly IMapper _mapper;

        public FuncionarioService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, IFuncionarioPersist funcionarioPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _funcionarioPersist = funcionarioPersist;
            _mapper = mapper;
        }
        public async Task<FuncionarioCreateDTO> AddFuncionario(FuncionarioCreateDTO model)
        {   
            if (_validacoesPersist.ExisteFuncionario(model.EmpresaId, model.CPF, model.Id, false, out string mensagem))
            {
                throw new Exception(mensagem);
            }
            else
            {
                model.Ativo = Convert.ToBoolean(Situacao.Ativo);
                var funcionarioCreateDTO = _mapper.Map<Funcionario>(model);
                funcionarioCreateDTO.Endereco = model.Logradouro;
                funcionarioCreateDTO.Municipio = model.Localidade;
                funcionarioCreateDTO.Nome = _validacoesPersist.AcertarNome(funcionarioCreateDTO.Nome);
                _geralPersist.Add<Funcionario>(funcionarioCreateDTO);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var retornoFuncionario = await _funcionarioPersist.GetFuncionarioByIdAsync(funcionarioCreateDTO.EmpresaId, funcionarioCreateDTO.Id);
                    var resultadoFuncionario = _mapper.Map<FuncionarioCreateDTO>(retornoFuncionario);
                    resultadoFuncionario.Logradouro = retornoFuncionario.Endereco;
                    resultadoFuncionario.Localidade = retornoFuncionario.Municipio;
                    return resultadoFuncionario;
                }
                throw new Exception(mensagem);
            }
        }

        public async Task<FuncionarioUpdateDTO> UpdateFuncionario(FuncionarioUpdateDTO model)
        {
            try
            {
                var funcionario = await _funcionarioPersist.GetFuncionarioByIdAsync(model.EmpresaId, model.Id);
                if (funcionario == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontradoUpdate);
                }
                else
                {
                    if (_validacoesPersist.ExisteFuncionario(model.EmpresaId, model.CPF, model.Id, true, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    else
                    {
                        var funcionarioUptadeDTO = _mapper.Map<Funcionario>(model);
                        funcionarioUptadeDTO.Endereco = model.Logradouro;
                        funcionarioUptadeDTO.Municipio = model.Localidade;
                        funcionarioUptadeDTO.Nome = _validacoesPersist.AcertarNome(funcionarioUptadeDTO.Nome);
                        _geralPersist.Update<Funcionario>(funcionarioUptadeDTO);
                        if (await _geralPersist.SaveChangesAsync())
                        {
                            var retornoFuncionario = await _funcionarioPersist.GetFuncionarioByIdAsync(funcionarioUptadeDTO.EmpresaId, funcionarioUptadeDTO.Id);
                            var resultadoFuncionario = _mapper.Map<FuncionarioUpdateDTO>(retornoFuncionario);
                            resultadoFuncionario.Logradouro = retornoFuncionario.Endereco;
                            resultadoFuncionario.Localidade = retornoFuncionario.Municipio;
                            return resultadoFuncionario;
                        }
                        throw new Exception(MensagemDeErro.ErroAoAtualizar);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFuncionario(int empresaId, int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioPersist.GetFuncionarioByIdAsync(empresaId, funcionarioId);
                if (funcionario == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontradoDelete);
                }
                else
                {
                    _geralPersist.Delete<Funcionario>(funcionario);
                    return await _geralPersist.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetAllFuncionariosAsync(int empresaId)
        {
            try
            {
                var funcionarios = await _funcionarioPersist.GetAllFuncionariosAsync(empresaId);
                if (funcionarios == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontrado);
                }
                else if (funcionarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontradoEmpresa);
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

        public async Task<FuncionarioDTO> GetFuncionarioByIdAsync(int empresaId, int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioPersist.GetFuncionarioByIdAsync(empresaId, funcionarioId);
                if (funcionario == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontrado);
                }
                else
                {
                    var resultadoFuncionario = _mapper.Map<FuncionarioDTO>(funcionario);
                    resultadoFuncionario.Logradouro = funcionario.Endereco;
                    resultadoFuncionario.Localidade = funcionario.Municipio;
                    return resultadoFuncionario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDTO> GetFuncionarioByIdLoginAsync(int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioPersist.GetFuncionarioLoginByIdAsync(funcionarioId);
                if (funcionario == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontrado);
                }
                else
                {
                    var resultadoFuncionario = _mapper.Map<FuncionarioDTO>(funcionario);
                    resultadoFuncionario.Logradouro = funcionario.Endereco;
                    resultadoFuncionario.Localidade = funcionario.Municipio;
                    return resultadoFuncionario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
