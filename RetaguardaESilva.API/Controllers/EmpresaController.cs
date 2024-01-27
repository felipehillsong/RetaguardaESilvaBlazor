#nullable disable
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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        // GET: api/Empresa/5
        [HttpGet]
        public async Task<ActionResult> GetEmpresas()
        {
            try
            {
                var empresa = await _empresaService.GetAllEmpresasAsync();
                if (empresa == null)
                {
                    return NotFound();
                }
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET: api/Empresa/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmpresa(int id)
        {
            try
            {
                var empresa = await _empresaService.GetEmpresaByIdAsync(id);
                if (empresa == null)
                {
                    return NotFound();
                }
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }
        
        // POST: api/Empresa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostEmpresa(EmpresaCreateDTO model)
        {
            try
            {
                var empresa = await _empresaService.AddEmpresa(model);
                if (empresa == null)
                {
                    return BadRequest();
                }
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // PUT: api/Empresa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEmpresa(EmpresaUpdateDTO model)
        {
            try
            {
                var empresa = await _empresaService.UpdateEmpresa(model);
                if (empresa == null)
                {
                    return BadRequest();
                }
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }


        // DELETE: api/Empresa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            try
            {
                if (await _empresaService.DeleteEmpresa(id))
                {
                    return Ok(new { message = MensagemDeSucesso.EmpresaDeletada });                    
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
