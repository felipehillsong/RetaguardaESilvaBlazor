using AngleSharp.Dom;
using AutoMapper;
using Correios.NET.Models;
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IUsuarioPersist _usuarioPersist;
        private readonly IMapper _mapper;

        public UsuarioService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, IUsuarioPersist usuarioPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _usuarioPersist = usuarioPersist;
            _mapper = mapper;
        }
        public async Task<UsuarioCreateDTO> AddUsuario(UsuarioCreateDTO model)
        {
            string mensagem = "";
            model.Email = _validacoesPersist.AcertarNome(model.Email);
            model.Senha = _validacoesPersist.AcertarNome(model.Senha);
            
            if (_validacoesPersist.ExisteUsuario(model.EmpresaId, model.Id, model.Email, false, out mensagem))
            {
                throw new Exception(mensagem);
            }
            else
            {
                model.Ativo = Convert.ToBoolean(Situacao.Ativo);
                var usuarioCreateDTO = _mapper.Map<Usuario>(model);
                _geralPersist.Add<Usuario>(usuarioCreateDTO);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var retornoUsuario = await _usuarioPersist.GetUsuarioByIdAsync(usuarioCreateDTO.EmpresaId, usuarioCreateDTO.Id);
                    return _mapper.Map<UsuarioCreateDTO>(retornoUsuario);
                }
                throw new Exception(mensagem);
            }
        }

        public async Task<UsuarioUpdateDTO> UpdateUsuario(UsuarioUpdateDTO model)
        {
            try
            {
                var usuario = await _usuarioPersist.GetUsuarioByIdAsync(model.EmpresaId, model.Id);
                var funcionario = await _usuarioPersist.GetFuncionarioUsuarioByIdAsync(model.EmpresaId, model.FuncionarioId);
                if (usuario == null || funcionario == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontradoUpdate);
                }
                else
                {
                    string mensagem = "";
                    model.Email = _validacoesPersist.AcertarNome(model.Email);
                    model.Senha = _validacoesPersist.AcertarNome(model.Senha);

                    if (_validacoesPersist.ExisteUsuario(model.EmpresaId, model.Id, model.Email, true, out mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    else
                    {
                        var usuarioUpdateDTO = _mapper.Map<Usuario>(model);
                        _geralPersist.Update<Usuario>(usuarioUpdateDTO);
                        if (await _geralPersist.SaveChangesAsync())
                        {
                            var funcionarioUpdate = new Funcionario()
                            {
                                Id = model.FuncionarioId,
                                Nome = model.Nome,
                                Endereco = funcionario.Endereco,
                                Bairro = funcionario.Bairro,
                                Numero = funcionario.Numero,
                                Municipio = funcionario.Municipio,
                                UF = funcionario.UF,
                                Pais = funcionario.Pais,
                                CEP = funcionario.CEP,
                                Complemento = funcionario.Complemento,
                                Telefone = funcionario.Telefone,
                                Email = model.Email,
                                CPF = funcionario.CPF,
                                DataCadastroFuncionario = funcionario.DataCadastroFuncionario,
                                Ativo = funcionario.Ativo,
                                EmpresaId = model.EmpresaId
                            };
                            _geralPersist.Update<Funcionario>(funcionarioUpdate);
                            if (await _geralPersist.SaveChangesAsync())
                            {
                                var permissoes = model.Permissoes;
                                var permissoesUsuario = UpdatePermissao(permissoes, out string mensagemRetorno);
                                if (mensagemRetorno == MensagemDeSucesso.UsuarioSemPermissao)
                                {
                                    _geralPersist.Add<Permissao>(permissoesUsuario);
                                    if (await _geralPersist.SaveChangesAsync())
                                    {
                                        var usuarioRetorno = await GetUsuarioByIdAsync(model.EmpresaId, model.Id);
                                        var usuarioUpdate = _mapper.Map<UsuarioUpdateDTO>(usuarioRetorno);
                                        return usuarioUpdate;
                                    }
                                }
                                else if (mensagemRetorno == MensagemDeSucesso.UsuarioDadosDiferente)
                                {
                                    _geralPersist.Update<Permissao>(permissoesUsuario);
                                    if (await _geralPersist.SaveChangesAsync())
                                    {
                                        var usuarioRetorno = await GetUsuarioByIdAsync(model.EmpresaId, model.Id);
                                        var usuarioUpdate = _mapper.Map<UsuarioUpdateDTO>(usuarioRetorno);
                                        return usuarioUpdate;
                                    }
                                }
                                else if (mensagemRetorno == MensagemDeSucesso.UsuarioMesmoDado)
                                {
                                    var usuarioRetorno = await GetUsuarioByIdAsync(model.EmpresaId, model.Id);
                                    var usuarioUpdate = _mapper.Map<UsuarioUpdateDTO>(usuarioRetorno);
                                    return usuarioUpdate;
                                }
                            }
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

        public async Task<bool> DeleteUsuario(int empresaId, int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.GetUsuarioByIdAsync(empresaId, usuarioId);
                if (usuario == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontradoDelete);
                }
                else
                {
                    _geralPersist.Delete<Usuario>(usuario);
                    if(await _geralPersist.SaveChangesAsync())
                    {
                        var permissao = await _usuarioPersist.GetPermissaoUsuarioByIdAsync(empresaId, usuarioId);
                        if(permissao != null)
                        {
                            _geralPersist.Delete<Permissao>(permissao);
                            return await _geralPersist.SaveChangesAsync();
                        }

                        return true;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.UsuarioErroDelete);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UsuariosRetornoDTO>> GetAllUsuariosAsync(int empresaId)
        {
            try
            {
                var usuarios = await _usuarioPersist.GetAllUsuariosAsync(empresaId);   
                if (usuarios == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontrado);
                }
                else if (usuarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.UsuariosNaoEncontradoEmpresa);
                }
                else
                {
                    var resultadoUsuarios = _validacoesPersist.RetornarUsuario(empresaId);
                    var ListaUsuarios = _mapper.Map<IEnumerable<UsuariosRetornoDTO>>(resultadoUsuarios);
                    return ListaUsuarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetAllFuncionariosUsuariosAsync(int empresaId)
        {
            try
            {
                var funcionariosUsuarios = await _usuarioPersist.GetAllFuncionariosUsuariosAsync(empresaId);
                if (funcionariosUsuarios == null)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontrado);
                }
                else if (funcionariosUsuarios.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.FuncionarioNaoEncontradoEmpresa);
                }
                else
                {
                    var resultadoUsuarios = _validacoesPersist.RetornarFuncionariosUsuario(empresaId);
                    var listaFuncionariosUsuarios = _mapper.Map<IEnumerable<FuncionarioDTO>>(resultadoUsuarios);
                    return listaFuncionariosUsuarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int empresaId, int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.GetUsuarioByIdAsync(empresaId, usuarioId);
                if (usuario == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontrado);
                }
                else
                {
                    var retornoUsuario = _validacoesPersist.RetornarUsuarioId(usuario.Id);
                    var resultadoUsuario = _mapper.Map<UsuarioDTO>(retornoUsuario);
                    var usuarioPermissao = _validacoesPersist.PermissaoUsuarioId(empresaId, usuarioId);
                    var resultadoPermissao = _mapper.Map<List<PermissaoDTO>>(usuarioPermissao);
                    resultadoUsuario.Permissoes = resultadoPermissao;
                    return resultadoUsuario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDTO> GetFuncionarioUsuarioByIdAsync(int empresaId, int funcionarioId)
        {
            try
            {
                var funcionarioUsuario = await _usuarioPersist.GetFuncionarioUsuarioByIdAsync(empresaId, funcionarioId);
                if (funcionarioUsuario == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontrado);
                }
                else
                {
                    var resultadoUsuario = _mapper.Map<FuncionarioDTO>(funcionarioUsuario);
                    return resultadoUsuario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAlteraLoginAsync(int funcionarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.GetUsuarioByIdAlteraLoginAsync(funcionarioId);
                if (usuario == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontrado);
                }
                else
                {
                    var resultadoUsuario = _mapper.Map<UsuarioDTO>(usuario);
                    return resultadoUsuario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Permissao UpdatePermissao(List<PermissaoDTO> model, out string mensagem)
        {
            try
            {
                var permissao = _usuarioPersist.GetPermissaoByIdAsync(model[0].EmpresaId, model[0].UsuarioId, model[0].Id);
                if (permissao == null)
                {
                    var permissaoModel = _mapper.Map<Permissao>(model[0]);
                    var permissoes = _validacoesPersist.PreenchePermissoes(permissaoModel);
                    mensagem = MensagemDeSucesso.UsuarioSemPermissao;
                    return permissoes;
                }
                else
                {
                    var permissaoModel = _mapper.Map<Permissao>(model[0]);
                    var permissoes = _validacoesPersist.VerificaPermissoes(permissao, permissaoModel);
                    if (permissoes)
                    {
                        mensagem = MensagemDeSucesso.UsuarioMesmoDado;
                        return permissao;
                    }
                    else
                    {
                        var permissoesRetorno = _validacoesPersist.PreenchePermissoes(permissaoModel);
                        mensagem = MensagemDeSucesso.UsuarioDadosDiferente;
                        return permissoesRetorno;
                    }
                   
                    throw new Exception(MensagemDeErro.ErroAoAtualizarCriar);
                }

                throw new Exception(MensagemDeErro.ErroAoAtualizarCriar);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
