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
using RetaguardaESilva.Persistence.Data;

namespace RetaguardaESilva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult> GetClientes(int empresaId)
        {            
            try
            {
                var clientes = await _clienteService.GetAllClientesAsync(empresaId);
                if (clientes == null)
                {
                    return NotFound();
                }
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }        

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCliente(int empresaId, int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(empresaId, id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCliente(ClienteCreateDTO model)
        {
            try
            {
                var cliente = await _clienteService.AddCliente(model);
                if (cliente == null)
                {
                    return BadRequest();
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCliente(ClienteUpdateDTO model)
        {
            try
            {
                var cliente = await _clienteService.UpdateCliente(model);
                if (cliente == null)
                {
                    return BadRequest();
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }        

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int empresaId, int id)
        {
            try
            {
                if (await _clienteService.DeleteCliente(empresaId, id))
                {
                    return Ok( new { message = MensagemDeSucesso.ClienteDeletado });
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
