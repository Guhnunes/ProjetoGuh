using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Produto.Repository;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.Repository;
using ProjetoGuh.Features.Venda.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public class CadastroVendaPresenter : BasePresenter<IPdvView>, ICadastroVendaPresenter
    {
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
            : base(null)
        {
            _repository = vendaRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _vendaAtiva = new VendaModel();
            _validator = new VendaModelValidator();
        }

        public override void SetView(IPdvView view)
        {
            base.SetView(view);
            View.BotaoAdicionarItemClicado += (s, e) => AdicionarItem();
            View.BotaoRemoverItemClicado += (s, e) => RemoverItem();
            View.BotaoFinalizarVendaClicado += (s, e) => FinalizarVenda();
            View.BotaoCancelarVendaClicado += (s, e) => CancelarVenda();
            View.ProdutoSelecionadoMudou += (s, e) => AtualizarDadosProdutoSelecionado();
            Inicializar();
        }

        public void Inicializar()
        {
            _vendaAtiva = new VendaModel { Itens = new List<ItemVendaModel>() };

            // Passamos como object para manter a View cega para o Model
            View.PreencherComboClientes(_clienteRepository.Listar());
            var listaProdutos = _produtoRepository.Listar();
            var produtosAtivos = listaProdutos.Where(p => p.Ativo == 'S').ToList();
            View.PreencherComboProdutos(produtosAtivos);
            View.PreencherComboFormasPagamento(_formaPagamentoRepository.Listar() ?? new List<FormaPagamentoModel>());
            View.AtualizarValorTotalVenda(0);
        }

        public void AtualizarDadosProdutoSelecionado()
        {
            int? produtoId = View.ObterProdutoSelecionadoId();

            if (produtoId.HasValue)
            {
                var produto = _produtoRepository.RetornarPorId(produtoId.Value);

                if (produto != null)
                {
                    View.AtualizarPrecoUnitario(produto.Preco);
                }
                else
                {
                    // Se cair aqui, o ID existe no Combo, mas não existe no seu Banco/Lista
                    View.AtualizarPrecoUnitario(0);
                }
            }
            else
            {
                View.AtualizarPrecoUnitario(0);
            }
        }

        public void AdicionarItem()
        {
            int? produtoId = View.ObterProdutoSelecionadoId();
            decimal quantidade = View.ObterQuantidade();

            if (!produtoId.HasValue)
            {
                View.ExibirMensagem("Selecione um produto.");
                return;
            }

            // Busca o produto
            var produto = _produtoRepository.RetornarPorId(produtoId.Value);

            // --- CORREÇÃO AQUI: Verifica se o produto existe ---
            if (produto == null)
            {
                View.ExibirMensagemErro("Produto não encontrado no cadastro.");
                return;
            }

            if (quantidade <= 0)
            {
                View.ExibirMensagem("A quantidade deve ser maior que zero.");
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
            View.LimparCamposItem();
        }

        public void RemoverItem()
        {
            // No PDV, geralmente removemos pelo índice da Grid ou ID do item
            int? index = View.ObterIndexItemSelecionado();

            if (index.HasValue && index >= 0 && index < _vendaAtiva.Itens.Count)
            {
                _vendaAtiva.Itens.RemoveAt(index.Value);
                RecalcularTotais();
            }
            else
            {
                View.ExibirMensagem("Selecione um item na lista para remover.");
            }
        }

        private void RecalcularTotais()
        {
            _vendaAtiva.ValorTotal = _vendaAtiva.Itens.Sum(i => i.ValorTotal);

            // Injetamos os dados brutos na View
            View.AtualizarGridItens(_vendaAtiva.Itens.ToList());
            View.AtualizarValorTotalVenda(_vendaAtiva.ValorTotal);
        }

        public void FinalizarVenda()
        {
            try
            {
                _vendaAtiva.IdCliente = View.ObterClienteSelecionadoId() ?? 0;
                _vendaAtiva.IdFormaPagamento = View.ObterFormaPagamentoId() ?? 0;
                _vendaAtiva.Observacao = View.ObterObservacao();
                _vendaAtiva.DataVenda = DateTime.Now;

                var erros = _validator.Validar(_vendaAtiva);
                if (erros.Count > 0)
                {
                    View.ExibirMensagemErro(string.Join("\n", erros));
                    return;
                }

                _repository.GravarVendaCompleta(_vendaAtiva);

                View.ExibirMensagem("Venda realizada com sucesso!");
                View.ReiniciarFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao gravar venda: {ex.Message}");
            }
        }

        public void CancelarVenda()
        {
            if (View.ExibirMensagemPerguntar("Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos."))
            {
                View.ReiniciarFormulario();
                Inicializar();
            }
        }

        public void Excluir(int id)
        {
            try
            {
                if (View.ExibirMensagemPerguntar($"Tem certeza que deseja excluir a venda nº {id}?"))
                {
                    _repository.Excluir(id);
                    View.ExibirMensagem("Venda excluída com sucesso!");
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao excluir venda: {ex.Message}");
            }
        }
    }
}