using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;

namespace RetaguardaESilva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        // GET: api/Fornecedor
        [HttpGet]
        public async Task<ActionResult> GetFornecedores(int empresaId)
        {            
            try
            {
                var fornecedores = await _fornecedorService.GetAllFornecedoresAsync(empresaId);
                if (fornecedores == null)
                {
                    return NotFound();
                }
                return Ok(fornecedores);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Fornecedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFornecedor(int empresaId, int id)
        {
            try
            {
                var fornecedor = await _fornecedorService.GetFornecedorByIdAsync(empresaId, id);
                if (fornecedor == null)
                {
                    return NotFound();
                }
                return Ok(fornecedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Fornecedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFornecedor(FornecedorCreateDTO model)
        {
            try
            {
                var fornecedor = await _fornecedorService.AddFornecedor(model);
                if (fornecedor == null)
                {
                    return BadRequest();
                }
                return Ok(fornecedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Fornecedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutFornecedor(FornecedorUpdateDTO model)
        {
            try
            {
                var fornecedor = await _fornecedorService.UpdateFornecedor(model);
                if (fornecedor == null)
                {
                    return BadRequest();
                }
                return Ok(fornecedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Fornecedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(int empresaId, int id)
        {
            try
            {
                if (await _fornecedorService.DeleteFornecedor(empresaId, id))
                {
                    return Ok(new { message = MensagemDeSucesso.FornecedorDeletado });
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
