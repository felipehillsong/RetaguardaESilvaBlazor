using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;
using RetaguardaESilva.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Persistencias
{
    public class FuncionarioPersist : IFuncionarioPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public FuncionarioPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }  

        public async Task<IEnumerable<Funcionario>> GetAllFuncionariosAsync(int empresaId)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId).OrderBy(f => f.Id).ToListAsync();            
        }

        public async Task<Funcionario> GetFuncionarioByIdAsync(int empresaId, int funcionarioId)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.Id == funcionarioId && f.EmpresaId == empresaId).OrderBy(f => f.Id).FirstOrDefaultAsync();
        }

        public async Task<Funcionario> GetFuncionarioLoginByIdAsync(int funcionarioId)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.Id == funcionarioId).OrderBy(f => f.Id).FirstOrDefaultAsync();
        }
    }
}
