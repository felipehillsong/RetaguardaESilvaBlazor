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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // GET: api/Produto
        [HttpGet]
        public async Task<ActionResult> GetProdutos(int empresaId)
        {            
            try
            {
                var produtos = await _produtoService.GetAllProdutosAsync(empresaId);
                if (produtos == null)
                {
                    return NotFound();
                }
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Fornecedor
        [HttpGet("api/Fornecedores")]
        public async Task<ActionResult> GetFornecedoresAtivos(int empresaId)
        {
            try
            {
                var fornecedores = await _produtoService.GetAllFornecedoresAsync(empresaId);
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

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduto(int empresaId, int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoByIdAsync(empresaId, id);
                if (produto == null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST: api/Produto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostProduto(ProdutoCreateDTO model)
        {
            try
            {
                var produto = await _produtoService.AddProduto(model);
                if (produto == null)
                {
                    return BadRequest();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Produto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProduto(ProdutoUpdateDTO model)
        {
            try
            {
                var produto = await _produtoService.UpdateProduto(model);
                if (produto == null)
                {
                    return BadRequest();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int empresaId, int id)
        {
            try
            {
                if (await _produtoService.DeleteProduto(empresaId, id))
                {
                    return Ok(new { message = MensagemDeSucesso.ProdutoDeletado });
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
