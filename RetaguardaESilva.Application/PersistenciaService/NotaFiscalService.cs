using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IValidacoesPersist _validacoesPersist;
        private readonly INotaFiscalPersist _notaFiscalPersist;
        private readonly IPedidoPersist _pedidoPersist;
        private readonly IPedidoNotaPersist _pedidoNotaPersist;
        private readonly ITransportadorPersist _transportadorPersist;
        private readonly IClientePersist _clientePersist;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public NotaFiscalService(IGeralPersist geralPersist, INotaFiscalPersist notaFiscalPersist, ITransportadorPersist transportadorPersist, IValidacoesPersist validacoesPersist, IPedidoPersist pedidoPersist, IPedidoNotaPersist pedidoNotaPersist, IMapper mapper, IMailService mailService, IClientePersist clientePersist)
        {
            _geralPersist = geralPersist;
            _validacoesPersist = validacoesPersist;
            _notaFiscalPersist = notaFiscalPersist;
            _pedidoPersist = pedidoPersist;
            _pedidoNotaPersist = pedidoNotaPersist;
            _transportadorPersist = transportadorPersist;
            _mapper = mapper;
            _mailService = mailService;
            _clientePersist = clientePersist;
        }

        public async Task<NotaFiscalDTO> AddNotaFiscal(NotaFiscalDTO model)
        {
            try
            {
                List<Produto> produtosNotaEmail = new List<Produto>();
                var pedido = await _pedidoPersist.GetPedidoByIdAsync(model.EmpresaId, model.PedidoId);
                var notaFiscalExistente = await _notaFiscalPersist.GetNotaFiscalPedidoByIdAsync(model.EmpresaId, model.PedidoId);
                if (pedido == null || notaFiscalExistente != null)
                {
                    throw new Exception(MensagemDeErro.PedidoNaoEncontradoOuNFExistente);
                }
                else
                {
                    var transportador = await _transportadorPersist.GetTransportadorByIdAsync(model.EmpresaId, pedido.TransportadorId);
                    pedido.Status = (int)StatusPedido.PedidoConfirmado;
                    var pedidoNota = await _pedidoNotaPersist.GetAllPedidosNotaAsync(model.EmpresaId, model.PedidoId);
                    foreach (var item in pedidoNota)
                    {   
                        var produto = _validacoesPersist.AtualizarQuantidadeProdutoPosPedido(model.PedidoId, model.EmpresaId, item.ProdutoId, item.Quantidade, out Estoque estoque, out string mensagem);
                        if (produto == null || estoque == null)
                        {
                            throw new Exception(mensagem);
                        }
                        else
                        {
                            produtosNotaEmail.Add(produto);
                            item.Status = (int)StatusPedido.PedidoConfirmado;
                            _geralPersist.Update<Produto>(produto);
                            _geralPersist.Update<PedidoNota>(item);
                            _geralPersist.Update<Pedido>(pedido);
                            _geralPersist.Update<Estoque>(estoque);
                            await _geralPersist.SaveChangesAsync();
                        }
                    }

                    /*Esse trecho do código é so para atualizar o status dos produtos na tabaela pedidoNota
                    porque no codigo acima quando tem vários produtos no pedido, um produto sempre fica com o status errado*/
                    var pedidoNotaAtualizaStatus = await _pedidoNotaPersist.GetPedidosNotaByIdStatusAsync(model.EmpresaId, model.PedidoId, (int)StatusPedido.PedidoEmAnalise);
                    if (pedidoNotaAtualizaStatus != null)
                    {
                        pedidoNotaAtualizaStatus.Status = (int)StatusPedido.PedidoConfirmado;
                        _geralPersist.Update<PedidoNota>(pedidoNotaAtualizaStatus);
                        await _geralPersist.SaveChangesAsync();
                    }

                    Random random = new Random();
                    List<int> randomNumbers = new List<int>();
                    string chaveAcesso = string.Empty;
                    for (int i = 0; i < 5; i++)
                    {
                        randomNumbers.Add(random.Next(1000, 9999));
                        chaveAcesso += randomNumbers[i];
                    }
                    model.ChaveAcesso = transportador.CNPJ + chaveAcesso;
                    model.Status = (int)StatusNotaFiscal.NotaFiscalAprovada;
                    var notaFiscalDTO = _mapper.Map<NotaFiscal>(model);
                    _geralPersist.Add<NotaFiscal>(notaFiscalDTO);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        var notaFiscal = await _notaFiscalPersist.GetNotaFiscalByIdAsync(notaFiscalDTO.EmpresaId, notaFiscalDTO.Id);
                        var resultadoNotaFiscal = _mapper.Map<NotaFiscalDTO>(notaFiscal);
                        return resultadoNotaFiscal;
                    }
                    else
                    {
                        throw new Exception(MensagemDeErro.ErroAoCadastrarNotaFiscal);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<NotasFiscaisDTO>> GetAllNotaFiscalAsync(int empresaId)
        {
            try
            {
                var notasFiscais = await _notaFiscalPersist.GetAllNotaFiscalAsync(empresaId);
                if (notasFiscais == null)
                {
                    throw new Exception(MensagemDeErro.NotaFiscalNaoEncontrada);
                }
                else if (notasFiscais.Count() == 0)
                {
                    throw new Exception(MensagemDeErro.NotaFiscalNaoEncontradoEmpresa);
                }
                else
                {
                    List<NotasFiscaisDTO> notasFiscaisList = new List<NotasFiscaisDTO>();
                    var notasFiscaisRetorno = _validacoesPersist.RetornarNotasFiscais(notasFiscais);
                    foreach (var item in notasFiscaisRetorno)
                    {
                        notasFiscaisList.Add(new NotasFiscaisDTO(item.Id, item.PedidoId, item.NomeCliente, item.QuantidadeItens, item.PrecoTotal, item.DataCadastroNotaFiscal, item.StatusNota, item.Status));
                    }
                    return notasFiscaisList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NotaFiscalIdDTO> GetNotaFiscalByIdAsync(int empresaId, int notaFiscalId, bool? notaFiscalEmissao, bool? exclusao)
        {
            try
            {
                var notaFiscal = await _notaFiscalPersist.GetNotaFiscalByIdAsync(empresaId, notaFiscalId);
                if (notaFiscal == null)
                {
                    throw new Exception(MensagemDeErro.NotaFiscalNaoEncontrada);
                }
                else
                {
                    var notaFiscalRetornoDTO = new NotaFiscalIdDTO();
                    notaFiscalRetornoDTO.Cliente = new Cliente();
                    notaFiscalRetornoDTO.Transportador = new Transportador();
                    notaFiscalRetornoDTO.Empresa = new Empresa();
                    notaFiscalRetornoDTO.Produto = new List<ProdutoPedidoDTO>();

                    var notasFiscaisRetorno = _validacoesPersist.RetornarNotaFiscal(notaFiscal);

                    notaFiscalRetornoDTO.ClienteId = notasFiscaisRetorno.Cliente.Id;
                    notaFiscalRetornoDTO.TransportadorId = notasFiscaisRetorno.Transportador.Id;
                    notaFiscalRetornoDTO.EmpresaId = notasFiscaisRetorno.Empresa.Id;
                    notaFiscalRetornoDTO.Id = notasFiscaisRetorno.Id;
                    notaFiscalRetornoDTO.PedidoId = notasFiscaisRetorno.Id;
                    notaFiscalRetornoDTO.PrecoTotal = notasFiscaisRetorno.PrecoTotal;
                    notaFiscalRetornoDTO.QuantidadeItens = notasFiscaisRetorno.QuantidadeItens;
                    notaFiscalRetornoDTO.DataCadastroNotaFiscal = notasFiscaisRetorno.DataCadastroNotaFiscal;
                    notaFiscalRetornoDTO.StatusNota = notasFiscaisRetorno.StatusNota;
                    notaFiscalRetornoDTO.ChaveAcesso = notasFiscaisRetorno.ChaveAcesso;
                    if (notaFiscalRetornoDTO.StatusNota == MensagemDeAlerta.NotaFiscalAprovada)
                    {
                        notaFiscalRetornoDTO.Status = (int)StatusNotaFiscal.NotaFiscalAprovada;
                    }
                    else
                    {
                        notaFiscalRetornoDTO.Status = (int)StatusNotaFiscal.NotaFiscalCancelada;
                    }
                    notaFiscalRetornoDTO.Cliente.Id = notasFiscaisRetorno.Cliente.Id;
                    notaFiscalRetornoDTO.Cliente.Nome = notasFiscaisRetorno.Cliente.Nome;
                    notaFiscalRetornoDTO.Cliente.Endereco = notasFiscaisRetorno.Cliente.Endereco;
                    notaFiscalRetornoDTO.Cliente.Bairro = notasFiscaisRetorno.Cliente.Bairro;
                    notaFiscalRetornoDTO.Cliente.Numero = notasFiscaisRetorno.Cliente.Numero;
                    notaFiscalRetornoDTO.Cliente.Municipio = notasFiscaisRetorno.Cliente.Municipio;
                    notaFiscalRetornoDTO.Cliente.UF = notasFiscaisRetorno.Cliente.UF;
                    notaFiscalRetornoDTO.Cliente.Pais = notasFiscaisRetorno.Cliente.Pais;
                    notaFiscalRetornoDTO.Cliente.CEP = notasFiscaisRetorno.Cliente.CEP;
                    notaFiscalRetornoDTO.Cliente.Complemento = notasFiscaisRetorno.Cliente.Complemento;
                    notaFiscalRetornoDTO.Cliente.Telefone = notasFiscaisRetorno.Cliente.Telefone;
                    notaFiscalRetornoDTO.Cliente.Email = notasFiscaisRetorno.Cliente.Email;
                    notaFiscalRetornoDTO.Cliente.CPFCNPJ = notasFiscaisRetorno.Cliente.CPFCNPJ;
                    notaFiscalRetornoDTO.Cliente.InscricaoMunicipal = notasFiscaisRetorno.Cliente.InscricaoMunicipal;
                    notaFiscalRetornoDTO.Cliente.InscricaoEstadual = notasFiscaisRetorno.Cliente.InscricaoEstadual;
                    notaFiscalRetornoDTO.Cliente.DataCadastroCliente = notasFiscaisRetorno.Cliente.DataCadastroCliente;
                    notaFiscalRetornoDTO.Cliente.Ativo = notasFiscaisRetorno.Cliente.Ativo;
                    notaFiscalRetornoDTO.Cliente.EmpresaId = notasFiscaisRetorno.Cliente.EmpresaId;
                    notaFiscalRetornoDTO.Transportador.Id = notasFiscaisRetorno.Transportador.Id;
                    notaFiscalRetornoDTO.Transportador.Nome = notasFiscaisRetorno.Transportador.Nome;
                    notaFiscalRetornoDTO.Transportador.Endereco = notasFiscaisRetorno.Transportador.Endereco;
                    notaFiscalRetornoDTO.Transportador.Bairro = notasFiscaisRetorno.Transportador.Bairro;
                    notaFiscalRetornoDTO.Transportador.Numero = notasFiscaisRetorno.Transportador.Numero;
                    notaFiscalRetornoDTO.Transportador.Municipio = notasFiscaisRetorno.Transportador.Municipio;
                    notaFiscalRetornoDTO.Transportador.UF = notasFiscaisRetorno.Transportador.UF;
                    notaFiscalRetornoDTO.Transportador.Pais = notasFiscaisRetorno.Transportador.Pais;
                    notaFiscalRetornoDTO.Transportador.CEP = notasFiscaisRetorno.Transportador.CEP;
                    notaFiscalRetornoDTO.Transportador.Complemento = notasFiscaisRetorno.Transportador.Complemento;
                    notaFiscalRetornoDTO.Transportador.Telefone = notasFiscaisRetorno.Transportador.Telefone;
                    notaFiscalRetornoDTO.Transportador.Email = notasFiscaisRetorno.Transportador.Email;
                    notaFiscalRetornoDTO.Transportador.CNPJ = notasFiscaisRetorno.Transportador.CNPJ;
                    notaFiscalRetornoDTO.Transportador.InscricaoMunicipal = notasFiscaisRetorno.Transportador.InscricaoMunicipal;
                    notaFiscalRetornoDTO.Transportador.InscricaoEstadual = notasFiscaisRetorno.Transportador.InscricaoEstadual;
                    notaFiscalRetornoDTO.Transportador.DataCadastroTransportador = notasFiscaisRetorno.Transportador.DataCadastroTransportador;
                    notaFiscalRetornoDTO.Transportador.Ativo = notasFiscaisRetorno.Transportador.Ativo;
                    notaFiscalRetornoDTO.Transportador.EmpresaId = notasFiscaisRetorno.Transportador.EmpresaId;
                    notaFiscalRetornoDTO.Empresa.Id = notasFiscaisRetorno.Empresa.Id;
                    notaFiscalRetornoDTO.Empresa.Nome = notasFiscaisRetorno.Empresa.Nome;
                    notaFiscalRetornoDTO.Empresa.Endereco = notasFiscaisRetorno.Empresa.Endereco;
                    notaFiscalRetornoDTO.Empresa.Bairro = notasFiscaisRetorno.Empresa.Bairro;
                    notaFiscalRetornoDTO.Empresa.Numero = notasFiscaisRetorno.Empresa.Numero;
                    notaFiscalRetornoDTO.Empresa.Municipio = notasFiscaisRetorno.Empresa.Municipio;
                    notaFiscalRetornoDTO.Empresa.UF = notasFiscaisRetorno.Empresa.UF;
                    notaFiscalRetornoDTO.Empresa.Pais = notasFiscaisRetorno.Empresa.Pais;
                    notaFiscalRetornoDTO.Empresa.CEP = notasFiscaisRetorno.Empresa.CEP;
                    notaFiscalRetornoDTO.Empresa.Complemento = notasFiscaisRetorno.Empresa.Complemento;
                    notaFiscalRetornoDTO.Empresa.Telefone = notasFiscaisRetorno.Empresa.Telefone;
                    notaFiscalRetornoDTO.Empresa.Email = notasFiscaisRetorno.Empresa.Email;
                    notaFiscalRetornoDTO.Empresa.CNPJ = notasFiscaisRetorno.Empresa.CNPJ;
                    notaFiscalRetornoDTO.Empresa.InscricaoMunicipal = notasFiscaisRetorno.Empresa.InscricaoMunicipal;
                    notaFiscalRetornoDTO.Empresa.InscricaoEstadual = notasFiscaisRetorno.Empresa.InscricaoEstadual;
                    notaFiscalRetornoDTO.Empresa.DataCadastroEmpresa = notasFiscaisRetorno.Empresa.DataCadastroEmpresa;
                    notaFiscalRetornoDTO.Empresa.Ativo = notasFiscaisRetorno.Empresa.Ativo;

                    foreach (var item in notasFiscaisRetorno.Produto)
                    {
                        var produtoDTO = new ProdutoPedidoDTO()
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            Quantidade = item.Quantidade,
                            QuantidadeVenda = item.QuantidadeVenda,
                            Ativo = item.Ativo,
                            PrecoCompra = item.PrecoCompra,
                            PrecoVenda = item.PrecoVenda,
                            PrecoVendaTotal = item.PrecoVendaTotal,
                            Codigo = item.Codigo,
                            DataCadastroProduto = item.DataCadastroProduto,
                            EmpresaId = item.EmpresaId,
                            FornecedorId = item.FornecedorId
                        };
                        notaFiscalRetornoDTO.Produto.Add(produtoDTO);
                    }
                    if (notaFiscalEmissao == true && exclusao == false)
                    {
                        await Task.Delay(3000);
                        var clienteEmail = await _clientePersist.GetClienteByIdAsync(notaFiscalRetornoDTO.EmpresaId, notaFiscalRetornoDTO.ClienteId);
                        var assunto = MensagemDeAlerta.EmailPedidoConfirmado;
                        var corpo = String.Concat(MensagemDeAlerta.EmailPedidoConfirmadoCorpo, notaFiscalRetornoDTO.PedidoId.ToString() + ".", MensagemDeAlerta.EmailPedidoValorTotal, notaFiscalRetornoDTO.PrecoTotal.ToString("C", new CultureInfo("pt-BR")).ToString(), MensagemDeAlerta.EmailNotaFiscal, notaFiscal.Id.ToString());
                        _mailService.SendMail(clienteEmail.Email, assunto, corpo, true, notaFiscal.Id, false, false);
                    }else if (notaFiscalEmissao == true && exclusao == true)
                    {
                        await Task.Delay(3000);
                        var clienteEmail = await _clientePersist.GetClienteByIdAsync(notaFiscalRetornoDTO.EmpresaId, notaFiscalRetornoDTO.ClienteId);
                        var assunto = MensagemDeAlerta.EmailPedidoExluido;
                        var corpo = String.Concat(MensagemDeAlerta.EmailNotaFiscalExluido, notaFiscalRetornoDTO.PedidoId.ToString());
                        _mailService.SendMail(clienteEmail.Email, assunto, corpo, true, notaFiscal.Id, exclusao, false);
                    }
                    return notaFiscalRetornoDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NotaFiscalDTO> GetNotaFiscalPedidoByIdAsync(int empresaId, int pedidoId)
        {
            try
            {
                var notaPedido = await _notaFiscalPersist.GetNotaFiscalPedidoByIdAsync(empresaId, pedidoId);
                if (notaPedido == null)
                {
                    throw new Exception(MensagemDeErro.NotaFiscalNaoEncontrada);
                }
                else
                {
                    var resultadoNotaPedido = _mapper.Map<NotaFiscalDTO>(notaPedido);
                    return resultadoNotaPedido;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NotaFiscalDTO> CancelarNotaFiscal(int empresaId, int notaFiscalId)
        {
            try
            {
                var notaFiscal = await _notaFiscalPersist.GetNotaFiscalByIdAsync(empresaId, notaFiscalId);
                var pedido = await _pedidoPersist.GetPedidoByIdAsync(notaFiscal.EmpresaId, notaFiscal.PedidoId);
                if (notaFiscal == null || pedido == null)
                {
                    throw new Exception(MensagemDeErro.NotaFiscalNaoEncontradaDelete);
                }
                else
                {
                    if (_validacoesPersist.AtualizarQuantidadeProdutoEstoquePosDeletePedido(pedido, out List<Produto> produtos, out List<Estoque> estoques, out List<PedidoNota> pedidosNotas))
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
                            item.Status = (int)StatusPedido.PedidoCancelado;
                            _geralPersist.Update(item);
                            await _geralPersist.SaveChangesAsync();
                        }

                        notaFiscal.Status = (int)StatusNotaFiscal.NotaFiscalCancelada;
                        _geralPersist.Update<NotaFiscal>(notaFiscal);
                        await _geralPersist.SaveChangesAsync();

                        pedido.Status = (int)StatusPedido.PedidoCancelado;
                        _geralPersist.Update<Pedido>(pedido);
                        await _geralPersist.SaveChangesAsync();
                        var resultadoNotaFiscal = _mapper.Map<NotaFiscalDTO>(notaFiscal);
                        return resultadoNotaFiscal;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
