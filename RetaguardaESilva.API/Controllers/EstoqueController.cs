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
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        // GET: api/Estoque
        [HttpGet]
        public async Task<ActionResult> GetEstoque(int empresaId)
        {            
            try
            {
                var estoques = await _estoqueService.GetAllEstoquesAsync(empresaId);
                if (estoques == null)
                {
                    return NotFound();
                }
                return Ok(estoques);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Estoque/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEstoque(int empresaId, int id)
        {
            try
            {
                var estoque = await _estoqueService.GetEstoqueByIdAsync(empresaId, id);
                if (estoque == null)
                {
                    return NotFound();
                }
                return Ok(estoque);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Estoque/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEstoque(EstoqueDTO model)
        {
            try
            {
                var estoque = await _estoqueService.UpdateEstoque(model.EmpresaId, model.Id, model.Quantidade);
                if (estoque == null)
                {
                    return BadRequest();
                }
                return Ok(estoque);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Estoque/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoque(int empresaId, int id)
        {
            try
            {
                if (await _estoqueService.DeleteEstoque(empresaId, id))
                {
                    return Ok( new { message = MensagemDeSucesso.EstoqueDeletado });
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

        // GET: api/EnderecoProduto
        /// <summary>
        /// EnderecoProduto/GET
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("api/[controller]")]
        [ActionName("Thumbnail")]
        public async Task<ActionResult> GetEnderecoProduto(double empresaId)
        {
            int idEmpresa = (int)empresaId;
            try
            {
                var estoques = await _estoqueService.GetAllEnderecosProdutosAsync(idEmpresa);
                if (estoques == null)
                {
                    return NotFound();
                }
                return Ok(estoques);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/EnderecoProduto
        /// <summary>
        /// EnderecoProduto/GET/ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("api/id/[controller]")]
        public async Task<ActionResult> GetEnderecoProduto(double empresaId, double id)
        {
            int idEmpresa = (int)empresaId;
            int idEnderecoProduto = (int)id;
            try
            {
                var estoque = await _estoqueService.GetEnderecoProdutoByIdAsync(idEmpresa, idEnderecoProduto);
                if (estoque == null)
                {
                    return NotFound();
                }
                return Ok(estoque);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/EnderecoProduto
        /// <summary>
        /// EnderecoProduto/POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostEnderecoProduto(EnderecoProdutoCreateDTO model)
        {
            try
            {
                var cliente = await _estoqueService.AddEnderecoProduto(model);
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

        // PUT: api/EnderecoProduto
        /// <summary>
        /// EnderecoProduto/PUT
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/[controller]")]
        public async Task<IActionResult> PutEnderecoProduto(EnderecoProdutoUpdateDTO model)
        {
            try
            {
                var estoque = await _estoqueService.UpdateEnderecoProduto(model);
                if (estoque == null)
                {
                    return BadRequest();
                }
                return Ok(estoque);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/EnderecoProduto
        /// <summary>
        /// EnderecoProduto/DELETE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("api/[controller]")]
        public async Task<IActionResult> DeleteEnderecoProduto(int empresaId, int id)
        {
            try
            {
                if (await _estoqueService.DeleteEnderecoProduto(empresaId, id))
                {
                    return Ok(new { message = MensagemDeSucesso.EstoqueDeletado });
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
