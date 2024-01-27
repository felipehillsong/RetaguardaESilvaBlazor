using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;
using RetaguardaESilva.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetaguardaESilva.Domain.ViewModels;
using RetaguardaESilva.Persistence.Migrations;
using System.Runtime.ConstrainedExecution;
using RetaguardaESilva.Domain.Mensagem;

namespace RetaguardaESilva.Persistence.Persistencias
{
    public class RelatorioPersist : IRelatorioPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public RelatorioPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cliente>> GetAllClientesAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId && c.DataCadastroCliente.Value.Date >= dataInicio && c.DataCadastroCliente.Value.Date <= dataFinal).OrderBy(c => c.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Cliente>> GetAllClientesAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId && c.DataCadastroCliente.Value.Date >= dataInicio && c.DataCadastroCliente.Value.Date <= dataFinal && c.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(c => c.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Cliente>> GetAllClientesInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId && c.DataCadastroCliente.Value.Date >= dataInicio && c.DataCadastroCliente.Value.Date <= dataFinal && c.Ativo == Convert.ToBoolean(Situacao.Inativo) && c.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(c => c.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Cliente>> GetAllClientesExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId && c.DataCadastroCliente.Value.Date >= dataInicio && c.DataCadastroCliente.Value.Date <= dataFinal && c.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(c => c.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Fornecedor>> GetAllFornecedoresAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFornecedor.Value.Date >= dataInicio && f.DataCadastroFornecedor.Value.Date <= dataFinal).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Fornecedor>> GetAllFornecedoresAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFornecedor.Value.Date >= dataInicio && f.DataCadastroFornecedor.Value.Date <= dataFinal && f.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Fornecedor>> GetAllFornecedoresInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFornecedor.Value.Date >= dataInicio && f.DataCadastroFornecedor.Value.Date <= dataFinal && f.Ativo == Convert.ToBoolean(Situacao.Inativo) && f.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Fornecedor>> GetAllFornecedoresExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFornecedor.Value.Date >= dataInicio && f.DataCadastroFornecedor.Value.Date <= dataFinal && f.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosAllAsync(int empresaId)
        {   
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosAtivoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosInativoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresProdutosExcluidoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Excluido)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosAllAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosAtivoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosInativoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresInativosProdutosExcluidoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Excluido)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosAllAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosAtivoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosInativoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Inativo)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<FornecedorProdutoViewModel>> GetFornecedoresExcluidosProdutosExcluidoAsync(int empresaId)
        {
            var fornecedorProdutoRetorno = new List<FornecedorProdutoViewModel>();
            var produtosFornecedor = new List<ProdutoViewModel>();
            var fornecedores = await _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
            foreach (var item in fornecedores)
            {
                var fornecedorProduto = new FornecedorProdutoViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Numero = item.Numero,
                    Municipio = item.Municipio,
                    UF = item.UF,
                    Pais = item.Pais,
                    CEP = item.CEP,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    CNPJ = item.CNPJ,
                    InscricaoMunicipal = item.InscricaoMunicipal,
                    InscricaoEstadual = item.InscricaoEstadual,
                    DataCadastroFornecedor = item.DataCadastroFornecedor,
                    Ativo = item.Ativo,
                    StatusExclusao = item.StatusExclusao,
                    EmpresaId = item.EmpresaId
                };

                fornecedorProdutoRetorno.Add(fornecedorProduto);
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.FornecedorId == item.Id && p.Ativo == Convert.ToBoolean(Situacao.Excluido)).OrderBy(p => p.Nome).ToListAsync();
                foreach (var item1 in produtos)
                {
                    var produto = new ProdutoViewModel
                    {
                        Id = item1.Id,
                        Nome = item1.Nome,
                        Quantidade = item1.Quantidade,
                        Ativo = item1.Ativo,
                        StatusExclusao = item1.StatusExclusao,
                        PrecoCompra = item1.PrecoCompra,
                        PrecoVenda = item1.PrecoVenda,
                        Codigo = item1.Codigo,
                        DataCadastroProduto = item1.DataCadastroProduto,
                        EmpresaId = item1.EmpresaId,
                        FornecedorId = item1.FornecedorId
                    };
                    produtosFornecedor.Add(produto);
                }
            }

            foreach (var item in fornecedorProdutoRetorno)
            {
                var produtos = produtosFornecedor.Where(p => p.FornecedorId == item.Id).ToList();
                item.Produtos = produtos;
            }

            foreach (var item in fornecedorProdutoRetorno.ToList())
            {
                if (item.Produtos.Count() == (int)ZerarIdFornecedor.FornecedorSemProduto)
                {
                    fornecedorProdutoRetorno.Remove(item);
                }
            }

            return fornecedorProdutoRetorno;
        }
        public async Task<IEnumerable<Funcionario>> GetAllFuncionariosAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFuncionario.Value.Date >= dataInicio && f.DataCadastroFuncionario.Value.Date <= dataFinal).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Funcionario>> GetAllFuncionariosAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFuncionario.Value.Date >= dataInicio && f.DataCadastroFuncionario.Value.Date <= dataFinal && f.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Funcionario>> GetAllFuncionariosInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFuncionario.Value.Date >= dataInicio && f.DataCadastroFuncionario.Value.Date <= dataFinal && f.Ativo == Convert.ToBoolean(Situacao.Inativo) && f.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Funcionario>> GetAllFuncionariosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId && f.DataCadastroFuncionario.Value.Date >= dataInicio && f.DataCadastroFuncionario.Value.Date <= dataFinal && f.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(f => f.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Transportador>> GetAllTransportadoresAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Transportador.AsNoTracking().Where(t => t.EmpresaId == empresaId && t.DataCadastroTransportador.Value.Date >= dataInicio && t.DataCadastroTransportador.Value.Date <= dataFinal).OrderBy(t => t.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Transportador>> GetAllTransportadoresAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Transportador.AsNoTracking().Where(t => t.EmpresaId == empresaId && t.DataCadastroTransportador.Value.Date >= dataInicio && t.DataCadastroTransportador.Value.Date <= dataFinal && t.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(t => t.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Transportador>> GetAllTransportadoresInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Transportador.AsNoTracking().Where(t => t.EmpresaId == empresaId && t.DataCadastroTransportador.Value.Date >= dataInicio && t.DataCadastroTransportador.Value.Date <= dataFinal && t.Ativo == Convert.ToBoolean(Situacao.Inativo) && t.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(t => t.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Transportador>> GetAllTransportadoresExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Transportador.AsNoTracking().Where(t => t.EmpresaId == empresaId && t.DataCadastroTransportador.Value.Date >= dataInicio && t.DataCadastroTransportador.Value.Date <= dataFinal && t.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(t => t.Nome).ToListAsync();
        }
        public async Task<IEnumerable<UsuarioViewModel>> GetAllUsuariosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            List<UsuarioViewModel> usuariosRetorno = new List<UsuarioViewModel>();
            var usuarios = (from users in _context.Usuario
                            join func in _context.Funcionario on users.FuncionarioId equals func.Id
                            join empre in _context.Empresa on users.EmpresaId equals empre.Id
                            select new
                            {
                                Id = users.Id,
                                NomeEmpresa = empre.Nome,
                                Nome = func.Nome,
                                Email = users.Email,
                                Senha = users.Senha,
                                DataCadastroUsuario = users.DataCadastroUsuario,
                                Ativo = users.Ativo,
                                EmpresaId = users.EmpresaId,
                                FuncionarioId = users.FuncionarioId
                            }).Where(u => u.EmpresaId == empresaId && u.DataCadastroUsuario.Value.Date >= dataInicio && u.DataCadastroUsuario.Value.Date <= dataFinal).ToList();

            foreach (var item in usuarios)
            {
                usuariosRetorno.Add(new UsuarioViewModel(item.Id, item.NomeEmpresa, item.Nome, item.Email, item.Senha, item.DataCadastroUsuario, item.Ativo, item.FuncionarioId, item.EmpresaId));
            }

            return usuariosRetorno;
        }
        public async Task<IEnumerable<Empresa>> GetAllEmpresasAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Empresa.AsNoTracking().Where(e => e.Id == empresaId && e.DataCadastroEmpresa.Value.Date >= dataInicio && e.DataCadastroEmpresa.Value.Date <= dataFinal).OrderBy(e => e.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Empresa>> GetAllEmpresasAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Empresa.AsNoTracking().Where(e => e.Id == empresaId && e.DataCadastroEmpresa.Value.Date >= dataInicio && e.DataCadastroEmpresa.Value.Date <= dataFinal && e.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(e => e.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Empresa>> GetAllEmpresasInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Empresa.AsNoTracking().Where(e => e.Id == empresaId && e.DataCadastroEmpresa.Value.Date >= dataInicio && e.DataCadastroEmpresa.Value.Date <= dataFinal && e.Ativo == Convert.ToBoolean(Situacao.Inativo) && e.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(e => e.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Empresa>> GetAllEmpresasExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Empresa.AsNoTracking().Where(e => e.Id == empresaId && e.DataCadastroEmpresa.Value.Date >= dataInicio && e.DataCadastroEmpresa.Value.Date <= dataFinal && e.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(e => e.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> GetAllProdutosAtivosInativosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroProduto.Value.Date >= dataInicio && p.DataCadastroProduto.Value.Date <= dataFinal).OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> GetAllProdutosAtivosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroProduto.Value.Date >= dataInicio && p.DataCadastroProduto.Value.Date <= dataFinal && p.Ativo == Convert.ToBoolean(Situacao.Ativo)).OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> GetAllProdutosInativosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroProduto.Value.Date >= dataInicio && p.DataCadastroProduto.Value.Date <= dataFinal && p.Ativo == Convert.ToBoolean(Situacao.Inativo) && p.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> GetAllProdutosExcluidosAsync(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            return await _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroProduto.Value.Date >= dataInicio && p.DataCadastroProduto.Value.Date <= dataFinal && p.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).OrderBy(p => p.Nome).ToListAsync();
        }

        public IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            List<Produto> produtos = new List<Produto>();
            List<EnderecoProduto> enderecoProduto = new List<EnderecoProduto>();
            List<EstoqueViewModelRelatorio> EstoqueProdutoRetorno = new List<EstoqueViewModelRelatorio>();
            var estoques = _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId && e.DataCadastroEstoque.Value.Date >= dataInicio && e.DataCadastroEstoque.Value.Date <= dataFinal).ToList();

            foreach (var item in estoques)
            {
                var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == item.ProdutoId);
                if (produto != null)
                {
                    produtos.Add(produto);
                }
            }
            
            if (estoques.Any())
            {
                var estoquesProdutos = from prod in produtos
                                                    from esto in estoques
                                                    where esto.ProdutoId == prod.Id && esto.EmpresaId == empresaId
                                                    select new
                                                    {
                                                        Id = esto.Id,
                                                        ProdutoId = prod.Id,
                                                        ProdutoNome = prod.Nome,
                                                        Quantidade = esto.Quantidade,
                                                        StatusExclusao = esto.StatusExclusao,
                                                        DataCadastroEstoque = esto.DataCadastroEstoque
                                                    };
                foreach (var produtosRetorno in estoquesProdutos)
                {
                    EstoqueProdutoRetorno.Add(new EstoqueViewModelRelatorio(produtosRetorno.Id, produtosRetorno.ProdutoId, produtosRetorno.ProdutoNome, produtosRetorno.Quantidade, produtosRetorno.StatusExclusao, produtosRetorno.DataCadastroEstoque));
                }
            }
            else
            {
                return EstoqueProdutoRetorno;
            }
            return EstoqueProdutoRetorno.OrderBy(p => p.ProdutoNome);
        }

        public IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueAtivoRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            List<Produto> produtos = new List<Produto>();
            List<EnderecoProduto> enderecoProduto = new List<EnderecoProduto>();
            List<EstoqueViewModelRelatorio> EstoqueProdutoRetorno = new List<EstoqueViewModelRelatorio>();
            var estoques = _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId && e.DataCadastroEstoque.Value.Date >= dataInicio && e.DataCadastroEstoque.Value.Date <= dataFinal && e.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).ToList();

            foreach (var item in estoques)
            {
                var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == item.ProdutoId && p.Ativo == Convert.ToBoolean(Situacao.Ativo));
                if (produto != null)
                {
                    produtos.Add(produto);
                }
            }

            if (estoques.Any())
            {
                var estoquesProdutos = from prod in produtos
                                       from esto in estoques
                                       where esto.ProdutoId == prod.Id && esto.EmpresaId == empresaId
                                       select new
                                       {
                                           Id = esto.Id,
                                           ProdutoId = prod.Id,
                                           ProdutoNome = prod.Nome,
                                           Quantidade = esto.Quantidade,
                                           StatusExclusao = esto.StatusExclusao,
                                           DataCadastroEstoque = esto.DataCadastroEstoque
                                       };
                foreach (var produtosRetorno in estoquesProdutos)
                {
                    EstoqueProdutoRetorno.Add(new EstoqueViewModelRelatorio(produtosRetorno.Id, produtosRetorno.ProdutoId, produtosRetorno.ProdutoNome, produtosRetorno.Quantidade, produtosRetorno.StatusExclusao, produtosRetorno.DataCadastroEstoque));
                }
            }
            else
            {
                return EstoqueProdutoRetorno;
            }
            return EstoqueProdutoRetorno.OrderBy(p => p.ProdutoNome);
        }

        public IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueInativoRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            List<Produto> produtos = new List<Produto>();
            List<EnderecoProduto> enderecoProduto = new List<EnderecoProduto>();
            List<EstoqueViewModelRelatorio> EstoqueProdutoRetorno = new List<EstoqueViewModelRelatorio>();
            var estoques = _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId && e.DataCadastroEstoque.Value.Date >= dataInicio && e.DataCadastroEstoque.Value.Date <= dataFinal && e.StatusExclusao != Convert.ToBoolean(Situacao.Excluido)).ToList();

            foreach (var item in estoques)
            {
                var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == item.ProdutoId && p.Ativo == Convert.ToBoolean(Situacao.Inativo) && p.StatusExclusao != Convert.ToBoolean(Situacao.Excluido));                
                if (produto != null)
                {
                    produtos.Add(produto);
                }
            }
            if (estoques.Any())
            {
                var estoquesProdutos = from prod in produtos
                                       from esto in estoques
                                       where esto.ProdutoId == prod.Id && esto.EmpresaId == empresaId
                                       select new
                                       {
                                           Id = esto.Id,
                                           ProdutoId = prod.Id,
                                           ProdutoNome = prod.Nome,
                                           Quantidade = esto.Quantidade,
                                           StatusExclusao = esto.StatusExclusao,
                                           DataCadastroEstoque = esto.DataCadastroEstoque
                                       };
                foreach (var produtosRetorno in estoquesProdutos)
                {
                    EstoqueProdutoRetorno.Add(new EstoqueViewModelRelatorio(produtosRetorno.Id, produtosRetorno.ProdutoId, produtosRetorno.ProdutoNome, produtosRetorno.Quantidade, produtosRetorno.StatusExclusao, produtosRetorno.DataCadastroEstoque));
                }
            }
            else
            {
                return EstoqueProdutoRetorno;
            }
            return EstoqueProdutoRetorno.OrderBy(p => p.ProdutoNome);
        }

        public IEnumerable<EstoqueViewModelRelatorio> GetAllEstoqueExcluidoRelatorio(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            List<Produto> produtos = new List<Produto>();
            List<EnderecoProduto> enderecoProduto = new List<EnderecoProduto>();
            List<EstoqueViewModelRelatorio> EstoqueProdutoRetorno = new List<EstoqueViewModelRelatorio>();
            var estoques = _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId && e.DataCadastroEstoque.Value.Date >= dataInicio && e.DataCadastroEstoque.Value.Date <= dataFinal && e.StatusExclusao == Convert.ToBoolean(Situacao.Excluido)).ToList();

            foreach (var item in estoques)
            {
                var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(Situacao.Excluido));
                if (produto != null)
                {
                    produtos.Add(produto);
                }
            }

            if (estoques.Any())
            {
                var estoquesProdutos = from prod in produtos
                                       from esto in estoques
                                       where esto.ProdutoId == prod.Id && esto.EmpresaId == empresaId
                                       select new
                                       {
                                           Id = esto.Id,
                                           ProdutoId = prod.Id,
                                           ProdutoNome = prod.Nome,
                                           Quantidade = esto.Quantidade,
                                           StatusExclusao = esto.StatusExclusao,
                                           DataCadastroEstoque = esto.DataCadastroEstoque
                                       };
                foreach (var produtosRetorno in estoquesProdutos)
                {
                    EstoqueProdutoRetorno.Add(new EstoqueViewModelRelatorio(produtosRetorno.Id, produtosRetorno.ProdutoId, produtosRetorno.ProdutoNome, produtosRetorno.Quantidade, produtosRetorno.StatusExclusao, produtosRetorno.DataCadastroEstoque));
                }
            }
            else
            {
                return EstoqueProdutoRetorno;
            }
            return EstoqueProdutoRetorno.OrderBy(p => p.ProdutoNome);
        }

        public IEnumerable<PedidoViewModel> GetAllPedidos(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {   
            var pedidosRetorno = new List<PedidoViewModel>();
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            var pedidos = _context.Pedido.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroPedido.Value.Date >= dataInicio && p.DataCadastroPedido.Value.Date <= dataFinal).ToListAsync();
            foreach (var pedido in pedidos.Result)
            {
                var pedidoNota = _context.PedidoNota.Where(pn => pn.PedidoId == pedido.Id).ToListAsync();
                foreach (var item in pedidoNota.Result)
                {
                    var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.ProdutoId);
                    if (produto != null)
                    {
                        var produtoView = new ProdutoViewModel()
                        {
                            Id = item.ProdutoId,
                            Nome = produto.Nome,
                            Quantidade = produto.Quantidade,
                            QuantidadeVenda = item.Quantidade,
                            Ativo = produto.Ativo,
                            PrecoCompra = produto.PrecoCompra,
                            PrecoVenda = item.PrecoVenda,
                            PrecoVendaTotal = item.PrecoTotal,
                            Codigo = item.CodigoProduto,
                            DataCadastroProduto = produto.DataCadastroProduto,
                            EmpresaId = produto.EmpresaId,
                            FornecedorId = produto.FornecedorId,
                            StatusExclusao = produto.StatusExclusao
                        };

                        if (produtoView != null)
                        {
                            produtos.Add(produtoView);
                        }
                        else
                        {
                            produtos.Add(null);
                        }
                    }
                }

                var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.Id == pedido.ClienteId);
                var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.Id == pedido.TransportadorId);
                var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Id == pedido.UsuarioId);
                if (cliente != null && transportador != null && usuario != null && produtos != null)
                {
                    var statusPedido = string.Empty;
                    switch (pedido.Status)
                    {
                        case 1:
                            statusPedido = MensagemDeAlerta.PedidoEmAnalise;
                            pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, new List<ProdutoViewModel>(produtos)));
                            break;
                        case 2:
                            statusPedido = MensagemDeAlerta.PedidoConfirmado;
                            pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, new List<ProdutoViewModel>(produtos)));
                            break;
                        case 3:
                            statusPedido = MensagemDeAlerta.PedidoCancelado;
                            pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, new List<ProdutoViewModel>(produtos)));
                            break;
                    }
                }
                produtos.Clear();
            }
            return pedidosRetorno;
        }

        public IEnumerable<PedidoViewModel> GetAllPedidosEmAnalise(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var pedidosRetorno = new List<PedidoViewModel>();
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            var pedidos = _context.Pedido.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroPedido.Value.Date >= dataInicio && p.DataCadastroPedido.Value.Date <= dataFinal && p.Status == (int)StatusPedido.PedidoEmAnalise).ToListAsync();
            foreach (var pedido in pedidos.Result)
            {
                var pedidoNota = _context.PedidoNota.Where(pn => pn.PedidoId == pedido.Id && pn.Status == (int)StatusPedido.PedidoEmAnalise).ToListAsync();
                foreach (var item in pedidoNota.Result)
                {
                    var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.ProdutoId);
                    if (produto != null)
                    {
                        var produtoView = new ProdutoViewModel()
                        {
                            Id = item.ProdutoId,
                            Nome = produto.Nome,
                            Quantidade = produto.Quantidade,
                            QuantidadeVenda = item.Quantidade,
                            Ativo = produto.Ativo,
                            PrecoCompra = produto.PrecoCompra,
                            PrecoVenda = item.PrecoVenda,
                            PrecoVendaTotal = item.PrecoTotal,
                            Codigo = item.CodigoProduto,
                            DataCadastroProduto = produto.DataCadastroProduto,
                            EmpresaId = produto.EmpresaId,
                            FornecedorId = produto.FornecedorId,
                            StatusExclusao = produto.StatusExclusao
                        };

                        if (produtoView != null)
                        {
                            produtos.Add(produtoView);
                        }
                        else
                        {
                            produtos.Add(null);
                        }
                    }
                }

                var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.Id == pedido.ClienteId);
                var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.Id == pedido.TransportadorId);
                var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Id == pedido.UsuarioId);
                if (cliente != null && transportador != null && usuario != null && produtos != null)
                {
                    var statusPedido = string.Empty;
                    statusPedido = MensagemDeAlerta.PedidoEmAnalise;
                    pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, new List<ProdutoViewModel>(produtos)));
                }
                produtos.Clear();
            }
            return pedidosRetorno;
        }

        public IEnumerable<PedidoViewModel> GetAllPedidosConfirmados(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var pedidosRetorno = new List<PedidoViewModel>();
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            var pedidos = _context.Pedido.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroPedido.Value.Date >= dataInicio && p.DataCadastroPedido.Value.Date <= dataFinal && p.Status == (int)StatusPedido.PedidoConfirmado).ToListAsync();
            foreach (var pedido in pedidos.Result)
            {
                var pedidoNota = _context.PedidoNota.Where(pn => pn.PedidoId == pedido.Id && pn.Status == (int)StatusPedido.PedidoConfirmado).ToListAsync();
                foreach (var item in pedidoNota.Result)
                {
                    var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.ProdutoId);
                    if (produto != null)
                    {
                        var produtoView = new ProdutoViewModel()
                        {
                            Id = item.ProdutoId,
                            Nome = produto.Nome,
                            Quantidade = produto.Quantidade,
                            QuantidadeVenda = item.Quantidade,
                            Ativo = produto.Ativo,
                            PrecoCompra = produto.PrecoCompra,
                            PrecoVenda = item.PrecoVenda,
                            PrecoVendaTotal = item.PrecoTotal,
                            Codigo = item.CodigoProduto,
                            DataCadastroProduto = produto.DataCadastroProduto,
                            EmpresaId = produto.EmpresaId,
                            FornecedorId = produto.FornecedorId,
                            StatusExclusao = produto.StatusExclusao
                        };

                        if (produtoView != null)
                        {
                            produtos.Add(produtoView);
                        }
                        else
                        {
                            produtos.Add(null);
                        }
                    }
                }

                var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.Id == pedido.ClienteId);
                var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.Id == pedido.TransportadorId);
                var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Id == pedido.UsuarioId);
                if (cliente != null && transportador != null && usuario != null && produtos != null)
                {
                    var statusPedido = string.Empty;
                    statusPedido = MensagemDeAlerta.PedidoConfirmado;
                    pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, new List<ProdutoViewModel>(produtos)));
                }
                produtos.Clear();
            }
            return pedidosRetorno;
        }

        public IEnumerable<PedidoViewModel> GetAllPedidosCancelados(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var pedidosRetorno = new List<PedidoViewModel>();
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            var pedidos = _context.Pedido.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.DataCadastroPedido.Value.Date >= dataInicio && p.DataCadastroPedido.Value.Date <= dataFinal && p.Status == (int)StatusPedido.PedidoCancelado).ToListAsync();
            foreach (var pedido in pedidos.Result)
            {
                var pedidoNota = _context.PedidoNota.Where(pn => pn.PedidoId == pedido.Id && pn.Status == (int)StatusPedido.PedidoCancelado).ToListAsync();
                foreach (var item in pedidoNota.Result)
                {
                    var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.ProdutoId);
                    if (produto != null)
                    {
                        var produtoView = new ProdutoViewModel()
                        {
                            Id = item.ProdutoId,
                            Nome = produto.Nome,
                            Quantidade = produto.Quantidade,
                            QuantidadeVenda = item.Quantidade,
                            Ativo = produto.Ativo,
                            PrecoCompra = produto.PrecoCompra,
                            PrecoVenda = item.PrecoVenda,
                            PrecoVendaTotal = item.PrecoTotal,
                            Codigo = item.CodigoProduto,
                            DataCadastroProduto = produto.DataCadastroProduto,
                            EmpresaId = produto.EmpresaId,
                            FornecedorId = produto.FornecedorId,
                            StatusExclusao = produto.StatusExclusao
                        };

                        if (produtoView != null)
                        {
                            produtos.Add(produtoView);
                        }
                        else
                        {
                            produtos.Add(null);
                        }
                    }
                }

                var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.Id == pedido.ClienteId);
                var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.Id == pedido.TransportadorId);
                var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Id == pedido.UsuarioId);
                if (cliente != null && transportador != null && usuario != null && produtos != null)
                {
                    var statusPedido = string.Empty;
                    statusPedido = MensagemDeAlerta.PedidoCancelado;
                    pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, new List<ProdutoViewModel>(produtos)));
                }
                produtos.Clear();
            }
            return pedidosRetorno;
        }

        public IEnumerable<NotaFiscalViewModel> GetAllNotasFiscais(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var notasFiscais =  _context.NotaFiscal.AsNoTracking().Where(nf => nf.EmpresaId == empresaId && nf.DataCadastroNotaFiscal.Value.Date >= dataInicio && nf.DataCadastroNotaFiscal.Value.Date <= dataFinal).OrderBy(nf => nf.EmpresaId).ToList();
            List<NotaFiscalViewModel> notaFiscalRetorno = new List<NotaFiscalViewModel>();
            if (notasFiscais != null)
            {
                foreach (var item in notasFiscais)
                {
                    var pedido = _context.Pedido.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.PedidoId);
                    var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == item.EmpresaId && c.Id == item.ClienteId);
                    if (pedido != null && cliente != null)
                    {
                        if (item.Status == (int)StatusNotaFiscal.NotaFiscalAprovada)
                        {
                            var status = MensagemDeAlerta.NotaFiscalAprovada;
                            notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, cliente.Nome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                        }
                        else if (item.Status == (int)StatusNotaFiscal.NotaFiscalCancelada)
                        {
                            var status = MensagemDeAlerta.NotaFiscalCancelada;
                            notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, cliente.Nome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                        }
                    }
                    else
                    {
                        var clienteSemNome = MensagemDeAlerta.ClienteExcluido;
                        if (item.Status == (int)StatusNotaFiscal.NotaFiscalAprovada)
                        {
                            var status = MensagemDeAlerta.NotaFiscalAprovada;
                            notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, clienteSemNome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                        }
                        else if (item.Status == (int)StatusNotaFiscal.NotaFiscalCancelada)
                        {
                            var status = MensagemDeAlerta.NotaFiscalCancelada;
                            notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, clienteSemNome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                        }
                    }
                }
            }
            return notaFiscalRetorno;
        }

        public IEnumerable<NotaFiscalViewModel> GetAllNotasFiscaisAprovadas(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var notasFiscais = _context.NotaFiscal.AsNoTracking().Where(nf => nf.EmpresaId == empresaId && nf.DataCadastroNotaFiscal.Value.Date >= dataInicio && nf.DataCadastroNotaFiscal.Value.Date <= dataFinal && nf.Status == (int)StatusNotaFiscal.NotaFiscalAprovada).OrderBy(nf => nf.EmpresaId).ToList();
            List<NotaFiscalViewModel> notaFiscalRetorno = new List<NotaFiscalViewModel>();
            if (notasFiscais != null)
            {
                foreach (var item in notasFiscais)
                {
                    var pedido = _context.Pedido.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.PedidoId);
                    var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == item.EmpresaId && c.Id == item.ClienteId);
                    if (pedido != null && cliente != null)
                    {
                        var status = MensagemDeAlerta.NotaFiscalAprovada;
                        notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, cliente.Nome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                    }
                    else
                    {
                        var clienteSemNome = MensagemDeAlerta.ClienteExcluido;
                        var status = MensagemDeAlerta.NotaFiscalAprovada;
                        notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, clienteSemNome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                    }
                }
            }
            return notaFiscalRetorno;
        }

        public IEnumerable<NotaFiscalViewModel> GetAllNotasFiscaisCanceladas(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var notasFiscais = _context.NotaFiscal.AsNoTracking().Where(nf => nf.EmpresaId == empresaId && nf.DataCadastroNotaFiscal.Value.Date >= dataInicio && nf.DataCadastroNotaFiscal.Value.Date <= dataFinal && nf.Status == (int)StatusNotaFiscal.NotaFiscalCancelada).OrderBy(nf => nf.EmpresaId).ToList();
            List<NotaFiscalViewModel> notaFiscalRetorno = new List<NotaFiscalViewModel>();
            if (notasFiscais != null)
            {
                foreach (var item in notasFiscais)
                {
                    var pedido = _context.Pedido.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.PedidoId);
                    var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == item.EmpresaId && c.Id == item.ClienteId);
                    if (pedido != null && cliente != null)
                    {
                        var status = MensagemDeAlerta.NotaFiscalCancelada;
                        notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, cliente.Nome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                    }
                    else
                    {
                        var clienteSemNome = MensagemDeAlerta.ClienteExcluido;
                        var status = MensagemDeAlerta.NotaFiscalCancelada;
                        notaFiscalRetorno.Add(new NotaFiscalViewModel(item.Id, item.PedidoId, clienteSemNome, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, status, item.Status));
                    }
                }
            }
            return notaFiscalRetorno;
        }
    }
}
