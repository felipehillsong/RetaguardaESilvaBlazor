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
    public class TransportadorService : ITransportadorService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly ITransportadorPersist _transportadorPersist;
        private readonly IMapper _mapper;

        public TransportadorService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, ITransportadorPersist transportadorPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _transportadorPersist = transportadorPersist;
            _mapper = mapper;
        }
        public async Task<TransportadorCreateDTO> AddTransportador(TransportadorCreateDTO model)
        {
            try
            {
                var transportador = _validacoesPersist.ExisteTransportador(model.EmpresaId, model.CNPJ, model.InscricaoMunicipal, model.InscricaoEstadual, model.Id, false, out string mensagem);
                if (transportador == true)
                {
                    throw new Exception(mensagem);
                }
                else
                {
                    model.Ativo = Convert.ToBoolean(Situacao.Ativo);
                    var transportadorCreateDTO = _mapper.Map<Transportador>(model);
                    transportadorCreateDTO.Endereco = model.Logradouro;
                    transportadorCreateDTO.Municipio = model.Localidade;
                    transportadorCreateDTO.Nome = _validacoesPersist.AcertarNome(transportadorCreateDTO.Nome);
                    _geralPersist.Add<Transportador>(transportadorCreateDTO);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        var retornoTransportador = await _transportadorPersist.GetTransportadorByIdAsync(transportadorCreateDTO.EmpresaId, transportadorCreateDTO.Id);
                        var resultadoTransportador = _mapper.Map<TransportadorCreateDTO>(retornoTransportador);
                        resultadoTransportador.Logradouro = retornoTransportador.Endereco;
                        resultadoTransportador.Localidade = retornoTransportador.Municipio;
                        return resultadoTransportador;
                    }
                    throw new Exception(mensagem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransportadorUpdateDTO> UpdateTransportador(TransportadorUpdateDTO model)
        {
            try
            {                
                var transportador = await _transportadorPersist.GetTransportadorByIdAsync(model.EmpresaId, model.Id);
                if (transportador == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorNaoEcontradoUpdate);
                }
                else
                {
                    if (_validacoesPersist.ExisteTransportador(model.EmpresaId, model.CNPJ, model.InscricaoMunicipal, model.InscricaoEstadual, model.Id, true, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    else
                    {
                        var transportadorUpdateDTO = _mapper.Map<Transportador>(model);
                        transportadorUpdateDTO.Endereco = model.Logradouro;
                        transportadorUpdateDTO.Municipio = model.Localidade;
                        transportadorUpdateDTO.Nome = _validacoesPersist.AcertarNome(transportadorUpdateDTO.Nome);
                        _geralPersist.Update<Transportador>(transportadorUpdateDTO);
                        if (await _geralPersist.SaveChangesAsync())
                        {
                            var retornoTransportador = await _transportadorPersist.GetTransportadorByIdAsync(transportadorUpdateDTO.EmpresaId, transportadorUpdateDTO.Id);
                            var resultadoTransportador = _mapper.Map<TransportadorUpdateDTO>(retornoTransportador);
                            resultadoTransportador.Logradouro = retornoTransportador.Endereco;
                            resultadoTransportador.Localidade = retornoTransportador.Municipio;
                            return resultadoTransportador;
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

        public async Task<bool> DeleteTransportador(int empresaId, int transportadorId)
        {
            try
            {
                var transportador = await _transportadorPersist.GetTransportadorByIdAsync(empresaId, transportadorId);
                if (transportador == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorNaoEncontradoDelete);
                }
                else
                {
                    _geralPersist.Delete<Transportador>(transportador);
                    return await _geralPersist.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransportadorDTO>> GetAllTransportadoresAsync(int empresaId)
        {
            try
            {
                var transportadoras = await _transportadorPersist.GetAllTransportadoresAsync(empresaId);
                if (transportadoras == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorNaoEncontrado);
                }
                else if (transportadoras.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.TransportadorNaoEncontradoEmpresa);
                }
                else
                {
                    var resultadoTransportadoras = _mapper.Map<IEnumerable<TransportadorDTO>>(transportadoras);
                    return resultadoTransportadoras;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        

        public async Task<TransportadorDTO> GetTransportadorByIdAsync(int empresaId, int transportadorId)
        {
            try
            {
                var transportador = await _transportadorPersist.GetTransportadorByIdAsync(empresaId, transportadorId);
                if (transportador == null)
                {
                    throw new Exception(MensagemDeErro.TransportadorNaoEncontrado);
                }
                else
                {
                    var resultadoTransportador = _mapper.Map<TransportadorDTO>(transportador);
                    resultadoTransportador.Logradouro = transportador.Endereco;
                    resultadoTransportador.Localidade = transportador.Municipio;
                    return resultadoTransportador;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
