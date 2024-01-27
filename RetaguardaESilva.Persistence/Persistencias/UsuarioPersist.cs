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
    public class UsuarioPersist : IUsuarioPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public UsuarioPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }  

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync(int empresaId)
        {
            return await _context.Usuario.AsNoTracking().Where(u => u.EmpresaId == empresaId).OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<IEnumerable<Funcionario>> GetAllFuncionariosUsuariosAsync(int empresaId)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId).OrderBy(f => f.Id).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int empresaId, int usuarioId)
        {
            return await _context.Usuario.AsNoTracking().Where(u => u.Id == usuarioId && u.EmpresaId == empresaId).OrderBy(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<Funcionario> GetFuncionarioUsuarioByIdAsync(int empresaId, int funcionarioId)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.Id == funcionarioId && f.EmpresaId == empresaId).OrderBy(f => f.Id).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAlteraLoginAsync(int funcionarioId)
        {
            return await _context.Usuario.AsNoTracking().Where(u => u.FuncionarioId == funcionarioId).OrderBy(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<Permissao> GetPermissaoUsuarioByIdAsync(int empresaId, int usuarioId)
        {
            return await _context.Permissao.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.UsuarioId == usuarioId).OrderBy(p => p.Id).FirstOrDefaultAsync();
        }
        public Permissao GetPermissaoByIdAsync(int empresaId, int usuarioId, int permissaoId)
        {
            return _context.Permissao.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.UsuarioId == usuarioId && p.Id == permissaoId).OrderBy(p => p.Id).FirstOrDefault();          
        }
    }
}
