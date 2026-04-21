using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.Repository;
using ProjetoGuh.Features.Venda.View;
using ProjetoGuh.Features.Produto.Repository;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public class CadastroVendaPresenter : ICadastroVendaPresenter
    {
        private IPdvView _view;
        private readonly IVendaRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly VendaModelValidator _validator;

        private VendaModel _vendaAtiva;

        public CadastroVendaPresenter(
            IVendaRepository vendaRepository,
            IClienteRepository clienteRepository,
            IProdutoRepository produtoRepository,
            IFormaPagamentoRepository formaPagamentoRepository)
        {
            _repository = vendaRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _vendaAtiva = new VendaModel();
            _validator = new VendaModelValidator();
        }

        public void SetView(IPdvView view)
        {
            _view = view;
            _view.BotaoAdicionarItemClicado += (s, e) => AdicionarItem();
            _view.BotaoRemoverItemClicado += (s, e) => RemoverItem();
            _view.BotaoFinalizarVendaClicado += (s, e) => FinalizarVenda();
            _view.BotaoCancelarVendaClicado += (s, e) => CancelarVenda();
            _view.ProdutoSelecionadoMudou += (s, e) => AtualizarDadosProdutoSelecionado();
        }

        public void Inicializar()
        {
            _vendaAtiva = new VendaModel { Itens = new List<ItemVendaModel>() };

            // Passamos como object para manter a View cega para o Model
            _view.PreencherComboClientes(_clienteRepository.Listar());
            var listaProdutos = _produtoRepository.Listar();
            var produtosAtivos = listaProdutos.Where(p => p.Ativo == 'S').ToList();
            _view.PreencherComboProdutos(produtosAtivos);
            _view.PreencherComboFormasPagamento(_formaPagamentoRepository.Listar());

            _view.AtualizarValorTotalVenda(0);
        }

        public void AtualizarDadosProdutoSelecionado()
        {
            int? produtoId = _view.ObterProdutoSelecionadoId();

            if (produtoId.HasValue)
            {
                var produto = _produtoRepository.RetornarPorId(produtoId.Value);

                if (produto != null)
                {
                    _view.AtualizarPrecoUnitario(produto.Preco);
                }
                else
                {
                    // Se cair aqui, o ID existe no Combo, mas não existe no seu Banco/Lista
                    _view.AtualizarPrecoUnitario(0);
                }
            }
            else
            {
                _view.AtualizarPrecoUnitario(0);
            }
        }

        public void AdicionarItem()
        {
            int? produtoId = _view.ObterProdutoSelecionadoId();
            decimal quantidade = _view.ObterQuantidade();

            if (!produtoId.HasValue)
            {
                _view.ExibirMensagem("Selecione um produto.");
                return;
            }

            // Busca o produto
            var produto = _produtoRepository.RetornarPorId(produtoId.Value);

            // --- CORREÇÃO AQUI: Verifica se o produto existe ---
            if (produto == null)
            {
                _view.ExibirMensagemErro("Produto não encontrado no cadastro.");
                return;
            }

            if (quantidade <= 0)
            {
                _view.ExibirMensagem("A quantidade deve ser maior que zero.");
                return;
            }

            // Agora é seguro acessar as propriedades do produto
            var novoItem = new ItemVendaModel
            {
                IdProduto = produto.Id,
                DescricaoProduto = produto.Descricao,
                Quantidade = (int)quantidade,
                ValorUnitario = produto.Preco,
                ValorTotal = quantidade * produto.Preco
            };

            _vendaAtiva.Itens.Add(novoItem);
            RecalcularTotais();
            _view.LimparCamposItem();
        }

        public void RemoverItem()
        {
            // No PDV, geralmente removemos pelo índice da Grid ou ID do item
            int? index = _view.ObterIndexItemSelecionado();

            if (index.HasValue && index >= 0 && index < _vendaAtiva.Itens.Count)
            {
                _vendaAtiva.Itens.RemoveAt(index.Value);
                RecalcularTotais();
            }
            else
            {
                _view.ExibirMensagem("Selecione um item na lista para remover.");
            }
        }

        private void RecalcularTotais()
        {
            _vendaAtiva.ValorTotal = _vendaAtiva.Itens.Sum(i => i.ValorTotal);

            // Injetamos os dados brutos na View
            _view.AtualizarGridItens(_vendaAtiva.Itens.ToList());
            _view.AtualizarValorTotalVenda(_vendaAtiva.ValorTotal);
        }

        public void FinalizarVenda()
        {
            try
            {
                _vendaAtiva.IdCliente = _view.ObterClienteSelecionadoId() ?? 0;
                _vendaAtiva.IdFormaPagamento = _view.ObterFormaPagamentoId() ?? 0;
                _vendaAtiva.Observacao = _view.ObterObservacao();
                _vendaAtiva.DataVenda = DateTime.Now;

                var erros = _validator.Validar(_vendaAtiva);
                if (erros.Count > 0)
                {
                    _view.ExibirMensagemErro(string.Join("\n", erros));
                    return;
                }

                _repository.GravarVendaCompleta(_vendaAtiva);

                _view.ExibirMensagem("Venda realizada com sucesso!");
                _view.ReiniciarFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao gravar venda: {ex.Message}");
            }
        }

        public void CancelarVenda()
        {
            if (_view.ExibirMensagemPerguntar("Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos."))
            {
                _view.ReiniciarFormulario();
                Inicializar();
            }
        }

        public void Excluir(int id)
        {
            try
            {
                if (_view.ExibirMensagemPerguntar($"Tem certeza que deseja excluir a venda nº {id}?"))
                {
                    _repository.Excluir(id);
                    _view.ExibirMensagem("Venda excluída com sucesso!");
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao excluir venda: {ex.Message}");
            }
        }
    }
}