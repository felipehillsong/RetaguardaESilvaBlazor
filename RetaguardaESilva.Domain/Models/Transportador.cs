using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Domain.Models
{
    public class Transportador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
        public string? Complemento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public DateTime? DataCadastroTransportador { get; set; }
        public bool Ativo { get; set; }
        public bool StatusExclusao { get; set; }
        public int EmpresaId { get; set; }
    }
}
