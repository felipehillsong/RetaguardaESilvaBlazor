using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface ILoginService
    {        
        Task<UsuarioLoginDTO> Login(string email, string senha);
        Task<UsuarioLoginDTO> AlterarLogin(string email, string senha, string nomeUsuario, FuncionarioDTO funcionario, UsuarioDTO usuario);
    }
}
