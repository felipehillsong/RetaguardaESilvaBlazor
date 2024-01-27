using System;
using System.Collections.Generic;
using System.Linq;
using RetaguardaESilva.Domain.Models;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Contratos
{
    public interface IFuncionarioPersist
    {
        Task<IEnumerable<Funcionario>> GetAllFuncionariosAsync(int empresaId);
        Task<Funcionario> GetFuncionarioByIdAsync(int empresaId, int funcionarioId);
        Task<Funcionario> GetFuncionarioLoginByIdAsync(int funcionarioId);
    }
}