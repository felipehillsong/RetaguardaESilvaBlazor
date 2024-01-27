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
using RetaguardaESilva.Persistence.Data;

namespace RetaguardaESilva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _notaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        // GET: api/NotaFiscal
        [HttpGet]
        public async Task<ActionResult> GetNotaFiscal(int empresaId)
        {
            try
            {
                var notasFiscais = await _notaFiscalService.GetAllNotaFiscalAsync(empresaId);
                if (notasFiscais == null)
                {
                    return NotFound();
                }
                return Ok(notasFiscais);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/NotaFiscal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostNotaFiscal(NotaFiscalDTO model)
        {
            try
            {
                var notaFiscal = await _notaFiscalService.AddNotaFiscal(model);
                if (notaFiscal == null)
                {
                    return BadRequest();
                }
                return Ok(notaFiscal);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/NotaFiscal/5
        [HttpGet("api/[controller]/{id}")]
        public async Task<ActionResult> GetNotaFiscalId(int empresaId, int id, bool? notaFiscalEmissao, bool? exclusao)
        {  
            try
            {
                var notaFiscal = await _notaFiscalService.GetNotaFiscalByIdAsync(empresaId, id, notaFiscalEmissao, exclusao);
                if (notaFiscal == null)
                {
                    return NotFound();
                }
                return Ok(notaFiscal);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/NotaFiscal/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetNotaFiscalPedido(int empresaId, int id)
        {
            try
            {
                var notaPedido = await _notaFiscalService.GetNotaFiscalPedidoByIdAsync(empresaId, id);
                if (notaPedido == null)
                {
                    return NotFound();
                }
                return Ok(notaPedido);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/NotaFiscal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelarNotaFiscal(int empresaId, int id)
        {
            try
            {
                var notaFiscal = await _notaFiscalService.CancelarNotaFiscal(empresaId, id);
                if(notaFiscal == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(new { message = MensagemDeSucesso.NotaFiscalCancelada, notaFiscal });
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }
    }
}
