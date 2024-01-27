using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IUsuarioService
    {
        Task<UsuarioCreateDTO> AddUsuario(UsuarioCreateDTO model);
        Task<UsuarioUpdateDTO> UpdateUsuario(UsuarioUpdateDTO model);
        Task<bool> DeleteUsuario(int empresaId, int usuarioId);
        Task<IEnumerable<UsuariosRetornoDTO>> GetAllUsuariosAsync(int empresaId);
        Task<IEnumerable<FuncionarioDTO>> GetAllFuncionariosUsuariosAsync(int empresaId);
        Task<UsuarioDTO> GetUsuarioByIdAsync(int empresaId, int usuarioId);
        Task<FuncionarioDTO> GetFuncionarioUsuarioByIdAsync(int empresaId, int funcionarioId);
        Task<UsuarioDTO> GetUsuarioByIdAlteraLoginAsync(int funcionarioId);
        Permissao UpdatePermissao(List<PermissaoDTO> model, out string mensagem);
    }
}
