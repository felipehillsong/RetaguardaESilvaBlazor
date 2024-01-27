using AutoMapper;
using Newtonsoft.Json;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Application.Helpers;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IFornecedorPersist _fornecedorPersist;
        private readonly IMapper _mapper;

        public FornecedorService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, IFornecedorPersist fornecedorPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _fornecedorPersist = fornecedorPersist;
            _mapper = mapper;
        }
        public async Task<FornecedorCreateDTO> AddFornecedor(FornecedorCreateDTO model)
        {
            try
            {   
                if (_validacoesPersist.ExisteFornecedor(model.EmpresaId, model.CNPJ, model.InscricaoMunicipal, model.InscricaoEstadual, model.Id, false, out string mensagem))
                {
                    throw new Exception(mensagem);
                }
                else
                {
                    model.Ativo = Convert.ToBoolean(Situacao.Ativo);
                    var fornecedorCreateDTO = _mapper.Map<Fornecedor>(model);
                    fornecedorCreateDTO.Endereco = model.Logradouro;
                    fornecedorCreateDTO.Municipio = model.Localidade;
                    fornecedorCreateDTO.Nome = _validacoesPersist.AcertarNome(fornecedorCreateDTO.Nome);
                    _geralPersist.Add<Fornecedor>(fornecedorCreateDTO);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        var retornoFornecedor = await _fornecedorPersist.GetFornecedorByIdAsync(fornecedorCreateDTO.EmpresaId, fornecedorCreateDTO.Id);
                        var resultadoFornecedor = _mapper.Map<FornecedorCreateDTO>(retornoFornecedor);
                        resultadoFornecedor.Logradouro = retornoFornecedor.Endereco;
                        resultadoFornecedor.Localidade = retornoFornecedor.Municipio;
                        return resultadoFornecedor;
                    }
                    throw new Exception(MensagemDeErro.ErroAoAtualizar);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FornecedorUpdateDTO> UpdateFornecedor(FornecedorUpdateDTO model)
        {
            try
            {
                var fornecedor = await _fornecedorPersist.GetFornecedorByIdAsync(model.EmpresaId, model.Id);
                if (fornecedor == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorNaoEcontradoUpdate);
                }
                else
                {
                    if (_validacoesPersist.ExisteFornecedor(model.EmpresaId, model.CNPJ, model.InscricaoMunicipal, model.InscricaoEstadual, model.Id, true, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    else
                    {
                        var fornecedorUpdateDTO = _mapper.Map<Fornecedor>(model);
                        fornecedorUpdateDTO.Endereco = model.Logradouro;
                        fornecedorUpdateDTO.Municipio = model.Localidade;
                        fornecedorUpdateDTO.Nome = _validacoesPersist.AcertarNome(fornecedorUpdateDTO.Nome);
                        _geralPersist.Update<Fornecedor>(fornecedorUpdateDTO);
                        if (await _geralPersist.SaveChangesAsync())
                        {
                            var retornoFornecedor = await _fornecedorPersist.GetFornecedorByIdAsync(fornecedorUpdateDTO.EmpresaId, fornecedorUpdateDTO.Id);
                            var resultadoFornecedor = _mapper.Map<FornecedorUpdateDTO>(retornoFornecedor);
                            resultadoFornecedor.Logradouro = retornoFornecedor.Endereco;
                            resultadoFornecedor.Localidade = retornoFornecedor.Municipio;
                            return resultadoFornecedor;
                        }
                        throw new Exception(mensagem);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFornecedor(int empresaId, int fornecedorId)
        {
            try
            {
                var fornecedor = await _fornecedorPersist.GetFornecedorByIdAsync(empresaId, fornecedorId);
                if (fornecedor == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorNaoEcontradoUpdate);
                }
                else
                {
                    fornecedor.StatusExclusao = Convert.ToBoolean(Situacao.Excluido);
                    fornecedor.Ativo = Convert.ToBoolean(Situacao.Inativo);
                    _geralPersist.Update<Fornecedor>(fornecedor);
                    return await _geralPersist.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FornecedorDTO>> GetAllFornecedoresAsync(int empresaId)
        {
            try
            {
                var fornecedores = await _fornecedorPersist.GetAllFornecedoresAsync(empresaId);
                if (fornecedores == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorNaoEncontrado);
                }
                else if (fornecedores.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FornecedorNaoEncontradoEmpresa);
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

        public async Task<FornecedorDTO> GetFornecedorByIdAsync(int empresaId, int fornecedorId)
        {
            try
            {
                var fornecedor = await _fornecedorPersist.GetFornecedorByIdAsync(empresaId, fornecedorId);
                if (fornecedor == null)
                {
                    throw new Exception(MensagemDeErro.FornecedorNaoEncontrado);
                }
                else
                {
                    var resultadoFornecedor = _mapper.Map<FornecedorDTO>(fornecedor);
                    resultadoFornecedor.Logradouro = fornecedor.Endereco;
                    resultadoFornecedor.Localidade = fornecedor.Municipio;
                    return resultadoFornecedor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
