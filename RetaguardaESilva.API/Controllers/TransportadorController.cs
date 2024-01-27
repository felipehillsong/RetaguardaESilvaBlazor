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
    public class TransportadorController : ControllerBase
    {
        private readonly ITransportadorService _transportadorService;

        public TransportadorController(ITransportadorService transportadorService)
        {
            _transportadorService = transportadorService;
        }

        // GET: api/Transportador
        [HttpGet]
        public async Task<ActionResult> GetTransportadores(int empresaId)
        {            
            try
            {
                var transportadores = await _transportadorService.GetAllTransportadoresAsync(empresaId);
                if (transportadores == null)
                {
                    return NotFound();
                }
                return Ok(transportadores);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Transportador/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTransportador(int empresaId, int id)
        {
            try
            {
                var transportador = await _transportadorService.GetTransportadorByIdAsync(empresaId, id);
                if (transportador == null)
                {
                    return NotFound();
                }
                return Ok(transportador);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Transportador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostTransportador(TransportadorCreateDTO model)
        {
            try
            {
                var transportador = await _transportadorService.AddTransportador(model);
                if (transportador == null)
                {
                    return BadRequest();
                }
                return Ok(transportador);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Transportador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutTransportador(TransportadorUpdateDTO model)
        {
            try
            {
                var transportador = await _transportadorService.UpdateTransportador(model);
                if (transportador == null)
                {
                    return BadRequest();
                }
                return Ok(transportador);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Transportador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransportador(int empresaId, int id)
        {
            try
            {
                if (await _transportadorService.DeleteTransportador(empresaId, id))
                {
                    return Ok(new { message = MensagemDeSucesso.TransportadorDeletado });
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
