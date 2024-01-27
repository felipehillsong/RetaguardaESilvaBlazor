using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Application.Login;
using RetaguardaESilva.Application.PersistenciaService;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Data;

namespace RetaguardaESilva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IFuncionarioService _FuncionarioService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly ILoginService _loginService; 

        public LoginController(IFuncionarioService funcionarioService, IUsuarioService usuarioService, IValidacoesPersist validacoesPersist, ILoginService loginService)
        {
            _FuncionarioService = funcionarioService;
            _validacoesPersist = validacoesPersist;
            _loginService = loginService;
            _usuarioService = usuarioService;
        }

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult> Logar(Login model)
        {            
            try
            {
                var usuario = await _loginService.Login(model.Email, model.Senha);
                if (usuario == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(usuario);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/LoginAlteracao
        [HttpPost("{id}")]
        public async Task<ActionResult> AlterarLogin(int id, UsuarioLoginDTO model)
        {            
            try
            {
                var funcionario = await _FuncionarioService.GetFuncionarioByIdLoginAsync(id);
                var usuario = await _usuarioService.GetUsuarioByIdAlteraLoginAsync(id);
                if (funcionario == null || usuario == null)
                {
                    return NotFound();
                }
                else
                {
                    var usuarioRetorno = await _loginService.AlterarLogin(model.Email, model.Senha, model.Nome, funcionario, usuario);
                    if (usuarioRetorno == null)
                    {
                        return BadRequest();
                    }
                    return Ok(usuarioRetorno);
                }
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }
    }
}
