using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Application.PersistenciaService;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;

namespace RetaguardaESilva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        /// <summary>
        /// Traz todos os usuarios
        /// </summary>
        /// <param name="empresaId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetUsuarios(int empresaId)
        {            
            try
            {
                var usuarios = await _usuarioService.GetAllUsuariosAsync(empresaId);
                if (usuarios == null)
                {
                    return NotFound();
                }
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Usuario
        /// <summary>
        /// Traz os funcionarios que ainda não são usuarios
        /// </summary>
        /// <param name="empresaId"></param>
        /// <returns></returns>
        [HttpGet("api/[controller]")]
        public async Task<ActionResult> GetFuncionariosUsuarios(double empresaId)
        {
            try
            {
                int idEmpresa = (int)empresaId;
                var funcionariosUsuarios = await _usuarioService.GetAllFuncionariosUsuariosAsync(idEmpresa);
                if (funcionariosUsuarios == null)
                {
                    return NotFound();
                }
                return Ok(funcionariosUsuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Usuario/5
        /// <summary>
        /// Traz o usuario com as pemissões por id
        /// </summary>
        /// <param name="empresaId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuario(int empresaId, int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(empresaId, id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Usuario/5
        /// <summary>
        /// Traz o funcionario para incluir como usuario
        /// </summary>
        /// <param name="empresaId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("api/[controller]/{id}")]
        public async Task<ActionResult> GetFuncionarioUsuario(double empresaId, double id)
        {
            int idEmpresa = (int)empresaId;
            int idFuncionario = (int)id;
            try
            {
                var usuario = await _usuarioService.GetFuncionarioUsuarioByIdAsync(idEmpresa, idFuncionario);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Usuario
        /// <summary>
        /// Cria um usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUsuario(UsuarioCreateDTO model)
        {
            try
            {
                var usuario = await _usuarioService.AddUsuario(model);
                if (usuario == null)
                {
                    return BadRequest();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Usuario/5
        /// <summary>
        /// Atualiza um usuario pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUsuario(UsuarioUpdateDTO model)
        {
            try
            {
                var usuario = await _usuarioService.UpdateUsuario(model);
                if (usuario == null)
                {
                    return BadRequest();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Usuario/5
        /// <summary>
        /// Deleta um usuario pelo id
        /// </summary>
        /// <param name="empresaId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int empresaId, int id)
        {
            try
            {
                if (await _usuarioService.DeleteUsuario(empresaId, id))
                {
                    return Ok(new { message = MensagemDeSucesso.UsuarioDeletado });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }      
    }
}
