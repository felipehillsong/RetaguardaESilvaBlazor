using AutoMapper;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Domain.ViewModels;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEstoquePersist _estoquePersist;
        private readonly IProdutoPersist _produtoPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IMapper _mapper;

        public EstoqueService(IGeralPersist geralPersist, IEstoquePersist estoquePersist, IProdutoPersist produtoPersist, IValidacoesPersist validacoesPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _estoquePersist = estoquePersist;
            _produtoPersist = produtoPersist;
            _validacoesPersist = validacoesPersist;
            _mapper = mapper;
        }

        public async Task<EstoqueViewModelUpdateDTO> UpdateEstoque(int empresaId, int estoqueId, int quantidade)
        {
            try
            {
                var estoque = await _estoquePersist.GetEstoqueByIdAsync(empresaId, estoqueId);
                var produto = await _produtoPersist.GetProdutoByIdAsync(empresaId, estoque.ProdutoId);
                if (estoque == null || produto == null)
                {
                    return null;
                }
                else
                {
                    estoque.Quantidade = quantidade;
                    _geralPersist.Update(estoque);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        produto.Quantidade = quantidade;
                        _geralPersist.Update(produto);
                        await _geralPersist.SaveChangesAsync();
                        var estoqueProduto = _validacoesPersist.RetornarProdutosEstoqueId(empresaId, estoqueId);
                        var estoqueProdutoRetorno = new EstoqueViewModelUpdateDTO(estoqueProduto.Id, estoqueProduto.EmpresaId, estoqueProduto.EmpresaNome, estoqueProduto.FornecedorId, estoqueProduto.FornecedorNome, estoqueProduto.ProdutoId, estoqueProduto.ProdutoNome, estoqueProduto.Quantidade, (int)Ids.IdCreate, (DateTime)estoque.DataCadastroEstoque);
                        return estoqueProdutoRetorno;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEstoque(int empresaId, int estoqueId)
        {
            try
            {
                var estoque = await _estoquePersist.GetEstoqueByIdAsync(empresaId, estoqueId);
                var enderecoProduto = await _estoquePersist.GetEnderecoProdutoDeleteByIdAsync(empresaId, estoqueId);
                var produto = await _produtoPersist.GetProdutoByIdAsync(empresaId, estoque.ProdutoId);
                if (estoque == null || produto == null)
                {
                    throw new Exception(MensagemDeErro.EstoqueNaoEncontradoDelete);
                }
                else
                {
                    var produtos = new Produto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Quantidade = (int)StatusProduto.ZerarQuantidade,
                        Ativo = Convert.ToBoolean(Situacao.Inativo),
                        StatusExclusao = Convert.ToBoolean(StatusProduto.ProdutoExcluido),
                        PrecoCompra = (int)StatusProduto.ZerarQuantidade,
                        PrecoVenda = (int)StatusProduto.ZerarQuantidade,
                        Codigo = (int)StatusProduto.ZerarQuantidade,
                        DataCadastroProduto = produto.DataCadastroProduto,
                        EmpresaId = produto.EmpresaId,
                        FornecedorId = produto.FornecedorId
                    };

                    _geralPersist.Update<Produto>(produtos);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        var estoques = new Estoque()
                        {
                            Id = estoque.Id,
                            EmpresaId = estoque.EmpresaId,
                            FornecedorId = estoque.FornecedorId,
                            ProdutoId = estoque.ProdutoId,
                            Quantidade = (int)StatusProduto.ZerarQuantidade,
                            StatusExclusao = Convert.ToBoolean(StatusProduto.ProdutoExcluido)
                        };
                        _geralPersist.Update<Estoque>(estoques);
                        await _geralPersist.SaveChangesAsync();
                        if (enderecoProduto != null)
                        {
                            return await DeleteEnderecoProduto(empresaId, enderecoProduto.Id);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EstoqueViewModelDTO>> GetAllEstoquesAsync(int empresaId)
        {
            try
            {
                var estoques = await _estoquePersist.GetAllEstoqueClienteAsync(empresaId);
                if (estoques == null)
                {
                    return null;
                }
                else
                {
                    List<EstoqueViewModelDTO> EstoqueProdutoRetorno = new List<EstoqueViewModelDTO>();
                    var estoqueProduto = _validacoesPersist.RetornarProdutosEstoque(empresaId);
                    foreach (var produtoEstoque in estoqueProduto)
                    {
                        EstoqueProdutoRetorno.Add(new EstoqueViewModelDTO(produtoEstoque.Id, produtoEstoque.EmpresaId, produtoEstoque.EmpresaNome, produtoEstoque.FornecedorId, produtoEstoque.FornecedorNome, produtoEstoque.ProdutoId, produtoEstoque.ProdutoNome, produtoEstoque.Quantidade, produtoEstoque.EnderecoProdutoId, produtoEstoque.DataCadastroEstoque));
                    }
                    return EstoqueProdutoRetorno;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        

        public async Task<EstoqueViewModelUpdateDTO> GetEstoqueByIdAsync(int empresaId, int estoqueId)
        {
            try
            {
                var estoque = await _estoquePersist.GetEstoqueByIdAsync(empresaId, estoqueId);
                if (estoque == null)
                {
                    return null;
                }
                else
                { 
                    var estoqueProduto = _validacoesPersist.RetornarProdutosEstoqueId(empresaId, estoqueId);
                    var estoqueProdutoRetorno = new EstoqueViewModelUpdateDTO(estoqueProduto.Id, estoqueProduto.EmpresaId, estoqueProduto.EmpresaNome, estoqueProduto.FornecedorId, estoqueProduto.FornecedorNome, estoqueProduto.ProdutoId, estoqueProduto.ProdutoNome, estoqueProduto.Quantidade, (int)Ids.IdCreate, (DateTime)estoque.DataCadastroEstoque);
                    return estoqueProdutoRetorno;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EnderecoProdutoCreateDTO> AddEnderecoProduto(EnderecoProdutoCreateDTO model)
        {
            try
            {    
                if (_validacoesPersist.ExisteEnderecoProduto(model.EmpresaId, model.Id, model.NomeEndereco, false, out string mensagem))
                {
                    throw new Exception(mensagem);
                }
                else
                {
                    model.Ativo = Convert.ToBoolean(Situacao.Ativo);
                    var enderecoProdutoCreateDTO = _mapper.Map<EnderecoProduto>(model);
                    enderecoProdutoCreateDTO.NomeEndereco = _validacoesPersist.AcertarNome(enderecoProdutoCreateDTO.NomeEndereco);
                    var enderecoProdutoDTO = _mapper.Map<EnderecoProduto>(model);
                    _geralPersist.Add<EnderecoProduto>(enderecoProdutoDTO);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        var retornoEnderecoProduto = await _estoquePersist.GetEnderecoProdutoByIdAsync(enderecoProdutoDTO.EmpresaId, enderecoProdutoDTO.Id);
                        return _mapper.Map<EnderecoProdutoCreateDTO>(retornoEnderecoProduto);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EnderecoProdutoUpdateDTO> UpdateEnderecoProduto(EnderecoProdutoUpdateDTO model)
        {
            try
            {
                var enderecoProduto = await _estoquePersist.GetEnderecoProdutoByIdAsync(model.EmpresaId, model.Id);
                if (enderecoProduto == null)
                {
                    return null;
                }
                else
                {
                    if (_validacoesPersist.ExisteEnderecoProduto(model.EmpresaId, model.Id, model.NomeEndereco, true, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    else
                    {
                        var enderecoProdutoUpdateDTO = _mapper.Map<EnderecoProduto>(model);
                        enderecoProdutoUpdateDTO.NomeEndereco = _validacoesPersist.AcertarNome(enderecoProdutoUpdateDTO.NomeEndereco);
                        _geralPersist.Update(enderecoProdutoUpdateDTO);
                        if (await _geralPersist.SaveChangesAsync())
                        {
                            var retornoEnderecoProduto = await _estoquePersist.GetEnderecoProdutoByIdAsync(model.EmpresaId, model.Id);
                            return _mapper.Map<EnderecoProdutoUpdateDTO>(retornoEnderecoProduto);
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEnderecoProduto(int empresaId, int enderecoProdutoId)
        {
            try
            {
                var enderecoProduto = await _estoquePersist.GetEnderecoProdutoByIdAsync(empresaId, enderecoProdutoId);
                if (enderecoProduto == null)
                {
                    throw new Exception(MensagemDeErro.EnderecoProdutoNaoEncontradoDelete);
                }
                else
                {
                    _geralPersist.Delete<EnderecoProduto>(enderecoProduto);
                    return await _geralPersist.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EnderecoProdutoViewModelDTO>> GetAllEnderecosProdutosAsync(int empresaId)
        {
            try
            {
                var enderecosProdutos = await _estoquePersist.GetAllEnderecosProdutosAsync(empresaId);
                if (enderecosProdutos == null)
                {
                    return null;
                }
                else
                {
                    List<EnderecoProdutoViewModelDTO> enderecoProdutoRetorno = new List<EnderecoProdutoViewModelDTO>();
                    var estoqueProduto = _validacoesPersist.RetornarProdutosEstoque(empresaId);
                    foreach (var produtoEstoque in estoqueProduto)
                    {
                        if (produtoEstoque.EnderecoProdutoId != null && produtoEstoque.EnderecoProdutoId != (int)ExisteEnderecoProdutoEnum.NaoExisteEndereco)
                        {
                            enderecoProdutoRetorno.Add(new EnderecoProdutoViewModelDTO(produtoEstoque.ProdutoNome, produtoEstoque.NomeEndereco, produtoEstoque.Ativo, produtoEstoque.DataCadastroEnderecoProduto));
                        }
                    }
                    return enderecoProdutoRetorno;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EnderecoProduto> GetEnderecoProdutoByIdAsync(int empresaId, int enderecoProdutoId)
        {
            try
            {
                var enderecoProduto = await _estoquePersist.GetEnderecoProdutoByIdAsync(empresaId, enderecoProdutoId);
                if (enderecoProduto == null)
                {
                    return null;
                }
                else
                {
                    return enderecoProduto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
