using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.Repository;
using ProjetoGuh.Features.Venda.View;
using ProjetoGuh.Features.Produto.Repository;
using System;
using System.Linq;

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

        // Esta é a venda que está sendo montada na memória
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
            // Assinando os eventos da IPdvView
            _view.BotaoAdicionarItemClicado += (s, e) => AdicionarItem();
            _view.BotaoRemoverItemClicado += (s, e) => RemoverItem();
            _view.BotaoFinalizarVendaClicado += (s, e) => FinalizarVenda();
            _view.BotaoCancelarVendaClicado += (s, e) => CancelarVenda();
            _view.ProdutoSelecionadoMudou += (s, e) => AtualizarPrecoProduto();
        }

        public void Inicializar()
        {
            _vendaAtiva = new VendaModel();

            // Carrega os dados iniciais dos ComboBoxes
            _view.PreencherComboClientes(_clienteRepository.Listar());
            _view.PreencherComboProdutos(_produtoRepository.Listar());
            _view.PreencherComboFormasPagamento(_formaPagamentoRepository.Listar());

            _view.AtualizarValorTotalVenda(0);
        }

        public void AtualizarPrecoProduto()
        {
            var produto = _view.ObterProdutoSelecionado();
            if (produto != null)
            {
                _view.AtualizarPrecoUnitario(produto.Preco);
            }
        }
        public int ObterQuantidade()
        {
            return (int)_view.ObterQuantidade();
        }
        public void AdicionarItem()
        {
            var produto = _view.ObterProdutoSelecionado();
            var quantidade = (int)_view.ObterQuantidade();

            if (produto == null)
            {
                _view.ExibirMensagem("Selecione um produto.");
                return;
            }else if(quantidade <=  0){
                _view.ExibirMensagem("A quantidade deve ser maior que zero.");
                return;
            }

            // Cria o item e adiciona na lista da memória
            var novoItem = new ItemVendaModel
            {
                IdProduto = produto.Id,
                DescricaoProduto = produto.Descricao, // Importante para exibir na Grid
                Quantidade = quantidade,
                ValorUnitario = produto.Preco,
                ValorTotal = quantidade * produto.Preco
            };

            _vendaAtiva.Itens.Add(novoItem);

            // Recalcula o total da venda
            _vendaAtiva.ValorTotal = _vendaAtiva.Itens.Sum(i => i.ValorTotal);

            // Atualiza a tela
            _view.AtualizarGridItens(_vendaAtiva.Itens.ToList());
            _view.AtualizarValorTotalVenda(_vendaAtiva.ValorTotal);
            _view.LimparCamposItem();
        }

        public void RemoverItem()
        {
            var item = _view.ObterItemSelecionado();

            if (item != null)
            {
                _vendaAtiva.Itens.Remove(item);

                // Recalcula o total
                _vendaAtiva.ValorTotal = _vendaAtiva.Itens.Sum(i => i.ValorTotal);

                // Manda a View atualizar a tela
                _view.AtualizarGridItens(_vendaAtiva.Itens.ToList());
                _view.AtualizarValorTotalVenda(_vendaAtiva.ValorTotal);
            }
            else
            {
                _view.ExibirMensagem("Selecione um item na lista para remover.");
            }
        }

        // Mude de SalvarVenda para FinalizarVenda
        public void FinalizarVenda()
        {
            try
            {
                _vendaAtiva.IdCliente = _view.ObterClienteSelecionadoId();
                _vendaAtiva.IdFormaPagamento = _view.ObterFormaPagamentoId();
                _vendaAtiva.Observacao = _view.ObterObservacao();
                _vendaAtiva.DataVenda = DateTime.Now;

                var erros = _validator.Validar(_vendaAtiva);
                if (erros.Count > 0)
                {
                    _view.ExibirMensagem(string.Join("\n", erros));
                    return;
                }

                // Manda o Repository resolver
                _repository.GravarVendaCompleta(_vendaAtiva);

                _view.ExibirMensagem("Venda realizada com sucesso!");
                _view.ReiniciarFormulario();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagem($"Erro ao gravar: {ex.Message}");
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
                // Sempre pedir confirmação para exclusões de registros no banco
                if (_view.ExibirMensagemPerguntar($"Tem certeza que deseja excluir a venda nº {id}?"))
                {
                    _repository.Excluir(id);
                    _view.ExibirMensagem("Venda excluída com sucesso!");

                    // Atualiza a tela/lista se necessário
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