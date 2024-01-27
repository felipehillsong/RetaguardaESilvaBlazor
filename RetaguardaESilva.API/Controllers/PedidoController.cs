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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult> GetPedidos(int empresaId)
        {
            try
            {
                var pedidos = await _pedidoService.GetAllPedidosAsync(empresaId);
                if (pedidos == null)
                {
                    return NotFound();
                }
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPedido(int empresaId, int id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoByIdAsync(empresaId, id);
                if (pedido == null)
                {
                    return NotFound();
                }
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Pedido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPedido(PedidoCreateDTO model)
        {
            try
            {
                var pedido = await _pedidoService.AddPedido(model);
                if (pedido == null)
                {
                    return BadRequest();
                }
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Pedido/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPedido(PedidoUpdateDTO model)
        {
            try
            {
                var pedido = await _pedidoService.UpdatePedido(model);
                if (pedido == null)
                {
                    return BadRequest();
                }
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int empresaId, int id)
        {
            try
            {
                if (await _pedidoService.DeletePedido(empresaId, id))
                {
                    return Ok( new { message = MensagemDeSucesso.PedidoDeletado });
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
