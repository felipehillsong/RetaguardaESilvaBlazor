using AngleSharp.Dom;
using AutoMapper;
using ProEventos.Persistence.Persistencias;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
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
    public class LoginService : ILoginService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IUsuarioPersist _usuarioPersist;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IMapper _mapper;

        public LoginService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, IUsuarioService usuarioService, IUsuarioPersist usuarioPersist, IFuncionarioPersist funcionarioPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _usuarioPersist = usuarioPersist;
            _usuarioService = usuarioService;
            _validacoesPersist = validacoesPersist;
            _mapper = mapper;
        }

        public async Task<UsuarioLoginDTO> Login(string email, string senha)
        {
            try
            {
                var usuario = _validacoesPersist.Login(email, senha, out string mensagem);
                if (usuario == null)
                {
                    throw new Exception(mensagem);
                }
                else
                {
                    var permissoes = _usuarioPersist.GetPermissaoUsuarioByIdAsync(usuario.EmpresaId, usuario.Id);
                    if (permissoes.Result == null)
                    {
                        var usuarioSemPermissoes = _validacoesPersist.PermissaoUsuarioId(usuario.EmpresaId, usuario.Id);
                        var resultadoPermissao = _mapper.Map<List<PermissaoDTO>>(usuarioSemPermissoes);
                        var usuarioSemPermissaoRetorno = _mapper.Map<UsuarioLoginDTO>(usuario);
                        var usuarioNovoPermissao = new List<PermissaoDTO>();
                        foreach (var item in resultadoPermissao)
                        {
                            usuarioNovoPermissao.Add(item);
                        }
                        usuarioSemPermissaoRetorno.Permissoes = usuarioNovoPermissao;
                        return usuarioSemPermissaoRetorno;
                    }
                    else
                    {
                        var permissaoDTO = _mapper.Map<PermissaoDTO>(permissoes.Result);
                        var usuarioRetorno = _mapper.Map<UsuarioLoginDTO>(usuario);
                        var usuarioPermissao = new List<PermissaoDTO>();
                        usuarioPermissao.Add(permissaoDTO);
                        usuarioRetorno.Permissoes = usuarioPermissao;
                        return usuarioRetorno;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UsuarioLoginDTO> AlterarLogin(string email, string senha, string nome, FuncionarioDTO funcionarioView, UsuarioDTO usuarioView)
        {
            try
            {
                var funcionario = new Funcionario()
                {
                    Id = funcionarioView.Id,
                    Nome = nome,
                    Endereco = funcionarioView.Logradouro,
                    Bairro = funcionarioView.Bairro,
                    Numero = funcionarioView.Numero,
                    Municipio = funcionarioView.Localidade,
                    UF = funcionarioView.UF,
                    Pais = funcionarioView.Pais,
                    CEP = funcionarioView.CEP,
                    Complemento = funcionarioView.Complemento,
                    Telefone = funcionarioView.Telefone,
                    Email = email,
                    CPF = funcionarioView.CPF,
                    DataCadastroFuncionario = funcionarioView.DataCadastroFuncionario,
                    Ativo = funcionarioView.Ativo,
                    EmpresaId = funcionarioView.EmpresaId
                };
               
                _geralPersist.Update<Funcionario>(funcionario);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var usuario = new Usuario()
                    {
                        Id = usuarioView.Id,
                        Email = email,
                        Senha = senha,
                        DataCadastroUsuario = usuarioView.DataCadastroUsuario,
                        Ativo = usuarioView.Ativo,
                        FuncionarioId = funcionarioView.Id,
                        EmpresaId = usuarioView.EmpresaId
                    };
                    _geralPersist.Update<Usuario>(usuario);
                    if(await _geralPersist.SaveChangesAsync())
                    {
                        var permissoes = _usuarioPersist.GetPermissaoUsuarioByIdAsync(usuario.EmpresaId, usuario.Id);
                        var permissaoDTO = _mapper.Map<PermissaoDTO>(permissoes.Result);
                        var retornoUsuario = _validacoesPersist.Login(funcionario.Email, usuario.Senha, out string mensagem);
                        var usuarioRetorno = _mapper.Map<UsuarioLoginDTO>(retornoUsuario);
                        var usuarioPermissao = new List<PermissaoDTO>();
                        usuarioPermissao.Add(permissaoDTO);
                        usuarioRetorno.Permissoes = usuarioPermissao;
                        return usuarioRetorno;
                    }
                }
                
                throw new Exception(MensagemDeErro.ErroAoAtualizar);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
