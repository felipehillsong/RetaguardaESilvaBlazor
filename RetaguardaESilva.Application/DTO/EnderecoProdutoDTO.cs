using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.DTO
{
    public class EnderecoProdutoDTO
    {
        public int Id { get; set; }
        public string NomeEndereco { get; set; }
        public int ProdutoId { get; set; }
        public int EstoqueId { get; set; }
        public int EmpresaId { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastroEnderecoProduto { get; set; }
    }

    public class EnderecoProdutoCreateDTO : EnderecoProdutoDTO
    {

    }

    public class EnderecoProdutoUpdateDTO : EnderecoProdutoDTO
    {

    }

    public class EnderecoProdutoViewModelDTO
    {
        public string ProdutoNome { get; set; }
        public string NomeEndereco { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastroEnderecoProduto { get; set; }

        public EnderecoProdutoViewModelDTO(string produtoNome, string nomeEndereco, bool ativo, DateTime? dataCadastroEnderecoProduto)
        {
            this.ProdutoNome = produtoNome;
            this.NomeEndereco = nomeEndereco;
            this.Ativo = ativo;
            this.DataCadastroEnderecoProduto = dataCadastroEnderecoProduto;
        }
    }
}