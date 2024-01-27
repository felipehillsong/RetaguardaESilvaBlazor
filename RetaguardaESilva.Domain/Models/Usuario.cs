using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime? DataCadastroUsuario { get; set; }
        public bool Ativo { get; set; }
        public int FuncionarioId { get; set; }
        public int EmpresaId { get; set; }
    }
}
