using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Linq;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.View;
using ProjetoGuh.Features.Venda.Repository;
using ProjetoGuh.Features.Cliente.Dao; // Adicione os DAOs necessários para carregar os combos
using ProjetoGuh.Features.Produto.Dao;
using ProjetoGuh.Features.Venda.Dao;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public class CadastroVendaPresenter : ICadastroVendaPresenter
    {
        private IPdvView _view;
        private readonly IVendaRepository _repository;
        private readonly IClienteDao _clienteDao;
        private readonly IProdutoDao _produtoDao;
        private readonly IFormaPagamentoDao _formaPagamentoDao;

        // Esta é a venda que está sendo montada na memória
        private VendaModel _vendaAtiva;

        public CadastroVendaPresenter(
            IVendaRepository repository,
            IClienteDao clienteDao,
            IProdutoDao produtoDao,
            IFormaPagamentoDao formaPagamentoDao)
        {
            _repository = repository;
            _clienteDao = clienteDao;
            _produtoDao = produtoDao;
            _formaPagamentoDao = formaPagamentoDao;
            _vendaAtiva = new VendaModel();
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
            _view.PreencherComboClientes(_clienteDao.Listar());
            _view.PreencherComboProdutos(_produtoDao.Listar());
            _view.PreencherComboFormasPagamento(_formaPagamentoDao.Listar());

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
            // Se você usa um NumericUpDown, o .Value é decimal, por isso o (int)
            return (int)_view.ObterQuantidade();
        }
        public void AdicionarItem()
        {
            var produto = _view.ObterProdutoSelecionado();
            var quantidade = (int)_view.ObterQuantidade();

            if (produto == null)
            {
                ControleDeMensagens.Avisar("Selecione um produto.");
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
                ControleDeMensagens.Avisar("Selecione um item na lista para remover.");
            }
        }

        // Mude de SalvarVenda para FinalizarVenda
        public void FinalizarVenda()
        {
            try
            {
                // Preenche os dados
                _vendaAtiva.IdCliente = _view.ObterClienteSelecionadoId();
                _vendaAtiva.IdFormaPagamento = _view.ObterFormaPagamentoId();
                _vendaAtiva.Observacao = _view.ObterObservacao();
                _vendaAtiva.DataVenda = DateTime.Now;

                // Manda o Repository resolver
                _repository.GravarVendaCompleta(_vendaAtiva);

                ControleDeMensagens.Informar("Venda realizada com sucesso!");
                _view.ReiniciarFormulario();
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao gravar: {ex.Message}");
            }
        }
        public void CancelarVenda()
        {
            if (ControleDeMensagens.Perguntar("Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos."))
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
                if (ControleDeMensagens.Perguntar($"Tem certeza que deseja excluir a venda nº {id}?"))
                {
                    _repository.Excluir(id);
                    ControleDeMensagens.Informar("Venda excluída com sucesso!");

                    // Atualiza a tela/lista se necessário
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao excluir venda: {ex.Message}");
            }
        }
    }
}