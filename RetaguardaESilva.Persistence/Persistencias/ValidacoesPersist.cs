using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Domain.Models;
using RetaguardaESilva.Persistence.Data;
using RetaguardaESilva.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetaguardaESilva.Domain.Enumeradores;
using RetaguardaESilva.Domain.Mensagem;
using System.Text.RegularExpressions;
using ProEventos.Persistence.Persistencias;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using RetaguardaESilva.Domain.ViewModels;
using Microsoft.OpenApi.Extensions;

namespace RetaguardaESilva.Persistence.Persistencias
{
    public class ValidacoesPersist : IValidacoesPersist
    {
        private readonly RetaguardaESilvaContext _context;
        public ValidacoesPersist(RetaguardaESilvaContext context)
        {
            _context = context;
        }

        public bool ExisteCliente(int empresaId, string cnpjCpf, string inscricaoMunicipal, string inscricaoEstadual, int clienteId, bool isUpdate, out string mensagem)
        {
            var clienteCpfCnpjRetorno = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == empresaId && c.CPFCNPJ == cnpjCpf && c.Id != clienteId);
            var clienteImRetorno = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == empresaId && c.InscricaoMunicipal == inscricaoMunicipal && c.Id != clienteId);
            var clienteIeRetorno = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == empresaId && c.InscricaoEstadual == inscricaoEstadual && c.Id != clienteId);
            var clienteCpfCnpjImIe = _context.Cliente.AsNoTracking().Where(c => c.EmpresaId == empresaId);

            if (ExisteEmpresaCadastrada(empresaId))
            {
                if (isUpdate)
                {
                    if (clienteId != null || clienteId != 0)
                    {
                        if (clienteCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.ClienteJaCadastrado;
                            return true;
                        }
                        else if (clienteImRetorno != null && clienteIeRetorno != null)
                        {
                            if (clienteImRetorno.InscricaoMunicipal == String.Empty && clienteIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else if (clienteImRetorno.InscricaoMunicipal == null && clienteIeRetorno.InscricaoEstadual == null)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.ClienteJaCadastrado;
                                return true;
                            }
                        }
                        else if (clienteImRetorno == null && clienteIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (clienteImRetorno != null)
                        {
                            if (clienteImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.ClienteJaCadastrado;
                                return true;
                            }
                        }
                        else if (clienteIeRetorno != null)
                        {
                            if (clienteIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.ClienteJaCadastrado;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.ClienteJaCadastrado;
                            return true;
                        }
                    }
                    else
                    {
                        mensagem = MensagemDeErro.ClienteSemId;
                        return true;
                    }
                }
                else
                {
                    if (clienteCpfCnpjImIe == null)
                    {
                        mensagem = MensagemDeSucesso.CadastrarOk;
                        return false;
                    }
                    else
                    {
                        if (clienteCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.ClienteJaCadastrado;
                            return true;
                        }
                        else if (clienteImRetorno != null && clienteIeRetorno != null)
                        {
                            if (clienteImRetorno.InscricaoMunicipal == String.Empty && clienteIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.CadastrarOk;
                                return false;
                            }
                            else if (clienteImRetorno.InscricaoMunicipal == null && clienteIeRetorno.InscricaoEstadual == null)
                            {
                                mensagem = MensagemDeSucesso.CadastrarOk;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.ClienteJaCadastrado;
                                return true;
                            }
                        }
                        else if (clienteImRetorno == null && clienteIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.CadastrarOk;
                            return false;
                        }
                        else if (clienteImRetorno != null)
                        {
                            if (clienteImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.CadastrarOk;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.ClienteJaCadastrado;
                                return true;
                            }
                        }
                        else if (clienteIeRetorno != null)
                        {
                            if (clienteIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.CadastrarOk;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.ClienteJaCadastrado;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.ClienteJaCadastrado;
                            return true;
                        }
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaClienteInexistente;
                return true;
            }
        }

        public bool ExisteFornecedor(int empresaId, string cnpj, string inscricaoMunicipal, string inscricaoEstadual, int fornecedorId, bool isUpdate, out string mensagem)
        {
            var fornecedorCpfCnpjRetorno = _context.Fornecedor.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.CNPJ == cnpj && f.Id != fornecedorId);
            var fornecedorImRetorno = _context.Fornecedor.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.InscricaoMunicipal == inscricaoMunicipal && f.Id != fornecedorId);
            var fornecedorIeRetorno = _context.Fornecedor.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.InscricaoEstadual == inscricaoEstadual && f.Id != fornecedorId);
            var fornecedorCpfCnpjImIe = _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId);

            if (ExisteEmpresaCadastrada(empresaId))
            {
                if (isUpdate)
                {
                    if (fornecedorId != null || fornecedorId != 0)
                    {
                        if (fornecedorCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.FornecedorJaCadastrado;
                            return true;
                        }
                        else if (fornecedorImRetorno != null && fornecedorIeRetorno != null)
                        {
                            if (fornecedorImRetorno.InscricaoMunicipal == String.Empty && fornecedorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.FornecedorJaCadastrado;
                                return true;
                            }
                        }
                        else if (fornecedorImRetorno == null && fornecedorIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (fornecedorImRetorno != null)
                        {
                            if (fornecedorImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.FornecedorJaCadastrado;
                                return true;
                            }
                        }
                        else if (fornecedorIeRetorno != null)
                        {
                            if (fornecedorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.FornecedorJaCadastrado;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.FornecedorJaCadastrado;
                            return true;
                        }
                    }
                    else
                    {
                        mensagem = MensagemDeErro.FornecedorSemId;
                        return true;
                    }
                }
                else
                {
                    if (fornecedorCpfCnpjImIe == null)
                    {
                        mensagem = MensagemDeSucesso.CadastrarOk;
                        return false;
                    }
                    else
                    {
                        if (fornecedorCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.FornecedorJaCadastrado;
                            return true;
                        }
                        else if (fornecedorImRetorno != null && fornecedorIeRetorno != null)
                        {
                            if (fornecedorImRetorno.InscricaoMunicipal == String.Empty && fornecedorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else if (fornecedorImRetorno.InscricaoMunicipal == null && fornecedorIeRetorno.InscricaoEstadual == null)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.FornecedorJaCadastrado;
                                return true;
                            }
                        }
                        else if (fornecedorImRetorno == null && fornecedorIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (fornecedorImRetorno != null)
                        {
                            if (fornecedorImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.FornecedorJaCadastrado;
                                return true;
                            }
                        }
                        else if (fornecedorIeRetorno != null)
                        {
                            if (fornecedorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.FornecedorJaCadastrado;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.FornecedorJaCadastrado;
                            return true;
                        }
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaFornecedorInexistente;
                return true;
            }
        }

        public bool ExisteTransportador(int empresaId, string cnpj, string inscricaoMunicipal, string inscricaoEstadual, int transportadorId, bool isUpdate, out string mensagem)
        {
            var transportadorCpfCnpjRetorno = _context.Transportador.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.CNPJ == cnpj && f.Id != transportadorId);
            var transportadorImRetorno = _context.Transportador.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.InscricaoMunicipal == inscricaoMunicipal && f.Id != transportadorId);
            var transportadorIeRetorno = _context.Transportador.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.InscricaoEstadual == inscricaoEstadual && f.Id != transportadorId);
            var transportadorCpfCnpjImIe = _context.Transportador.AsNoTracking().Where(f => f.EmpresaId == empresaId);

            if (ExisteEmpresaCadastrada(empresaId))
            {
                if (isUpdate)
                {
                    if (transportadorId != null || transportadorId != 0)
                    {
                        if (transportadorCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.TransportadorJaCadastrado;
                            return true;
                        }
                        else if (transportadorImRetorno != null && transportadorIeRetorno != null)
                        {
                            if (transportadorImRetorno.InscricaoMunicipal == String.Empty && transportadorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.TransportadorJaCadastrado;
                                return true;
                            }
                        }
                        else if (transportadorImRetorno == null && transportadorIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (transportadorImRetorno != null)
                        {
                            if (transportadorImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.TransportadorJaCadastrado;
                                return true;
                            }
                        }
                        else if (transportadorIeRetorno != null)
                        {
                            if (transportadorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.TransportadorJaCadastrado;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.TransportadorJaCadastrado;
                            return true;
                        }
                    }
                    else
                    {
                        mensagem = MensagemDeErro.TransportadorSemId;
                        return true;
                    }
                }
                else
                {
                    if (transportadorCpfCnpjImIe == null)
                    {
                        mensagem = MensagemDeSucesso.CadastrarOk;
                        return false;
                    }
                    else
                    {
                        if (transportadorCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.TransportadorJaCadastrado;
                            return true;
                        }
                        else if (transportadorImRetorno != null && transportadorIeRetorno != null)
                        {
                            if (transportadorImRetorno.InscricaoMunicipal == String.Empty && transportadorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else if (transportadorImRetorno.InscricaoMunicipal == null && transportadorIeRetorno.InscricaoEstadual == null)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.TransportadorJaCadastrado;
                                return true;
                            }
                        }
                        else if (transportadorImRetorno == null && transportadorIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (transportadorImRetorno != null)
                        {
                            if (transportadorImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.TransportadorJaCadastrado;
                                return true;
                            }
                        }
                        else if (transportadorIeRetorno != null)
                        {
                            if (transportadorIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.TransportadorJaCadastrado;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.TransportadorJaCadastrado;
                            return true;
                        }
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaTransportadorInexistente;
                return true;
            }
        }

        public bool ExisteFuncionario(int empresaId, string cpf, int funcionarioId, bool isUpdate, out string mensagem)
        {
            var funcionarioCpfUpdateRetorno = _context.Funcionario.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.CPF == cpf && f.Id != funcionarioId);
            var funcionarioCpfAdd = _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId);
            if (ExisteEmpresaCadastrada(empresaId))
            {
                if (isUpdate)
                {
                    if (funcionarioId != null || funcionarioId != 0)
                    {
                        if (funcionarioCpfUpdateRetorno != null)
                        {
                            mensagem = MensagemDeErro.FuncionarioJaCadastrado;
                            return true;
                        }
                        else
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                    }
                    else
                    {
                        mensagem = MensagemDeErro.FuncionarioSemId;
                        return true;
                    }
                }
                else
                {
                    var funcionarioCpf = funcionarioCpfAdd.FirstOrDefault(f => f.CPF == cpf);
                    if (funcionarioCpf != null)
                    {
                        mensagem = MensagemDeErro.FuncionarioJaCadastrado;
                        return true;
                    }
                    else
                    {
                        mensagem = MensagemDeSucesso.CadastrarOk;
                        return false;
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaFuncionarioInexistente;
                return true;
            }

        }

        public bool ExisteEmpresa(int empresaId, int id, string cnpj, string inscricaoMunicipal, string inscricaoEstadual, bool isUpdate, out string mensagem)
        {
            if (VerificaAdministrador(empresaId, out mensagem))
            {
                var empresaCpfCnpjRetorno = _context.Empresa.AsNoTracking().FirstOrDefault(e => e.CNPJ == cnpj && e.Id != id);
                var empresaImRetorno = _context.Empresa.AsNoTracking().FirstOrDefault(e => e.InscricaoMunicipal == inscricaoMunicipal && e.Id != id);
                var empresaIeRetorno = _context.Empresa.AsNoTracking().FirstOrDefault(e => e.InscricaoEstadual == inscricaoEstadual && e.Id != id);
                var empresaCpfCnpjImIe = _context.Empresa.AsNoTracking().Where(e => e.Id == id);

                if (isUpdate)
                {
                    if (id != null || id != 0)
                    {
                        if (empresaCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.EmpresaJaCadastrada;
                            return true;
                        }
                        else if (empresaImRetorno != null && empresaIeRetorno != null)
                        {
                            if (empresaImRetorno.InscricaoMunicipal == String.Empty && empresaIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.EmpresaJaCadastrada;
                                return true;
                            }
                        }
                        else if (empresaImRetorno == null && empresaIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (empresaImRetorno != null)
                        {
                            if (empresaImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.EmpresaJaCadastrada;
                                return true;
                            }
                        }
                        else if (empresaIeRetorno != null)
                        {
                            if (empresaIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.EmpresaJaCadastrada;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.EmpresaJaCadastrada;
                            return true;
                        }
                    }
                    else
                    {
                        mensagem = MensagemDeErro.EmpresaSemId;
                        return true;
                    }
                }
                else
                {
                    if (empresaCpfCnpjImIe == null)
                    {
                        mensagem = MensagemDeSucesso.CadastrarOk;
                        return false;
                    }
                    else
                    {
                        if (empresaCpfCnpjRetorno != null)
                        {
                            mensagem = MensagemDeErro.EmpresaJaCadastrada;
                            return true;
                        }
                        else if (empresaImRetorno != null && empresaIeRetorno != null)
                        {
                            if (empresaImRetorno.InscricaoMunicipal == String.Empty && empresaIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.EmpresaJaCadastrada;
                                return true;
                            }
                        }
                        else if (empresaImRetorno == null && empresaIeRetorno == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                        else if (empresaImRetorno != null)
                        {
                            if (empresaImRetorno.InscricaoMunicipal == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.EmpresaJaCadastrada;
                                return true;
                            }
                        }
                        else if (empresaIeRetorno != null)
                        {
                            if (empresaIeRetorno.InscricaoEstadual == String.Empty)
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                return false;
                            }
                            else
                            {
                                mensagem = MensagemDeErro.EmpresaJaCadastrada;
                                return true;
                            }
                        }
                        else
                        {
                            mensagem = MensagemDeErro.EmpresaJaCadastrada;
                            return true;
                        }
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.CadastrarEmpresa;
                return true;
            }

        }

        public bool ExisteUsuario(int empresaId, int usuarioId, string email, bool isUpdate, out string mensagem)
        {
            var usuarioCpfUpdateRetorno = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.EmpresaId == empresaId && u.Email == email && u.Id != usuarioId);
            var usuarioCpfAdd = _context.Usuario.AsNoTracking().Where(u => u.EmpresaId == empresaId);
            if (ExisteEmpresaCadastrada(empresaId))
            {
                if (isUpdate)
                {
                    if (usuarioId != null || usuarioId != 0)
                    {
                        if (usuarioCpfUpdateRetorno != null)
                        {
                            mensagem = MensagemDeErro.UsuarioJaCadastrado;
                            return true;
                        }
                        else
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            return false;
                        }
                    }
                    else
                    {
                        mensagem = MensagemDeErro.UsuarioSemId;
                        return true;
                    }
                }
                else
                {
                    var usuarioCpf = usuarioCpfAdd.FirstOrDefault(u => u.Email == email);
                    if (usuarioCpf != null)
                    {
                        mensagem = MensagemDeErro.UsuarioJaCadastrado;
                        return true;
                    }
                    else
                    {
                        mensagem = MensagemDeSucesso.CadastrarOk;
                        return false;
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaUsuarioInexistente;
                return true;
            }
        }

        public Produto ExisteProduto(int empresaId, string nomeProduto, decimal precoCompra, decimal precoVenda, double codigo, out string mensagem)
        {
            if (ExisteEmpresaCadastrada(empresaId))
            {
                var produto = _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.Nome == nomeProduto && p.PrecoCompra == precoCompra && p.PrecoVenda == precoVenda && p.Codigo == codigo).OrderBy(p => p.Id).FirstOrDefault();
                if (produto == null)
                {
                    mensagem = MensagemDeSucesso.CadastrarOk;
                    return null;
                }
                else
                {
                    var nomeProdutoView = nomeProduto.Replace(" ", "");
                    var nomeProdutoRetorno = produto.Nome.Replace(" ", "");
                    if (nomeProdutoRetorno == nomeProdutoView)
                    {
                        mensagem = MensagemDeErro.ExisteProduto;
                        return produto;
                    }
                    else
                    {
                        mensagem = MensagemDeErro.ProdutoErroAoConsultar;
                        return null;
                    }
                }
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaProdutoInexistente;
                return null;
            }
        }

        public bool ExisteProdutoUpdate(Produto produtoBanco, Produto produtoView, out Produto produtoAtualizaQuantidade, out string mensagem)
        {
            if (ExisteEmpresaCadastrada(produtoView.EmpresaId))
            {
                if (produtoView.Id != null && produtoView.Id != 0)
                {
                    var produtoRetornoUpdate = _context.Produto.AsNoTracking().Where(p => p.EmpresaId == produtoView.EmpresaId && p.Nome == produtoView.Nome && p.PrecoCompra == produtoView.PrecoCompra && p.PrecoVenda == produtoView.PrecoVenda && p.Codigo == produtoView.Codigo && p.FornecedorId == produtoView.FornecedorId && p.Id != produtoView.Id);
                    if (produtoView.Id == produtoBanco.Id && produtoView.Nome == produtoBanco.Nome && produtoView.Quantidade == produtoBanco.Quantidade && produtoView.PrecoCompra == produtoBanco.PrecoCompra && produtoBanco.PrecoVenda == produtoView.PrecoVenda && produtoView.Codigo == produtoBanco.Codigo && produtoView.EmpresaId == produtoBanco.EmpresaId && produtoView.FornecedorId == produtoBanco.FornecedorId)
                    {
                        mensagem = MensagemDeSucesso.AtualizarOK;
                        produtoAtualizaQuantidade = null;
                        return true;
                    }
                    else
                    {
                        if (produtoRetornoUpdate == null)
                        {
                            mensagem = MensagemDeSucesso.AtualizarOK;
                            produtoAtualizaQuantidade = null;
                            return true;
                        }
                        else
                        {
                            var produtoReturnResposta = produtoRetornoUpdate.FirstOrDefault(p => p.Nome == produtoView.Nome && p.PrecoCompra == produtoView.PrecoCompra && p.PrecoVenda == produtoView.PrecoVenda && p.Codigo == produtoView.Codigo && p.EmpresaId == produtoView.EmpresaId && p.FornecedorId == produtoView.FornecedorId);
                            if (produtoReturnResposta != null)
                            {
                                mensagem = MensagemDeSucesso.AtualizarQuantidadeProduto;
                                produtoAtualizaQuantidade = new Produto()
                                {
                                    Id = produtoReturnResposta.Id,
                                    Nome = produtoReturnResposta.Nome,
                                    Quantidade = produtoReturnResposta.Quantidade,
                                    Ativo = produtoReturnResposta.Ativo,
                                    PrecoCompra = produtoReturnResposta.PrecoCompra,
                                    PrecoVenda = produtoReturnResposta.PrecoVenda,
                                    Codigo = produtoReturnResposta.Codigo,
                                    DataCadastroProduto = produtoReturnResposta.DataCadastroProduto,
                                    EmpresaId = produtoReturnResposta.EmpresaId,
                                    FornecedorId = produtoReturnResposta.FornecedorId
                                };

                                return true;
                            }
                            else
                            {
                                mensagem = MensagemDeSucesso.AtualizarOK;
                                produtoAtualizaQuantidade = null;
                                return true;
                            }
                        }
                    }
                }
                mensagem = MensagemDeErro.ProdutoErroAtualizar;
                produtoAtualizaQuantidade = null;
                return true;
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaProdutoInexistente;
                produtoAtualizaQuantidade = null;
                return true;
            }
        }

        public bool ExisteEnderecoProduto(int empresaId, int enderecoId, string nomeEndereco, bool isUpdate, out string mensagem)
        {
            if (ExisteEmpresaCadastrada(empresaId))
            {
                if (isUpdate)
                {
                    var nomeEnderecoProdutoUpdate = _context.EnderecoProduto.AsNoTracking().FirstOrDefault(ep => ep.NomeEndereco == nomeEndereco && ep.EmpresaId == empresaId && ep.Id != enderecoId);

                    if (nomeEnderecoProdutoUpdate == null)
                    {
                        mensagem = MensagemDeSucesso.AtualizarOK;
                        return false;
                    }
                    else if (nomeEnderecoProdutoUpdate.NomeEndereco == nomeEndereco)
                    {
                        mensagem = MensagemDeErro.EnderecoProdutoSendoUsado;
                        return true;
                    }
                }
                else
                {
                    var nomeEnderecoProduto = _context.EnderecoProduto.AsNoTracking().FirstOrDefault(ep => ep.NomeEndereco == nomeEndereco && ep.EmpresaId == empresaId);
                    if (nomeEnderecoProduto == null)
                    {
                        mensagem = MensagemDeSucesso.AtualizarOK;
                        return false;
                    }
                    else if (nomeEnderecoProduto.NomeEndereco == nomeEndereco)
                    {
                        mensagem = MensagemDeErro.EnderecoProdutoSendoUsado;
                        return true;
                    }
                }
                mensagem = MensagemDeErro.ErroAoAtualizarCriar;
                return false;
            }
            else
            {
                mensagem = MensagemDeErro.EmpresaProdutoInexistente;
                return false;
            }

        }

        public bool VerificaAdministrador(int empresaId, out string mensagem)
        {
            if (empresaId.Equals(((int)TipoEmpresa.Administrador)))
            {
                mensagem = MensagemDeSucesso.VisualizarOK;
                return true;
            }
            else
            {
                mensagem = MensagemDeErro.VisualizarEmpresa;
                return false;
            }
        }

        public bool ExisteEmpresaCadastrada(int empresaId)
        {
            var EmpresaExiste = _context.Empresa.AsNoTracking().FirstOrDefault(e => e.Id == empresaId);
            if (EmpresaExiste == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string AcertarNome(string nome)
        {
            var nomeSemEspacos = Regex.Replace(nome, " {2,}", " ");
            nomeSemEspacos = nomeSemEspacos.Trim();
            return nomeSemEspacos;
        }

        public UsuarioViewModel Login(string email, string senha, out string mensagem)
        {
            var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Email == email && u.Senha == senha);
            var empresa = _context.Empresa.AsNoTracking().FirstOrDefault(e => e.Id == usuario.EmpresaId && e.Ativo == Convert.ToBoolean(Situacao.Ativo) && e.StatusExclusao != Convert.ToBoolean(Situacao.Excluido));
            if (empresa == null)
            {
                mensagem = MensagemDeErro.EmpresaDesativadaOuInativada;
                return null;
            }
            else if (usuario == null)
            {
                mensagem = MensagemDeErro.LoginErro;
                return null;
            }
            else
            {
                var usuarioDados = (from users in _context.Usuario
                                    join func in _context.Funcionario on users.FuncionarioId equals func.Id
                                    join empre in _context.Empresa on users.EmpresaId equals empre.Id
                                    select new
                                    {
                                        Nome = func.Nome,
                                        NomeEmpresa = empre.Nome,
                                        Email = users.Email,
                                        DataCadastroUsuario = users.DataCadastroUsuario,
                                        Ativo = users.Ativo,
                                        EmpresaId = users.EmpresaId,
                                        Senha = users.Senha,
                                        FuncionarioId = users.FuncionarioId,
                                        UsuarioId = users.Id
                                    }).Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();


                var usuarioRetorno = new UsuarioViewModel()
                {
                    Id = usuarioDados.UsuarioId,
                    NomeEmpresa = usuarioDados.NomeEmpresa,
                    Nome = usuarioDados.Nome,
                    Email = usuarioDados.Email,
                    Senha = usuarioDados.Senha,
                    DataCadastroUsuario = usuarioDados.DataCadastroUsuario,
                    Ativo = usuarioDados.Ativo,
                    FuncionarioId = usuarioDados.FuncionarioId,
                    EmpresaId = usuarioDados.EmpresaId
                };

                mensagem = MensagemDeSucesso.SucessoSenha;
                return usuarioRetorno;
            }
        }

        public IEnumerable<UsuarioViewModel> RetornarUsuario(int empresaId)
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
                            }).Where(u => u.EmpresaId == empresaId).ToList();

            foreach (var item in usuarios)
            {
                usuariosRetorno.Add(new UsuarioViewModel(item.Id, item.NomeEmpresa, item.Nome, item.Email, item.Senha, item.DataCadastroUsuario, item.Ativo, item.FuncionarioId, item.EmpresaId));
            }

            return usuariosRetorno;
        }

        public UsuarioViewModel RetornarFuncionarioUsuarioId(int funcionarioId)
        {
            var usuarioRetorno = new UsuarioViewModel();
            var usuario = (from users in _context.Usuario
                           join func in _context.Funcionario on users.FuncionarioId equals func.Id
                           join empre in _context.Empresa on users.EmpresaId equals empre.Id
                           select new
                           {
                               Id = users.Id,
                               Nome = func.Nome,
                               NomeEmpresa = empre.Nome,
                               Email = users.Email,
                               Senha = users.Senha,
                               DataCadastroUsuario = users.DataCadastroUsuario,
                               Ativo = users.Ativo,
                               EmpresaId = users.EmpresaId,
                               FuncionarioId = users.FuncionarioId
                           }).Where(u => u.FuncionarioId == funcionarioId).ToList();

            foreach (var item in usuario)
            {
                usuarioRetorno.Id = item.Id;
                usuarioRetorno.Nome = item.Nome;
                usuarioRetorno.NomeEmpresa = item.NomeEmpresa;
                usuarioRetorno.Email = item.Email;
                usuarioRetorno.Senha = item.Senha;
                usuarioRetorno.DataCadastroUsuario = item.DataCadastroUsuario;
                usuarioRetorno.Ativo = item.Ativo;
                usuarioRetorno.FuncionarioId = item.FuncionarioId;
                usuarioRetorno.EmpresaId = item.EmpresaId;
            }

            return usuarioRetorno;
        }

        public UsuarioViewModel RetornarUsuarioId(int usuarioId)
        {
            var usuarioRetorno = new UsuarioViewModel();
            var usuario = (from users in _context.Usuario
                           join func in _context.Funcionario on users.FuncionarioId equals func.Id
                           join empre in _context.Empresa on users.EmpresaId equals empre.Id
                           select new
                           {
                               Id = users.Id,
                               Nome = func.Nome,
                               NomeEmpresa = empre.Nome,
                               Email = users.Email,
                               Senha = users.Senha,
                               DataCadastroUsuario = users.DataCadastroUsuario,
                               Ativo = users.Ativo,
                               EmpresaId = users.EmpresaId,
                               FuncionarioId = users.FuncionarioId
                           }).Where(u => u.Id == usuarioId).ToList();

            foreach (var item in usuario)
            {
                usuarioRetorno.Id = item.Id;
                usuarioRetorno.Nome = item.Nome;
                usuarioRetorno.NomeEmpresa = item.NomeEmpresa;
                usuarioRetorno.Email = item.Email;
                usuarioRetorno.Senha = item.Senha;
                usuarioRetorno.DataCadastroUsuario = item.DataCadastroUsuario;
                usuarioRetorno.Ativo = item.Ativo;
                usuarioRetorno.FuncionarioId = item.FuncionarioId;
                usuarioRetorno.EmpresaId = item.EmpresaId;
            }

            return usuarioRetorno;
        }

        public IEnumerable<FuncionariosUsuariosViewModel> RetornarFuncionariosUsuario(int empresaId)
        {
            List<FuncionariosUsuariosViewModel> funcionariosUsuariosRetorno = new List<FuncionariosUsuariosViewModel>();
            var usuarios = _context.Usuario.AsNoTracking().Where(u => u.EmpresaId == empresaId).ToList();
            var funcionarios = _context.Funcionario.AsNoTracking().Where(f => f.EmpresaId == empresaId).ToList();
            var funcionariosUsuarios = from func in funcionarios
                                       from user in usuarios
                                       where func.Id == user.FuncionarioId
                                       select func;
            var listaFinal = funcionarios.Except(funcionariosUsuarios);

            foreach (var item in listaFinal)
            {
                funcionariosUsuariosRetorno.Add(new FuncionariosUsuariosViewModel(item.Id, item.Nome, item.Endereco, item.Bairro, item.Numero, item.Municipio, item.UF, item.Pais, item.CEP, item.Complemento, item.Telefone, item.Email, item.CPF, item.DataCadastroFuncionario, item.Ativo, item.EmpresaId));
            }
            return funcionariosUsuariosRetorno;
        }

        public IEnumerable<EstoqueViewModelEnderecoProduto> RetornarProdutosEstoque(int empresaId)
        {
            List<EstoqueViewModelEnderecoProduto> EstoqueProdutoRetorno = new List<EstoqueViewModelEnderecoProduto>();
            var estoques = _context.Estoque.AsNoTracking().Where(e => e.EmpresaId == empresaId && e.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).ToList();
            var produtos = _context.Produto.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido)).ToList();
            var fornecedor = _context.Fornecedor.AsNoTracking().Where(f => f.EmpresaId == empresaId).ToList();
            var empresa = _context.Empresa.AsNoTracking().Where(em => em.Id == empresaId).ToList();
            var enderecoProduto = _context.EnderecoProduto.AsNoTracking().Where(ep => ep.EmpresaId == empresaId).ToList();
            if (estoques != null && produtos != null)
            {
                var produtosEstoques = from prod in produtos
                                       from esto in estoques
                                       from forn in fornecedor
                                       from emp in empresa
                                       from endPro in enderecoProduto
                                       where esto.ProdutoId == prod.Id && esto.FornecedorId == forn.Id && esto.EmpresaId == empresaId
                                       select new
                                       {
                                           Id = esto.Id,
                                           EmpresaId = emp.Id,
                                           EmpresaNome = emp.Nome,
                                           FornecedorId = forn.Id,
                                           FornecedorNome = forn.Nome,
                                           ProdutoId = prod.Id,
                                           ProdutoNome = prod.Nome,
                                           Quantidade = esto.Quantidade,
                                           DataCadastroEstoque = esto.DataCadastroEstoque,
                                           DataCadastroEnderecoProduto = endPro.DataCadastroEnderecoProduto
                                        };
                if (produtosEstoques.Count() != 0)
                {
                    foreach (var produtosComFornecedor in produtosEstoques)
                    {
                        foreach (var enderecosProdutos in enderecoProduto)
                        {
                            var existeEstoqueId = produtosEstoques.Where(pe => pe.Id == enderecosProdutos.EstoqueId);
                            var existeNaLista = EstoqueProdutoRetorno.FirstOrDefault(ep => ep.Id == enderecosProdutos.EstoqueId);
                            if (existeEstoqueId != null && existeNaLista == null)
                            {
                                foreach (var item in existeEstoqueId)
                                {
                                    EstoqueProdutoRetorno.Add(new EstoqueViewModelEnderecoProduto(item.Id, item.EmpresaId, item.EmpresaNome, item.FornecedorId, item.FornecedorNome, item.ProdutoId, item.ProdutoNome, item.Quantidade, enderecosProdutos.Id, enderecosProdutos.NomeEndereco, enderecosProdutos.Ativo, (DateTime)item.DataCadastroEstoque, (DateTime)item.DataCadastroEnderecoProduto));
                                }
                            }
                        }
                        var naoExisteEnderecoProdutoId = EstoqueProdutoRetorno.Where(pe => pe.Id == produtosComFornecedor.Id).FirstOrDefault();
                        if (naoExisteEnderecoProdutoId == null)
                        {
                            EstoqueProdutoRetorno.Add(new EstoqueViewModelEnderecoProduto(produtosComFornecedor.Id, produtosComFornecedor.EmpresaId, produtosComFornecedor.EmpresaNome, produtosComFornecedor.FornecedorId, produtosComFornecedor.FornecedorNome, produtosComFornecedor.ProdutoId, produtosComFornecedor.ProdutoNome, produtosComFornecedor.Quantidade, (int)ExisteEnderecoProdutoEnum.NaoExisteEndereco, MensagemDeAlerta.ProdutoSemEndereco, false, (DateTime)produtosComFornecedor.DataCadastroEstoque, null));
                        }
                    }
                }
                else
                {
                    var produtosLista = from prod in produtos
                                           from esto in estoques
                                           from forn in fornecedor
                                           from emp in empresa
                                           where esto.ProdutoId == prod.Id && esto.FornecedorId == forn.Id && esto.EmpresaId == empresaId
                                           select new
                                           {
                                               Id = esto.Id,
                                               EmpresaId = emp.Id,
                                               EmpresaNome = emp.Nome,
                                               FornecedorId = forn.Id,
                                               FornecedorNome = forn.Nome,
                                               ProdutoId = prod.Id,
                                               ProdutoNome = prod.Nome,
                                               Quantidade = esto.Quantidade,
                                               DataCadastroEstoque = esto.DataCadastroEstoque
                                           };
                    foreach (var produtoLista in produtosLista)
                    {
                        EstoqueProdutoRetorno.Add(new EstoqueViewModelEnderecoProduto(produtoLista.Id, produtoLista.EmpresaId, produtoLista.EmpresaNome, produtoLista.FornecedorId, produtoLista.FornecedorNome, produtoLista.ProdutoId, produtoLista.ProdutoNome, produtoLista.Quantidade, (int)ExisteEnderecoProdutoEnum.NaoExisteEndereco, MensagemDeAlerta.ProdutoSemEndereco, false, (DateTime)produtoLista.DataCadastroEstoque, null));
                    }
                }
                return EstoqueProdutoRetorno.OrderBy(p => p.ProdutoNome);
            }
            else
            {
                return null;
            }
        }

        public EstoqueProdutoViewModelUpdate RetornarProdutosEstoqueId(int empresaId, int estoqueId)
        {
            var produtoEstoqueSemFornecedor = new EstoqueProdutoViewModelUpdate();
            var estoque = _context.Estoque.AsNoTracking().FirstOrDefault(e => e.EmpresaId == empresaId && e.Id == estoqueId);
            var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == estoque.ProdutoId);
            var fornecedor = _context.Fornecedor.AsNoTracking().FirstOrDefault(f => f.EmpresaId == empresaId && f.Id == estoque.FornecedorId);
            var empresa = _context.Empresa.AsNoTracking().FirstOrDefault(em => em.Id == empresaId);
            if (fornecedor == null)
            {
                produtoEstoqueSemFornecedor = new EstoqueProdutoViewModelUpdate(estoque.Id, estoque.EmpresaId, empresa.Nome, (int)ZerarIdFornecedor.FornecedorId, MensagemDeAlerta.ProdutoSemFornecedor, produto.Id, produto.Nome, estoque.Quantidade, (DateTime)estoque.DataCadastroEstoque);

            }
            else
            {
                produtoEstoqueSemFornecedor = new EstoqueProdutoViewModelUpdate(estoque.Id, estoque.EmpresaId, empresa.Nome, fornecedor.Id, fornecedor.Nome, produto.Id, produto.Nome, estoque.Quantidade, (DateTime)estoque.DataCadastroEstoque);
            }
            return produtoEstoqueSemFornecedor;
        }

        public bool VerificaQuantidade(int empresaId, int produtoId, int quantidadeVenda, out string mensagem)
        {
            var produto = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == produtoId && p.Quantidade != 0);
            var estoque = _context.Estoque.AsNoTracking().FirstOrDefault(e => e.EmpresaId == empresaId && e.ProdutoId == produtoId && e.Quantidade != 0);
            if (produto != null && estoque != null)
            {
                if (quantidadeVenda > produto.Quantidade)
                {
                    mensagem = $"{MensagemDeErro.ProdutoErroQuantidadeVenda}. Nome do produto: {produto.Nome}";
                    return false;
                }
                else
                {
                    mensagem = MensagemDeSucesso.ProdutoQuantidadeVenda;
                    return true;
                }
            }
            mensagem = MensagemDeErro.ProdutoEstoqueZerados;
            return false;
        }

        public IEnumerable<PedidoViewModel> RetornarPedidosView(IEnumerable<PedidoViewModel> pedidos)
        {
            List<PedidoViewModel> pedidosRetorno = new List<PedidoViewModel>();
            foreach (var item in pedidos)
            {
                var pedido = _context.Pedido.AsNoTracking().FirstOrDefault(p => p.EmpresaId == item.EmpresaId && p.Id == item.Id);
                var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == item.EmpresaId && c.Id == item.ClienteId);
                var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.Id == pedido.TransportadorId);
                var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Id == pedido.UsuarioId);
                if (pedido != null && cliente != null)
                {
                    var statusPedido = string.Empty;
                    if (pedido.Status == (int)StatusPedido.PedidoEmAnalise)
                    {
                        statusPedido = MensagemDeAlerta.PedidoEmAnalise;
                        pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido));
                    }
                    else if (pedido.Status == (int)StatusPedido.PedidoConfirmado)
                    {
                        statusPedido = MensagemDeAlerta.PedidoConfirmado;
                        pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido));
                    }
                    else if (pedido.Status == (int)StatusPedido.PedidoCancelado)
                    {
                        statusPedido = MensagemDeAlerta.PedidoCancelado;
                        pedidosRetorno.Add(new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido));
                    }
                }
                else
                {
                    pedidosRetorno = null;
                }
            }
            return pedidosRetorno;
        }

        public Produto AtualizarQuantidadeProdutoPosPedido(int pedidoId, int empresaId, int produtoId, int quantidadeVenda, out Estoque estoque, out string mensagem)
        {
            var produtoBD = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == empresaId && p.Id == produtoId);
            var estoqueBD = _context.Estoque.AsNoTracking().FirstOrDefault(e => e.EmpresaId == empresaId && e.ProdutoId == produtoId);
            Produto produtoRetorno = null;
            Estoque estoqueRetorno = null;
            if (produtoBD != null && estoqueBD != null)
            {
                if (quantidadeVenda < produtoBD.Quantidade && quantidadeVenda < estoqueBD.Quantidade)
                {
                    produtoBD.Quantidade -= quantidadeVenda;
                    estoqueBD.Quantidade -= quantidadeVenda;
                    produtoRetorno = produtoBD;
                    estoqueRetorno = estoqueBD;
                }
                else if (quantidadeVenda == produtoBD.Quantidade && quantidadeVenda == estoqueBD.Quantidade)
                {
                    produtoRetorno = produtoBD;
                    estoqueRetorno = estoqueBD;
                }
            }
            else
            {
                produtoRetorno = null;
                estoqueRetorno = null;
            }

            if (produtoRetorno != null && estoqueRetorno != null)
            {
                estoque = estoqueRetorno;
                mensagem = MensagemDeSucesso.ProdutoSeraAtualizado;
                return produtoRetorno;
            }
            else
            {
                estoque = estoqueRetorno;
                mensagem = MensagemDeErro.ProdutoErroQauntidadeAtualizar;
                return produtoRetorno;
            }
        }

        public bool AtualizarQuantidadeProdutoEstoquePosDeletePedido(Pedido pedido, out List<Produto> produtos, out List<Estoque> estoques, out List<PedidoNota> pedidosNotas)
        {
            List<Produto> produto = new List<Produto>();
            List<Estoque> estoque = new List<Estoque>();
            var pedidoNotaBD = _context.PedidoNota.AsNoTracking().Where(pn => pn.EmpresaId == pedido.EmpresaId && pn.PedidoId == pedido.Id && pn.Status == (int)StatusPedido.PedidoConfirmado).ToList();
            foreach (var item in pedidoNotaBD)
            {
                var produtoBD = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido));
                var estoqueBD = _context.Estoque.AsNoTracking().FirstOrDefault(e => e.EmpresaId == pedido.EmpresaId && e.ProdutoId == item.ProdutoId);
                if (produtoBD != null && estoqueBD != null)
                {
                    produtoBD.Quantidade += item.Quantidade;
                    estoqueBD.Quantidade += item.Quantidade;
                    produto.Add(produtoBD);
                    estoque.Add(estoqueBD);
                }
                else
                {
                    produto.Add(null);
                    estoque.Add(null);
                }
            }

            if (produto != null && estoque != null)
            {
                produtos = produto;
                estoques = estoque;
                pedidosNotas = pedidoNotaBD;
                return true;
            }
            else
            {
                produtos = null;
                estoques = null;
                pedidosNotas = null;
                return false;
            }
        }

        public PedidoViewModel MontarPedidoRetorno(Pedido pedido)
        {
            var pedidoRetorno = new PedidoViewModel();
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            var pedidoNota = _context.PedidoNota.AsNoTracking().Where(pn => pn.EmpresaId == pedido.EmpresaId && pn.PedidoId == pedido.Id).ToList();
            var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.Id == pedido.ClienteId);
            var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.Id == pedido.TransportadorId);
            var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(u => u.Id == pedido.UsuarioId);
            foreach (var item in pedidoNota)
            {
                var produtoExcluido = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido));
                if (produtoExcluido != null)
                {
                    var produtoView = new ProdutoViewModel()
                    {
                        Id = item.ProdutoId,
                        Nome = produtoExcluido.Nome,
                        Quantidade = produtoExcluido.Quantidade,
                        QuantidadeVenda = item.Quantidade,
                        Ativo = produtoExcluido.Ativo,
                        PrecoCompra = produtoExcluido.PrecoCompra,
                        PrecoVenda = item.PrecoVenda,
                        PrecoVendaTotal = item.PrecoTotal,
                        Codigo = item.CodigoProduto,
                        DataCadastroProduto = produtoExcluido.DataCadastroProduto,
                        EmpresaId = produtoExcluido.EmpresaId,
                        FornecedorId = produtoExcluido.FornecedorId,
                        StatusExclusao = produtoExcluido.StatusExclusao
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
                else
                {
                    var produtoExistente = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoExcluido));
                    if (produtoExistente != null)
                    {
                        var produtoView = new ProdutoViewModel()
                        {
                            Id = item.ProdutoId,
                            Nome = produtoExistente.Nome,
                            Quantidade = produtoExistente.Quantidade,
                            QuantidadeVenda = item.Quantidade,
                            Ativo = produtoExistente.Ativo,
                            PrecoCompra = produtoExistente.PrecoCompra,
                            PrecoVenda = item.PrecoVenda,
                            PrecoVendaTotal = item.PrecoTotal,
                            Codigo = item.CodigoProduto,
                            DataCadastroProduto = produtoExistente.DataCadastroProduto,
                            EmpresaId = produtoExistente.EmpresaId,
                            FornecedorId = produtoExistente.FornecedorId,
                            StatusExclusao = produtoExistente.StatusExclusao
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
            }

            if (cliente != null && transportador != null && usuario != null && produtos != null)
            {
                var statusPedido = string.Empty;
                switch (pedido.Status)
                {
                    case 1:
                        statusPedido = MensagemDeAlerta.PedidoEmAnalise;
                        pedidoRetorno = new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, produtos);
                        break;
                    case 2:
                        statusPedido = MensagemDeAlerta.PedidoConfirmado;
                        pedidoRetorno = new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, produtos);
                        break;
                    case 3:
                        statusPedido = MensagemDeAlerta.PedidoCancelado;
                        pedidoRetorno = new PedidoViewModel(pedido.Id, cliente.Id, cliente.Nome, transportador.Id, transportador.Nome, pedido.EmpresaId, usuario.Id, pedido.PrecoTotal, pedido.DataCadastroPedido, statusPedido, produtos);
                        break;
                }
            }
            return pedidoRetorno;
        }

        public IEnumerable<NotaFiscalViewModel> RetornarNotasFiscais(IEnumerable<NotaFiscal> notaFiscal)
        {
            List<NotaFiscalViewModel> notaFiscalRetorno = new List<NotaFiscalViewModel>();
            if (notaFiscal != null)
            {
                foreach (var item in notaFiscal)
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

        public NotaFiscalIdViewModel RetornarNotaFiscal(NotaFiscal notaFiscal)
        {
            var notaFiscalRetorno = new NotaFiscalIdViewModel();
            notaFiscalRetorno.Cliente = new Cliente();
            notaFiscalRetorno.Transportador = new Transportador();
            notaFiscalRetorno.Empresa = new Empresa();
            notaFiscalRetorno.Produto = new List<ProdutoViewModel>();
            if (notaFiscal != null)
            {
                var pedido = _context.Pedido.AsNoTracking().FirstOrDefault(p => p.EmpresaId == notaFiscal.EmpresaId && p.Id == notaFiscal.PedidoId);
                var empresa = _context.Empresa.AsNoTracking().FirstOrDefault(e => e.Id == notaFiscal.EmpresaId);
                var cliente = _context.Cliente.AsNoTracking().FirstOrDefault(c => c.EmpresaId == notaFiscal.EmpresaId && c.Id == notaFiscal.ClienteId);
                var transportador = _context.Transportador.AsNoTracking().FirstOrDefault(t => t.EmpresaId == notaFiscal.EmpresaId && t.Id == notaFiscal.TransportadorId);
                var pedidoNota = _context.PedidoNota.AsNoTracking().Where(pn => pn.EmpresaId == notaFiscal.EmpresaId && pn.PedidoId == notaFiscal.PedidoId);

                if (pedido != null && cliente != null && empresa != null && transportador != null)
                {
                    if (notaFiscal.Status == (int)StatusNotaFiscal.NotaFiscalAprovada)
                    {
                        var status = MensagemDeAlerta.NotaFiscalAprovada;
                        notaFiscalRetorno.Id = notaFiscal.Id;
                        notaFiscalRetorno.PedidoId = pedido.Id;
                        notaFiscalRetorno.PrecoTotal = notaFiscal.PrecoTotal;
                        notaFiscalRetorno.QuantidadeItens = notaFiscal.QuantidadeItens;
                        notaFiscalRetorno.DataCadastroNotaFiscal = notaFiscal.DataCadastroNotaFiscal;
                        notaFiscalRetorno.ChaveAcesso = notaFiscal.ChaveAcesso;
                        notaFiscalRetorno.StatusNota = status;
                        notaFiscalRetorno.Cliente.Id = cliente.Id;
                        notaFiscalRetorno.Cliente.Nome = cliente.Nome;
                        notaFiscalRetorno.Cliente.Endereco = cliente.Endereco;
                        notaFiscalRetorno.Cliente.Bairro = cliente.Bairro;
                        notaFiscalRetorno.Cliente.Numero = cliente.Numero;
                        notaFiscalRetorno.Cliente.Municipio = cliente.Municipio;
                        notaFiscalRetorno.Cliente.UF = cliente.UF;
                        notaFiscalRetorno.Cliente.Pais = cliente.Pais;
                        notaFiscalRetorno.Cliente.CEP = cliente.CEP;
                        notaFiscalRetorno.Cliente.Complemento = cliente.Complemento;
                        notaFiscalRetorno.Cliente.Telefone = cliente.Telefone;
                        notaFiscalRetorno.Cliente.Email = cliente.Email;
                        notaFiscalRetorno.Cliente.CPFCNPJ = cliente.CPFCNPJ;
                        notaFiscalRetorno.Cliente.InscricaoMunicipal = cliente.InscricaoMunicipal;
                        notaFiscalRetorno.Cliente.InscricaoEstadual = cliente.InscricaoEstadual;
                        notaFiscalRetorno.Cliente.DataCadastroCliente = cliente.DataCadastroCliente;
                        notaFiscalRetorno.Cliente.Ativo = cliente.Ativo;
                        notaFiscalRetorno.Cliente.EmpresaId = cliente.EmpresaId;
                        notaFiscalRetorno.Transportador.Id = transportador.Id;
                        notaFiscalRetorno.Transportador.Nome = transportador.Nome;
                        notaFiscalRetorno.Transportador.Endereco = transportador.Endereco;
                        notaFiscalRetorno.Transportador.Bairro = transportador.Bairro;
                        notaFiscalRetorno.Transportador.Numero = transportador.Numero;
                        notaFiscalRetorno.Transportador.Municipio = transportador.Municipio;
                        notaFiscalRetorno.Transportador.UF = transportador.UF;
                        notaFiscalRetorno.Transportador.Pais = transportador.Pais;
                        notaFiscalRetorno.Transportador.CEP = transportador.CEP;
                        notaFiscalRetorno.Transportador.Complemento = transportador.Complemento;
                        notaFiscalRetorno.Transportador.Telefone = transportador.Telefone;
                        notaFiscalRetorno.Transportador.Email = transportador.Email;
                        notaFiscalRetorno.Transportador.CNPJ = transportador.CNPJ;
                        notaFiscalRetorno.Transportador.InscricaoMunicipal = transportador.InscricaoMunicipal;
                        notaFiscalRetorno.Transportador.InscricaoEstadual = transportador.InscricaoEstadual;
                        notaFiscalRetorno.Transportador.DataCadastroTransportador = transportador.DataCadastroTransportador;
                        notaFiscalRetorno.Transportador.Ativo = transportador.Ativo;
                        notaFiscalRetorno.Transportador.EmpresaId = transportador.EmpresaId;
                        notaFiscalRetorno.Empresa.Id = empresa.Id;
                        notaFiscalRetorno.Empresa.Nome = empresa.Nome;
                        notaFiscalRetorno.Empresa.Endereco = empresa.Endereco;
                        notaFiscalRetorno.Empresa.Bairro = empresa.Bairro;
                        notaFiscalRetorno.Empresa.Numero = empresa.Numero;
                        notaFiscalRetorno.Empresa.Municipio = empresa.Municipio;
                        notaFiscalRetorno.Empresa.UF = empresa.UF;
                        notaFiscalRetorno.Empresa.Pais = empresa.Pais;
                        notaFiscalRetorno.Empresa.CEP = empresa.CEP;
                        notaFiscalRetorno.Empresa.Complemento = empresa.Complemento;
                        notaFiscalRetorno.Empresa.Telefone = empresa.Telefone;
                        notaFiscalRetorno.Empresa.Email = empresa.Email;
                        notaFiscalRetorno.Empresa.CNPJ = empresa.CNPJ;
                        notaFiscalRetorno.Empresa.InscricaoMunicipal = empresa.InscricaoMunicipal;
                        notaFiscalRetorno.Empresa.InscricaoEstadual = empresa.InscricaoEstadual;
                        notaFiscalRetorno.Empresa.DataCadastroEmpresa = empresa.DataCadastroEmpresa;
                        notaFiscalRetorno.Empresa.Ativo = empresa.Ativo;

                        foreach (var item in pedidoNota)
                        {
                            var produtoExcluido = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido));
                            if (produtoExcluido != null)
                            {
                                var produtoView = new ProdutoViewModel()
                                {
                                    Id = item.ProdutoId,
                                    Nome = produtoExcluido.Nome,
                                    Quantidade = produtoExcluido.Quantidade,
                                    QuantidadeVenda = item.Quantidade,
                                    Ativo = produtoExcluido.Ativo,
                                    PrecoCompra = produtoExcluido.PrecoCompra,
                                    PrecoVenda = item.PrecoVenda,
                                    PrecoVendaTotal = item.PrecoTotal,
                                    Codigo = item.CodigoProduto,
                                    DataCadastroProduto = produtoExcluido.DataCadastroProduto,
                                    EmpresaId = produtoExcluido.EmpresaId,
                                    FornecedorId = produtoExcluido.FornecedorId,
                                    StatusExclusao = produtoExcluido.StatusExclusao
                                };

                                if (produtoView != null)
                                {
                                    notaFiscalRetorno.Produto.Add(produtoView);
                                }
                                else
                                {
                                    notaFiscalRetorno.Produto.Add(null);
                                }
                            }
                            else
                            {
                                var produtoExistente = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoExcluido));
                                if (produtoExistente != null)
                                {
                                    var produtoView = new ProdutoViewModel()
                                    {
                                        Id = item.ProdutoId,
                                        Nome = produtoExistente.Nome,
                                        Quantidade = produtoExistente.Quantidade,
                                        QuantidadeVenda = item.Quantidade,
                                        Ativo = produtoExistente.Ativo,
                                        PrecoCompra = produtoExistente.PrecoCompra,
                                        PrecoVenda = item.PrecoVenda,
                                        PrecoVendaTotal = item.PrecoTotal,
                                        Codigo = item.CodigoProduto,
                                        DataCadastroProduto = produtoExistente.DataCadastroProduto,
                                        EmpresaId = produtoExistente.EmpresaId,
                                        FornecedorId = produtoExistente.FornecedorId,
                                        StatusExclusao = produtoExistente.StatusExclusao
                                    };

                                    if (produtoView != null)
                                    {
                                        notaFiscalRetorno.Produto.Add(produtoView);
                                    }
                                    else
                                    {
                                        notaFiscalRetorno.Produto.Add(null);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var status = MensagemDeAlerta.NotaFiscalCancelada;
                        notaFiscalRetorno.Id = notaFiscal.Id;
                        notaFiscalRetorno.PedidoId = pedido.Id;
                        notaFiscalRetorno.PrecoTotal = notaFiscal.PrecoTotal;
                        notaFiscalRetorno.DataCadastroNotaFiscal = notaFiscal.DataCadastroNotaFiscal;
                        notaFiscalRetorno.ChaveAcesso = notaFiscal.ChaveAcesso;
                        notaFiscalRetorno.QuantidadeItens = notaFiscal.QuantidadeItens;
                        notaFiscalRetorno.StatusNota = status;
                        notaFiscalRetorno.Cliente.Id = cliente.Id;
                        notaFiscalRetorno.Cliente.Nome = cliente.Nome;
                        notaFiscalRetorno.Cliente.Endereco = cliente.Endereco;
                        notaFiscalRetorno.Cliente.Bairro = cliente.Bairro;
                        notaFiscalRetorno.Cliente.Numero = cliente.Numero;
                        notaFiscalRetorno.Cliente.Municipio = cliente.Municipio;
                        notaFiscalRetorno.Cliente.UF = cliente.UF;
                        notaFiscalRetorno.Cliente.Pais = cliente.Pais;
                        notaFiscalRetorno.Cliente.CEP = cliente.CEP;
                        notaFiscalRetorno.Cliente.Complemento = cliente.Complemento;
                        notaFiscalRetorno.Cliente.Telefone = cliente.Telefone;
                        notaFiscalRetorno.Cliente.Email = cliente.Email;
                        notaFiscalRetorno.Cliente.CPFCNPJ = cliente.CPFCNPJ;
                        notaFiscalRetorno.Cliente.InscricaoMunicipal = cliente.InscricaoMunicipal;
                        notaFiscalRetorno.Cliente.InscricaoEstadual = cliente.InscricaoEstadual;
                        notaFiscalRetorno.Cliente.DataCadastroCliente = cliente.DataCadastroCliente;
                        notaFiscalRetorno.Cliente.Ativo = cliente.Ativo;
                        notaFiscalRetorno.Cliente.EmpresaId = cliente.EmpresaId;
                        notaFiscalRetorno.Transportador.Id = transportador.Id;
                        notaFiscalRetorno.Transportador.Nome = transportador.Nome;
                        notaFiscalRetorno.Transportador.Endereco = transportador.Endereco;
                        notaFiscalRetorno.Transportador.Bairro = transportador.Bairro;
                        notaFiscalRetorno.Transportador.Numero = transportador.Numero;
                        notaFiscalRetorno.Transportador.Municipio = transportador.Municipio;
                        notaFiscalRetorno.Transportador.UF = transportador.UF;
                        notaFiscalRetorno.Transportador.Pais = transportador.Pais;
                        notaFiscalRetorno.Transportador.CEP = transportador.CEP;
                        notaFiscalRetorno.Transportador.Complemento = transportador.Complemento;
                        notaFiscalRetorno.Transportador.Telefone = transportador.Telefone;
                        notaFiscalRetorno.Transportador.Email = transportador.Email;
                        notaFiscalRetorno.Transportador.CNPJ = transportador.CNPJ;
                        notaFiscalRetorno.Transportador.InscricaoMunicipal = transportador.InscricaoMunicipal;
                        notaFiscalRetorno.Transportador.InscricaoEstadual = transportador.InscricaoEstadual;
                        notaFiscalRetorno.Transportador.DataCadastroTransportador = transportador.DataCadastroTransportador;
                        notaFiscalRetorno.Transportador.Ativo = transportador.Ativo;
                        notaFiscalRetorno.Transportador.EmpresaId = transportador.EmpresaId;
                        notaFiscalRetorno.Empresa.Id = empresa.Id;
                        notaFiscalRetorno.Empresa.Nome = empresa.Nome;
                        notaFiscalRetorno.Empresa.Endereco = empresa.Endereco;
                        notaFiscalRetorno.Empresa.Bairro = empresa.Bairro;
                        notaFiscalRetorno.Empresa.Numero = empresa.Numero;
                        notaFiscalRetorno.Empresa.Municipio = empresa.Municipio;
                        notaFiscalRetorno.Empresa.UF = empresa.UF;
                        notaFiscalRetorno.Empresa.Pais = empresa.Pais;
                        notaFiscalRetorno.Empresa.CEP = empresa.CEP;
                        notaFiscalRetorno.Empresa.Complemento = empresa.Complemento;
                        notaFiscalRetorno.Empresa.Telefone = empresa.Telefone;
                        notaFiscalRetorno.Empresa.Email = empresa.Email;
                        notaFiscalRetorno.Empresa.CNPJ = empresa.CNPJ;
                        notaFiscalRetorno.Empresa.InscricaoMunicipal = empresa.InscricaoMunicipal;
                        notaFiscalRetorno.Empresa.InscricaoEstadual = empresa.InscricaoEstadual;
                        notaFiscalRetorno.Empresa.DataCadastroEmpresa = empresa.DataCadastroEmpresa;
                        notaFiscalRetorno.Empresa.Ativo = empresa.Ativo;

                        foreach (var item in pedidoNota)
                        {
                            var produtoExcluido = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoNaoExcluido));
                            if (produtoExcluido != null)
                            {
                                var produtoView = new ProdutoViewModel()
                                {
                                    Id = item.ProdutoId,
                                    Nome = produtoExcluido.Nome,
                                    Quantidade = produtoExcluido.Quantidade,
                                    QuantidadeVenda = item.Quantidade,
                                    Ativo = produtoExcluido.Ativo,
                                    PrecoCompra = produtoExcluido.PrecoCompra,
                                    PrecoVenda = item.PrecoVenda,
                                    PrecoVendaTotal = item.PrecoTotal,
                                    Codigo = item.CodigoProduto,
                                    DataCadastroProduto = produtoExcluido.DataCadastroProduto,
                                    EmpresaId = produtoExcluido.EmpresaId,
                                    FornecedorId = produtoExcluido.FornecedorId,
                                    StatusExclusao = produtoExcluido.StatusExclusao
                                };

                                if (produtoView != null)
                                {
                                    notaFiscalRetorno.Produto.Add(produtoView);
                                }
                                else
                                {
                                    notaFiscalRetorno.Produto.Add(null);
                                }
                            }
                            else
                            {
                                var produtoExistente = _context.Produto.AsNoTracking().FirstOrDefault(p => p.EmpresaId == pedido.EmpresaId && p.Id == item.ProdutoId && p.StatusExclusao == Convert.ToBoolean(StatusProduto.ProdutoExcluido));
                                if (produtoExistente != null)
                                {
                                    var produtoView = new ProdutoViewModel()
                                    {
                                        Id = item.ProdutoId,
                                        Nome = produtoExistente.Nome,
                                        Quantidade = produtoExistente.Quantidade,
                                        QuantidadeVenda = item.Quantidade,
                                        Ativo = produtoExistente.Ativo,
                                        PrecoCompra = produtoExistente.PrecoCompra,
                                        PrecoVenda = item.PrecoVenda,
                                        PrecoVendaTotal = item.PrecoTotal,
                                        Codigo = item.CodigoProduto,
                                        DataCadastroProduto = produtoExistente.DataCadastroProduto,
                                        EmpresaId = produtoExistente.EmpresaId,
                                        FornecedorId = produtoExistente.FornecedorId,
                                        StatusExclusao = produtoExistente.StatusExclusao
                                    };

                                    if (produtoView != null)
                                    {
                                        notaFiscalRetorno.Produto.Add(produtoView);
                                    }
                                    else
                                    {
                                        notaFiscalRetorno.Produto.Add(null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return notaFiscalRetorno;
        } 
        public bool VerificaPermissoes(Permissao permissaoBD, Permissao permissaoModel)
        {
            if (permissaoBD.Id == permissaoModel.Id
            && permissaoBD.VisualizarCliente == permissaoModel.VisualizarCliente
            && permissaoBD.ClienteCadastro == permissaoModel.ClienteCadastro
            && permissaoBD.ClienteEditar == permissaoModel.ClienteEditar
            && permissaoBD.ClienteDetalhe == permissaoModel.ClienteDetalhe
            && permissaoBD.ClienteExcluir == permissaoModel.ClienteExcluir
            && permissaoBD.VisualizarEmpresa == permissaoModel.VisualizarEmpresa
            && permissaoBD.EmpresaCadastro == permissaoModel.EmpresaCadastro
            && permissaoBD.EmpresaEditar == permissaoModel.EmpresaEditar
            && permissaoBD.EmpresaDetalhe == permissaoModel.EmpresaDetalhe
            && permissaoBD.EmpresaExcluir == permissaoModel.EmpresaExcluir
            && permissaoBD.VisualizarEstoque == permissaoModel.VisualizarEstoque
            && permissaoBD.EstoqueEditar == permissaoModel.EstoqueEditar
            && permissaoBD.EstoqueDetalhe == permissaoModel.EstoqueDetalhe
            && permissaoBD.EstoqueExcluir == permissaoModel.EstoqueExcluir
            && permissaoBD.VisualizarEnderecoProduto == permissaoModel.VisualizarEnderecoProduto
            && permissaoBD.EnderecoProdutoCadastro == permissaoModel.EnderecoProdutoCadastro
            && permissaoBD.EnderecoProdutoEditar == permissaoModel.EnderecoProdutoEditar
            && permissaoBD.EnderecoProdutoDetalhe == permissaoModel.EnderecoProdutoDetalhe
            && permissaoBD.EnderecoProdutoExcluir == permissaoModel.EnderecoProdutoExcluir
            && permissaoBD.VisualizarFornecedor == permissaoModel.VisualizarFornecedor
            && permissaoBD.FornecedorCadastro == permissaoModel.FornecedorCadastro
            && permissaoBD.FornecedorEditar == permissaoModel.FornecedorEditar
            && permissaoBD.FornecedorDetalhe == permissaoModel.FornecedorDetalhe
            && permissaoBD.FornecedorExcluir == permissaoModel.FornecedorExcluir
            && permissaoBD.VisualizarFuncionario == permissaoModel.VisualizarFuncionario
            && permissaoBD.FuncionarioCadastro == permissaoModel.FuncionarioCadastro
            && permissaoBD.FuncionarioEditar == permissaoModel.FuncionarioEditar
            && permissaoBD.FuncionarioDetalhe == permissaoModel.FuncionarioDetalhe
            && permissaoBD.FuncionarioExcluir == permissaoModel.FuncionarioExcluir
            && permissaoBD.VisualizarProduto == permissaoModel.VisualizarProduto
            && permissaoBD.ProdutoCadastro == permissaoModel.ProdutoCadastro
            && permissaoBD.ProdutoEditar == permissaoModel.ProdutoEditar
            && permissaoBD.ProdutoDetalhe == permissaoModel.ProdutoDetalhe
            && permissaoBD.ProdutoExcluir == permissaoModel.ProdutoExcluir
            && permissaoBD.GerarRelatorio == permissaoModel.GerarRelatorio
            && permissaoBD.VisualizarTransportador == permissaoModel.VisualizarTransportador
            && permissaoBD.TransportadorCadastro == permissaoModel.TransportadorCadastro
            && permissaoBD.TransportadorEditar == permissaoModel.TransportadorEditar
            && permissaoBD.TransportadorDetalhe == permissaoModel.TransportadorDetalhe
            && permissaoBD.TransportadorExcluir == permissaoModel.TransportadorExcluir
            && permissaoBD.VisualizarUsuario == permissaoModel.VisualizarUsuario
            && permissaoBD.UsuarioCadastro == permissaoModel.UsuarioCadastro
            && permissaoBD.UsuarioEditar == permissaoModel.UsuarioEditar
            && permissaoBD.UsuarioPermissoes == permissaoModel.UsuarioPermissoes
            && permissaoBD.UsuarioExcluir == permissaoModel.UsuarioExcluir
            && permissaoBD.VisualizarPedido == permissaoModel.VisualizarPedido
            && permissaoBD.PedidoCadastro == permissaoModel.PedidoCadastro
            && permissaoBD.PedidoEditar == permissaoModel.PedidoEditar
            && permissaoBD.PedidoDetalhe == permissaoModel.PedidoDetalhe
            && permissaoBD.PedidoExcluir == permissaoModel.PedidoExcluir
            && permissaoBD.VisualizarNotaFiscal == permissaoModel.VisualizarNotaFiscal
            && permissaoBD.NotaFiscalCadastro == permissaoModel.NotaFiscalCadastro
            && permissaoBD.NotaFiscalGerarPDF == permissaoModel.NotaFiscalGerarPDF
            && permissaoBD.NotaFiscalCancelar == permissaoModel.NotaFiscalCancelar
            && permissaoBD.EmpresaId == permissaoModel.EmpresaId
            && permissaoBD.UsuarioId == permissaoModel.UsuarioId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Permissao PreenchePermissoes(Permissao permissao)
        {
            var permissaoRetorno = new Permissao()
            {
                Id = permissao.Id,
                VisualizarCliente = permissao.VisualizarCliente,
                ClienteCadastro = permissao.ClienteCadastro,
                ClienteEditar = permissao.ClienteEditar,
                ClienteDetalhe = permissao.ClienteDetalhe,
                ClienteExcluir = permissao.ClienteExcluir,
                VisualizarEmpresa = permissao.VisualizarEmpresa,
                EmpresaCadastro = permissao.EmpresaCadastro,
                EmpresaEditar = permissao.EmpresaEditar,
                EmpresaDetalhe = permissao.EmpresaDetalhe,
                EmpresaExcluir = permissao.EmpresaExcluir,
                VisualizarEstoque = permissao.VisualizarEstoque,
                EstoqueEditar = permissao.EstoqueEditar,
                EstoqueDetalhe = permissao.EstoqueDetalhe,
                EstoqueExcluir = permissao.EstoqueExcluir,
                VisualizarEnderecoProduto = permissao.VisualizarEnderecoProduto,
                EnderecoProdutoCadastro = permissao.EnderecoProdutoCadastro,
                EnderecoProdutoEditar = permissao.EnderecoProdutoEditar,
                EnderecoProdutoDetalhe = permissao.EnderecoProdutoDetalhe,
                EnderecoProdutoExcluir = permissao.EnderecoProdutoExcluir,
                VisualizarFornecedor = permissao.VisualizarFornecedor,
                FornecedorCadastro = permissao.FornecedorCadastro,
                FornecedorEditar = permissao.FornecedorEditar,
                FornecedorDetalhe = permissao.FornecedorDetalhe,
                FornecedorExcluir = permissao.FornecedorExcluir,
                VisualizarFuncionario = permissao.VisualizarFuncionario,
                FuncionarioCadastro = permissao.FuncionarioCadastro,
                FuncionarioEditar = permissao.FuncionarioEditar,
                FuncionarioDetalhe = permissao.FuncionarioDetalhe,
                FuncionarioExcluir = permissao.FuncionarioExcluir,
                VisualizarProduto = permissao.VisualizarProduto,
                ProdutoCadastro = permissao.ProdutoCadastro,
                ProdutoEditar = permissao.ProdutoEditar,
                ProdutoDetalhe = permissao.ProdutoDetalhe,
                ProdutoExcluir = permissao.ProdutoExcluir,
                GerarRelatorio = permissao.GerarRelatorio,
                VisualizarTransportador = permissao.VisualizarTransportador,
                TransportadorCadastro = permissao.TransportadorCadastro,
                TransportadorEditar = permissao.TransportadorEditar,
                TransportadorDetalhe = permissao.TransportadorDetalhe,
                TransportadorExcluir = permissao.TransportadorExcluir,
                VisualizarUsuario = permissao.VisualizarUsuario,
                UsuarioCadastro = permissao.UsuarioCadastro,
                UsuarioEditar = permissao.UsuarioEditar,
                UsuarioPermissoes = permissao.UsuarioPermissoes,
                UsuarioExcluir = permissao.UsuarioExcluir,
                VisualizarPedido = permissao.VisualizarPedido,
                PedidoCadastro = permissao.PedidoCadastro,
                PedidoEditar = permissao.PedidoEditar,
                PedidoDetalhe = permissao.PedidoDetalhe,
                PedidoExcluir = permissao.PedidoExcluir,
                VisualizarNotaFiscal = permissao.VisualizarNotaFiscal,
                NotaFiscalCadastro = permissao.NotaFiscalCadastro,
                NotaFiscalGerarPDF = permissao.NotaFiscalGerarPDF,
                NotaFiscalCancelar = permissao.NotaFiscalCancelar,
                EmpresaId = permissao.EmpresaId,
                UsuarioId = permissao.UsuarioId
            };
            return permissaoRetorno;
        }

        public List<Permissao> PermissaoUsuarioId(int empresaId, int usuarioId)
        {
            try
            {
                var usuarioPermissao = _context.Permissao.AsNoTracking().Where(p => p.EmpresaId == empresaId && p.UsuarioId == usuarioId).OrderBy(p => p.Id).FirstOrDefault();
                var usuarioExistente = _context.Usuario.AsNoTracking().Where(u => u.Id == usuarioId && u.EmpresaId == empresaId).OrderBy(u => u.Id).FirstOrDefault();
                if (usuarioExistente == null)
                {
                    throw new Exception(MensagemDeErro.UsuarioNaoEncontrado);
                }
                else
                {
                    var retornoPermissao = new List<Permissao>();
                    if (usuarioPermissao == null)
                    {
                        var Permissao = new Permissao()
                        {
                            Id = (int)Ids.IdCreate,
                            VisualizarCliente = false,
                            ClienteCadastro = false,
                            ClienteEditar = false,
                            ClienteDetalhe = false,
                            ClienteExcluir = false,
                            VisualizarEmpresa = false,
                            EmpresaCadastro = false,
                            EmpresaEditar = false,
                            EmpresaDetalhe = false,
                            EmpresaExcluir = false,
                            VisualizarEstoque = false,
                            EstoqueEditar = false,
                            EstoqueDetalhe = false,
                            EstoqueExcluir = false,
                            VisualizarEnderecoProduto = false,
                            EnderecoProdutoCadastro = false,
                            EnderecoProdutoEditar = false,
                            EnderecoProdutoDetalhe = false,
                            EnderecoProdutoExcluir = false,
                            VisualizarFornecedor = false,
                            FornecedorCadastro = false,
                            FornecedorEditar = false,
                            FornecedorDetalhe = false,
                            FornecedorExcluir = false,
                            VisualizarFuncionario = false,
                            FuncionarioCadastro = false,
                            FuncionarioEditar = false,
                            FuncionarioDetalhe = false,
                            FuncionarioExcluir = false,
                            VisualizarProduto = false,
                            ProdutoCadastro = false,
                            ProdutoEditar = false,
                            ProdutoDetalhe = false,
                            ProdutoExcluir = false,
                            GerarRelatorio = false,
                            VisualizarTransportador = false,
                            TransportadorCadastro = false,
                            TransportadorEditar = false,
                            TransportadorDetalhe = false,
                            TransportadorExcluir = false,
                            VisualizarUsuario = false,
                            UsuarioCadastro = false,
                            UsuarioEditar = false,
                            UsuarioPermissoes = false,
                            UsuarioExcluir = false,
                            VisualizarPedido = false,
                            PedidoCadastro = false,
                            PedidoEditar = false,
                            PedidoDetalhe = false,
                            PedidoExcluir = false,
                            VisualizarNotaFiscal = false,
                            NotaFiscalCadastro = false,
                            NotaFiscalGerarPDF = false,
                            NotaFiscalCancelar = false,
                            EmpresaId = usuarioExistente.EmpresaId,
                            UsuarioId = usuarioExistente.Id
                        };
                        retornoPermissao.Add(Permissao);
                        return retornoPermissao;
                    }
                    else
                    {
                        var Permissao = new Permissao()
                        {
                            Id = usuarioPermissao.Id,
                            VisualizarCliente = usuarioPermissao.VisualizarCliente,
                            ClienteCadastro = usuarioPermissao.ClienteCadastro,
                            ClienteEditar = usuarioPermissao.ClienteEditar,
                            ClienteDetalhe = usuarioPermissao.ClienteDetalhe,
                            ClienteExcluir = usuarioPermissao.ClienteExcluir,
                            VisualizarEmpresa = usuarioPermissao.VisualizarEmpresa,
                            EmpresaCadastro = usuarioPermissao.EmpresaCadastro,
                            EmpresaEditar = usuarioPermissao.EmpresaEditar,
                            EmpresaDetalhe = usuarioPermissao.EmpresaDetalhe,
                            EmpresaExcluir = usuarioPermissao.EmpresaExcluir,
                            VisualizarEstoque = usuarioPermissao.VisualizarEstoque,
                            EstoqueEditar = usuarioPermissao.EstoqueEditar,
                            EstoqueDetalhe = usuarioPermissao.EstoqueDetalhe,
                            EstoqueExcluir = usuarioPermissao.EstoqueExcluir,
                            VisualizarEnderecoProduto = usuarioPermissao.VisualizarEnderecoProduto,
                            EnderecoProdutoCadastro = usuarioPermissao.EnderecoProdutoCadastro,
                            EnderecoProdutoEditar = usuarioPermissao.EnderecoProdutoEditar,
                            EnderecoProdutoDetalhe = usuarioPermissao.EnderecoProdutoDetalhe,
                            EnderecoProdutoExcluir = usuarioPermissao.EnderecoProdutoExcluir,
                            VisualizarFornecedor = usuarioPermissao.VisualizarFornecedor,
                            FornecedorCadastro = usuarioPermissao.FornecedorCadastro,
                            FornecedorEditar = usuarioPermissao.FornecedorEditar,
                            FornecedorDetalhe = usuarioPermissao.FornecedorDetalhe,
                            FornecedorExcluir = usuarioPermissao.FornecedorExcluir,
                            VisualizarFuncionario = usuarioPermissao.VisualizarFuncionario,
                            FuncionarioCadastro = usuarioPermissao.FuncionarioCadastro,
                            FuncionarioEditar = usuarioPermissao.FuncionarioEditar,
                            FuncionarioDetalhe = usuarioPermissao.FuncionarioDetalhe,
                            FuncionarioExcluir = usuarioPermissao.FuncionarioExcluir,
                            VisualizarProduto = usuarioPermissao.VisualizarProduto,
                            ProdutoCadastro = usuarioPermissao.ProdutoCadastro,
                            ProdutoEditar = usuarioPermissao.ProdutoEditar,
                            ProdutoDetalhe = usuarioPermissao.ProdutoDetalhe,
                            ProdutoExcluir = usuarioPermissao.ProdutoExcluir,
                            GerarRelatorio = usuarioPermissao.GerarRelatorio,
                            VisualizarTransportador = usuarioPermissao.VisualizarTransportador,
                            TransportadorCadastro = usuarioPermissao.TransportadorCadastro,
                            TransportadorEditar = usuarioPermissao.TransportadorEditar,
                            TransportadorDetalhe = usuarioPermissao.TransportadorDetalhe,
                            TransportadorExcluir = usuarioPermissao.TransportadorExcluir,
                            VisualizarUsuario = usuarioPermissao.VisualizarUsuario,
                            UsuarioCadastro = usuarioPermissao.UsuarioCadastro,
                            UsuarioEditar = usuarioPermissao.UsuarioEditar,
                            UsuarioPermissoes = usuarioPermissao.UsuarioPermissoes,
                            UsuarioExcluir = usuarioPermissao.UsuarioExcluir,
                            VisualizarPedido = usuarioPermissao.VisualizarPedido,
                            PedidoCadastro = usuarioPermissao.PedidoCadastro,
                            PedidoEditar = usuarioPermissao.PedidoEditar,
                            PedidoDetalhe = usuarioPermissao.PedidoDetalhe,
                            PedidoExcluir = usuarioPermissao.PedidoExcluir,
                            VisualizarNotaFiscal = usuarioPermissao.VisualizarNotaFiscal,
                            NotaFiscalCadastro = usuarioPermissao.NotaFiscalCadastro,
                            NotaFiscalGerarPDF = usuarioPermissao.NotaFiscalGerarPDF,
                            NotaFiscalCancelar = usuarioPermissao.NotaFiscalCancelar,
                            EmpresaId = usuarioPermissao.EmpresaId,
                            UsuarioId = usuarioPermissao.UsuarioId
                        };
                        retornoPermissao.Add(Permissao);
                        return retornoPermissao;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}