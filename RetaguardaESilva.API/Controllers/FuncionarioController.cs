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
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _FuncionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _FuncionarioService = funcionarioService;
        }

        // GET: api/Funcionario
        [HttpGet]
        public async Task<ActionResult> GetFuncionarios(int empresaId)
        {            
            try
            {
                var funcionarios= await _FuncionarioService.GetAllFuncionariosAsync(empresaId);
                if (funcionarios == null)
                {
                    return NotFound();
                }
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Funcionario/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFuncionario(int empresaId, int id)
        {
            try
            {
                var funcionario = await _FuncionarioService.GetFuncionarioByIdAsync(empresaId, id);
                if (funcionario == null)
                {
                    return NotFound();
                }
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Funcionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFuncionario(FuncionarioCreateDTO model)
        {
            try
            {
                var funcionario = await _FuncionarioService.AddFuncionario(model);
                if (funcionario == null)
                {
                    return BadRequest();
                }
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Funcionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutFuncionario(FuncionarioUpdateDTO model)
        {
            try
            {
                var funcionario = await _FuncionarioService.UpdateFuncionario(model);
                if (funcionario == null)
                {
                    return BadRequest();
                }
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Funcionario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int empresaId, int id)
        {
            try
            {
                if (await _FuncionarioService.DeleteFuncionario(empresaId, id))
                {
                    return Ok(new { message = MensagemDeSucesso.FuncionarioDeletado });
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
