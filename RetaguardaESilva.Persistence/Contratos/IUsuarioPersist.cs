using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IUsuarioPersist
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync(int empresaId);
        Task<IEnumerable<Funcionario>> GetAllFuncionariosUsuariosAsync(int empresaId);
        Task<Usuario> GetUsuarioByIdAsync(int empresaId, int usuarioId);
        Task<Funcionario> GetFuncionarioUsuarioByIdAsync(int empresaId, int funcionarioId);
        Task<Usuario> GetUsuarioByIdAlteraLoginAsync(int usuarioId);
        Task<Permissao> GetPermissaoUsuarioByIdAsync(int empresaId, int usuarioId);
        Permissao GetPermissaoByIdAsync(int empresaId, int usuarioId, int permissaoId);
    }
}