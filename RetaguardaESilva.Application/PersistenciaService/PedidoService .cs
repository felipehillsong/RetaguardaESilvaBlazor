using AutoMapper;
using Newtonsoft.Json;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.DTO;
using RetaguardaESilva.Application.Helpers;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Domain.ViewModels;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Persistencias;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class PedidoService : IPedidoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly IPedidoPersist _pedidoPersist;
        private readonly IPedidoNotaPersist _pedidoNotaPersist;
        private readonly IClientePersist _clientePersist;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public PedidoService(IGeralPersist geralPersist, IValidacoesPersist validacoesPersist, IPedidoPersist pedidoPersist, IPedidoNotaPersist pedidoNotaPersist, IClientePersist clientePersist, IMapper mapper, IMailService mailService)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _pedidoPersist = pedidoPersist;
            _pedidoNotaPersist = pedidoNotaPersist;
            _clientePersist = clientePersist;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<PedidoCreateDTO> AddPedido(PedidoCreateDTO model)
        {
            try
            {
                foreach (var item in model.Produtos)
                {
                    if (!_validacoesPersist.VerificaQuantidade(model.EmpresaId, item.Id, item.QuantidadeVenda, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                }
                
                model.Status = (int)StatusPedido.PedidoEmAnalise;
                var pedidoCreateDTO = _mapper.Map<Pedido>(model);
                _geralPersist.Add<Pedido>(pedidoCreateDTO);
                if(await _geralPersist.SaveChangesAsync())
                {
                    var retornoPedido = await _pedidoPersist.GetPedidoByIdAsync(pedidoCreateDTO.EmpresaId, pedidoCreateDTO.Id);
                    if (retornoPedido != null)
                    {   
                        foreach (var item in model.Produtos)
                        {
                            var pedidoNotaDTO = new PedidoNotaDTO()
                            {
                                Id = (int)Ids.IdCreate,
                                PedidoId = retornoPedido.Id,
                                ClienteId = retornoPedido.ClienteId,
                                FornecedorId = item.FornecedorId,
                                ProdutoId = item.Id,
                                CodigoProduto = item.Codigo,
                                EmpresaId = item.EmpresaId,
                                TransportadorId = retornoPedido.TransportadorId,
                                UsuarioId = retornoPedido.UsuarioId,
                                Quantidade = item.QuantidadeVenda,
                                PrecoVenda = item.PrecoVenda,
                                PrecoTotal = item.QuantidadeVenda * item.PrecoVenda,
                                DataCadastroPedidoNota = model.DataCadastroPedido,
                                Status = retornoPedido.Status
                            };
                            var pedidoNotaCreate = _mapper.Map<PedidoNota>(pedidoNotaDTO);
                            _geralPersist.Add<PedidoNota>(pedidoNotaCreate);
                            if (await _geralPersist.SaveChangesAsync())
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception(MensagemDeErro.ErroAoCadastrarItensPedido);
                            }
                        }
                        List<String> produtosEmails = new List<String>();
                        foreach (var item in model.Produtos)
                        {
                            decimal valorTotal = item.QuantidadeVenda * item.PrecoVenda;
                            produtosEmails.Add("\nNome do produto: " + item.Nome + "\nQuantidade de compra: " + item.QuantidadeVenda.ToString() + "\nValor de compra do produto: " + item.PrecoVenda.ToString("C", new CultureInfo("pt-BR")) + "\nValor Total: " + valorTotal.ToString("C", new CultureInfo("pt-BR")));
                        }
                        var produtosEmail = string.Join("\n", produtosEmails);
                        var clienteEmail = await _clientePersist.GetClienteByIdAsync(model.EmpresaId, model.ClienteId);
                        var assunto = MensagemDeAlerta.EmailPedidoEmAnalise;
                        var corpo = String.Concat(MensagemDeAlerta.EmailPedidoEmAnaliseCorpo, retornoPedido.Id.ToString() + ".", MensagemDeAlerta.EmailPedidoProdutos, "\n"+produtosEmail, MensagemDeAlerta.EmailPedidoValorTotal, retornoPedido.PrecoTotal.ToString("C", new CultureInfo("pt-BR"))).ToString();
                        _mailService.SendMail(clienteEmail.Email, assunto, corpo, false, null, false, false);
                        var retornoPedidoCompleto = _mapper.Map<PedidoCreateDTO>(retornoPedido);
                        return retornoPedidoCompleto;
                    }
                }
                throw new Exception(MensagemDeErro.ErroAoCadastrarPedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 

        public async Task<PedidoUpdateDTO> UpdatePedido(PedidoUpdateDTO model)
        {
            try
            {
                foreach (var item in model.Produtos)
                {
                    if (!_validacoesPersist.VerificaQuantidade(model.EmpresaId, item.Id, item.QuantidadeVenda, out string mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                }
                model.Status = (int)StatusPedido.PedidoEmAnalise;
                var pedidoUpdateDTO = _mapper.Map<Pedido>(model);
                _geralPersist.Update<Pedido>(pedidoUpdateDTO);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var retornoPedido = await _pedidoPersist.GetPedidoByIdAsync(pedidoUpdateDTO.EmpresaId, pedidoUpdateDTO.Id);
                    if (retornoPedido != null)
                    {
                        foreach (var item in model.Produtos)
                        {
                            var pedidosNotas = await _pedidoNotaPersist.GetPedidosNotaByIdAsync(retornoPedido.EmpresaId, retornoPedido.Id, item.Id);
                            if (pedidosNotas == null)
                            {
                                var pedidoNotaDTO = new PedidoNotaDTO()
                                {
                                    Id = (int)Ids.IdCreate,
                                    PedidoId = model.Id,
                                    ClienteId = model.ClienteId,
                                    FornecedorId = item.FornecedorId,
                                    ProdutoId = item.Id,
                                    CodigoProduto = item.Codigo,
                                    EmpresaId = item.EmpresaId,
                                    TransportadorId = model.TransportadorId,
                                    UsuarioId = model.UsuarioId,
                                    Quantidade = item.QuantidadeVenda,
                                    PrecoVenda = item.PrecoVenda,
                                    PrecoTotal = item.QuantidadeVenda * item.PrecoVenda,
                                    DataCadastroPedidoNota = model.DataCadastroPedido,
                                    Status = retornoPedido.Status
                                };
                                var pedidoNotaCreate = _mapper.Map<PedidoNota>(pedidoNotaDTO);
                                _geralPersist.Add<PedidoNota>(pedidoNotaCreate);
                                if (await _geralPersist.SaveChangesAsync())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                var pedidoNotaUpdateDTO = new PedidoNotaUpdateDTO()
                                {
                                    Id = pedidosNotas.Id,
                                    PedidoId = retornoPedido.Id,
                                    ClienteId = model.ClienteId,
                                    FornecedorId = item.FornecedorId,
                                    ProdutoId = item.Id,
                                    CodigoProduto = item.Codigo,
                                    EmpresaId = item.EmpresaId,
                                    TransportadorId = model.TransportadorId,
                                    UsuarioId = model.UsuarioId,
                                    Quantidade = item.QuantidadeVenda,
                                    PrecoVenda = item.PrecoVenda,
                                    PrecoTotal = item.QuantidadeVenda * item.PrecoVenda,
                                    DataCadastroPedidoNota = pedidosNotas.DataCadastroPedidoNota,
                                    Status = retornoPedido.Status
                                };
                                var pedidoNotaUpdate = _mapper.Map<PedidoNota>(pedidoNotaUpdateDTO);
                                _geralPersist.Update<PedidoNota>(pedidoNotaUpdate);
                                await _geralPersist.SaveChangesAsync();
                            }
                        }
                        var pedidoNota = await _pedidoNotaPersist.GetAllPedidosNotaAsync(model.EmpresaId, model.Id);
                        foreach (var item in pedidoNota)
                        {
                            var produto = model.Produtos.Where(p => p.Id == item.ProdutoId).FirstOrDefault();
                            if (produto == null)
                            {
                                var pedidoNotaDelete = await _pedidoNotaPersist.GetPedidosNotaByIdAsync(item.EmpresaId, model.Id, item.ProdutoId);
                                _geralPersist.Delete<PedidoNota>(pedidoNotaDelete);
                                await _geralPersist.SaveChangesAsync();
                            }
                        }
                        if (model.EnviarEmailPosFinalizar)
                        {
                            List<String> produtosEmails = new List<String>();
                            foreach (var item in model.Produtos)
                            {
                                decimal valorTotal = item.QuantidadeVenda * item.PrecoVenda;
                                produtosEmails.Add("\nNome do produto: " + item.Nome + "\nQuantidade de compra: " + item.QuantidadeVenda.ToString() + "\nValor de compra do produto: " + item.PrecoVenda.ToString("C", new CultureInfo("pt-BR")) + "\nValor Total: " + valorTotal.ToString("C", new CultureInfo("pt-BR")));
                            }
                            var produtosEmail = string.Join("\n", produtosEmails);
                            var clienteEmail = await _clientePersist.GetClienteByIdAsync(model.EmpresaId, model.ClienteId);
                            var assunto = MensagemDeAlerta.EmailPedidoEmAnalise;
                            var corpo = String.Concat(MensagemDeAlerta.EmailPedidoEmAnaliseAtualizarCorpo, retornoPedido.Id.ToString() + ".", MensagemDeAlerta.EmailPedidoProdutos, "\n" + produtosEmail, MensagemDeAlerta.EmailPedidoValorTotal, retornoPedido.PrecoTotal.ToString("C", new CultureInfo("pt-BR"))).ToString();
                            _mailService.SendMail(clienteEmail.Email, assunto, corpo, false, null, false, false);
                        }
                        var retornoPedidoCompleto = _mapper.Map<PedidoUpdateDTO>(retornoPedido);
                        return retornoPedidoCompleto;
                    }
                }
                throw new Exception(MensagemDeErro.ErroAoCadastrarPedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePedido(int empresaId, int pedidoId)
        {
            try
            {
                var pedido = await _pedidoPersist.GetPedidoByIdAsync(empresaId, pedidoId);
                if (pedido == null)
                {
                    throw new Exception(MensagemDeErro.PedidoNaoEncontradoDelete);
                }
                else
                {
                    if(_validacoesPersist.AtualizarQuantidadeProdutoEstoquePosDeletePedido(pedido, out List<Produto> produtos, out List<Estoque> estoques, out List<PedidoNota> pedidosNotas))
                    {
                        foreach (var item in produtos)
                        {
                            if (item != null)
                            {
                                _geralPersist.Update<Produto>(item);
                                await _geralPersist.SaveChangesAsync();
                            }
                        }
                        foreach (var item in estoques)
                        {
                            if (item != null)
                            {
                                _geralPersist.Update<Estoque>(item);
                                await _geralPersist.SaveChangesAsync();
                            }
                        }
                        foreach (var item in pedidosNotas)
                        {
                            _geralPersist.Delete(item);
                            await _geralPersist.SaveChangesAsync();
                        }

                        if (produtos.Count == (int)StatusPedido.RetornoPedido && estoques.Count == (int)StatusPedido.RetornoPedido && pedidosNotas.Count == (int)StatusPedido.RetornoPedido)
                        {
                            var pedidoNotaDelete = await _pedidoNotaPersist.GetAllPedidosNotaAsync(empresaId, pedidoId);
                            foreach (var item in pedidoNotaDelete)
                            {
                                _geralPersist.Delete<PedidoNota>(item);
                                await _geralPersist.SaveChangesAsync();
                            }
                        }
                        _geralPersist.Delete<Pedido>(pedido);
                        if(await _geralPersist.SaveChangesAsync())
                        {
                            var clienteEmail = await _clientePersist.GetClienteByIdAsync(pedido.EmpresaId, pedido.ClienteId);
                            var assunto = MensagemDeAlerta.PedidoCancelado;
                            var corpo = String.Concat("O pedido " + pedidoId + " foi cancelado com sucesso!");
                            _mailService.SendMail(clienteEmail.Email, assunto, corpo, false, null, false);
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

        public async Task<IEnumerable<PedidoRetornoDTO>> GetAllPedidosAsync(int empresaId)
        {
            try
            {
                var pedidos = await _pedidoPersist.GetAllPedidosAsync(empresaId);
                if (pedidos == null)
                {
                    throw new Exception(MensagemDeErro.PedidoNaoEncontradoEmpresa);
                }
                else if (pedidos.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.PedidoNaoEncontrado);
                }
                else
                {
                    var pedidosViewModel = _mapper.Map<IEnumerable<PedidoViewModel>>(pedidos);
                    var pedidosRetorno = _validacoesPersist.RetornarPedidosView(pedidosViewModel);
                    var resultadoPedidos = _mapper.Map<IEnumerable<PedidoRetornoDTO>>(pedidosRetorno);
                    return resultadoPedidos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PedidoRetornoDTO> GetPedidoByIdAsync(int empresaId, int pedidoId)
        {
            try
            {
                var pedido = await _pedidoPersist.GetPedidoByIdAsync(empresaId, pedidoId);
                if (pedido == null)
                {
                    throw new Exception(MensagemDeErro.PedidoNaoEncontrado);
                }
                else
                {
                    var pedidoRetorno = _validacoesPersist.MontarPedidoRetorno(pedido);
                    var resultadoPedido = _mapper.Map<PedidoRetornoDTO>(pedidoRetorno);
                    return resultadoPedido;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
