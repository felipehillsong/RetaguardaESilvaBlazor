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
    public class EmpresaService : IEmpresaService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IEmpresaPersist _empresaPersist;
        private readonly IMapper _mapper;

        public EmpresaService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, IEmpresaPersist empresaPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _empresaPersist = empresaPersist;
            _mapper = mapper;
        }
        public async Task<EmpresaCreateDTO> AddEmpresa(EmpresaCreateDTO model)
        {
            try
            {
                var empresa = _validacoesPersist.ExisteEmpresa(model.EmpresaId, model.Id, model.CNPJ, model.InscricaoMunicipal, model.InscricaoEstadual, false, out string mensagem);
                if (empresa == true)
                {
                    throw new Exception(mensagem);
                }
                else
                {
                    model.StatusExclusao = Convert.ToBoolean(Situacao.Inativo);
                    model.Ativo = Convert.ToBoolean(Situacao.Ativo);
                    var empresaCreateDTO = _mapper.Map<Empresa>(model);
                    empresaCreateDTO.Endereco = model.Logradouro;
                    empresaCreateDTO.Municipio = model.Localidade;
                    empresaCreateDTO.Nome = _validacoesPersist.AcertarNome(empresaCreateDTO.Nome);
                    _geralPersist.Add<Empresa>(empresaCreateDTO);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        var retornoEmpresa = await _empresaPersist.GetEmpresaByIdAsync(empresaCreateDTO.Id);
                        var resultadoEmpresa = _mapper.Map<EmpresaCreateDTO>(retornoEmpresa);
                        resultadoEmpresa.Logradouro = retornoEmpresa.Endereco;
                        resultadoEmpresa.Localidade = retornoEmpresa.Municipio;
                        return resultadoEmpresa;
                    }                    
                    throw new Exception(MensagemDeErro.ErroSalvarEmpresa);
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmpresaUpdateDTO> UpdateEmpresa(EmpresaUpdateDTO model)
        {
            try
            {
                var empresa = await _empresaPersist.GetEmpresaByIdAsync(model.Id);
                if (empresa == null)
                {
                    return null;
                }
                else
                {
                    if (_validacoesPersist.ExisteEmpresa(model.EmpresaId, model.Id, model.CNPJ, model.InscricaoMunicipal, model.InscricaoEstadual, true, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    else
                    {
                        model.StatusExclusao = Convert.ToBoolean(Situacao.Inativo);
                        var empresaUpdateDTO = _mapper.Map<Empresa>(model);
                        empresaUpdateDTO.Endereco = model.Logradouro;
                        empresaUpdateDTO.Municipio = model.Localidade;
                        empresaUpdateDTO.Nome = _validacoesPersist.AcertarNome(empresaUpdateDTO.Nome);
                        _geralPersist.Update(empresaUpdateDTO);
                        if (await _geralPersist.SaveChangesAsync())
                        {
                            var retornoEmpresa = await _empresaPersist.GetEmpresaByIdAsync(empresaUpdateDTO.Id);
                            var resultadoEmpresa = _mapper.Map<EmpresaUpdateDTO>(retornoEmpresa);
                            resultadoEmpresa.Logradouro = retornoEmpresa.Endereco;
                            resultadoEmpresa.Localidade = retornoEmpresa.Municipio;
                            return resultadoEmpresa;
                        }
                        throw new Exception(MensagemDeErro.ErroAtualizarEmpresa);
                    }   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEmpresa(int id)
        {
            try
            {
                var empresa = await _empresaPersist.GetEmpresaByIdAsync(id);
                if (empresa == null)
                {
                    throw new Exception(MensagemDeErro.EmpresaNaoEncontradaParaDelete);
                }
                else
                {
                    empresa.StatusExclusao = Convert.ToBoolean(Situacao.Excluido);
                    empresa.Ativo = Convert.ToBoolean(Situacao.Inativo);
                    _geralPersist.Update<Empresa>(empresa);
                    return await _geralPersist.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EmpresaDTO>> GetAllEmpresasAsync()
        {
            try
            {
                var empresas = await _empresaPersist.GetAllEmpresasAsync();
                if (empresas == null)
                {
                    throw new Exception(MensagemDeErro.EmpresaNaoEncontrada);
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

        public async Task<EmpresaDTO> GetEmpresaByIdAsync(int id)
        {
            try
            {
                var empresa = await _empresaPersist.GetEmpresaByIdAsync(id);
                if (empresa == null)
                {
                    throw new Exception(MensagemDeErro.EmpresaNaoEncontrada);
                }
                else
                {
                    var resultadoEmpresa = _mapper.Map<EmpresaDTO>(empresa);
                    resultadoEmpresa.Logradouro = empresa.Endereco;
                    resultadoEmpresa.Localidade = empresa.Municipio;
                    return resultadoEmpresa;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
