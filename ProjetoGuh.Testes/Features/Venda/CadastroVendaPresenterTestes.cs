using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Repository;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.Presenter;
using ProjetoGuh.Features.Venda.Repository;
using ProjetoGuh.Features.Venda.View;
using System.Collections.Generic;

[TestFixture]
public class VendaPresenterTests
{
    private Mock<IPdvView> _viewMock;
    private Mock<IVendaRepository> _repositoryMock;
    private Mock<IClienteRepository> _clienteMock;
    private Mock<IProdutoRepository> _produtoMock;
    private Mock<IFormaPagamentoRepository> _formaPagamentoRepositoryMock;
    private CadastroVendaPresenter _presenter;

    [SetUp]
    public void Setup()
    {
        _viewMock = new Mock<IPdvView>();
        _repositoryMock = new Mock<IVendaRepository>();
        _clienteMock = new Mock<IClienteRepository>();
        _produtoMock = new Mock<IProdutoRepository>();
        _formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();

        // --- CONFIGURAÇÃO GLOBAL (Resolve o erro do Inicializar) ---
        _repositoryMock.Setup(r => r.Listar()).Returns(new List<VendaModel>());
        _clienteMock.Setup(r => r.Listar()).Returns(new List<ClienteModel>());
        _produtoMock.Setup(r => r.Listar()).Returns(new List<ProdutoModel>());
        _formaPagamentoRepositoryMock.Setup(r => r.Listar()).Returns(new List<FormaPagamentoModel>());

        _presenter = new CadastroVendaPresenter(
            _repositoryMock.Object,
            _clienteMock.Object,
            _produtoMock.Object,
            _formaPagamentoRepositoryMock.Object
        );

        // Agora, ao chamar SetView, o Inicializar() encontrará listas vazias em vez de nulo
        _presenter.SetView(_viewMock.Object);
    }
    [Test]
    public void AdicionarItem_ProdutoValido_DeveAdicionarNaLista()
    {
        var produtoId = 1;
        var produtoEsperado = new ProdutoModel { Id = produtoId, Descricao = "Coca-Cola", Preco = 5.00M, Ativo = 'S' };

        _viewMock.Setup(v => v.ObterProdutoSelecionadoId()).Returns(produtoId);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(2);
        _produtoMock.Setup(r => r.RetornarPorId(produtoId)).Returns(produtoEsperado);
        _presenter.AdicionarItem();
        _viewMock.Verify(v => v.AtualizarGridItens(It.Is<List<ItemVendaModel>>(l => l.Count == 1)), Times.Once);
        _viewMock.Verify(v => v.AtualizarValorTotalVenda(10.00M), Times.Once);
    }
    [Test]
    public void AdicionarItem_QuantidadeZero_NaoDeveAdicionarNaLista()
    {
        var produtoId = 1;
        var produtoEsperado = new ProdutoModel { Id = produtoId, Descricao = "Coca-Cola", Preco = 5.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionadoId()).Returns(produtoId);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(0);
        _produtoMock.Setup(r => r.RetornarPorId(produtoId)).Returns(produtoEsperado);
        _presenter.AdicionarItem();
        _viewMock.Verify(v => v.ExibirMensagem("A quantidade deve ser maior que zero."), Times.Once);
    }
    [Test]
    public void FinalizarVenda_SemItens_NaoDeveSalvar()
    {
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(1);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste sem itens");
        _presenter.FinalizarVenda();
        _repositoryMock.Verify(d => d.GravarVendaCompleta(It.IsAny<VendaModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagemErro(It.Is<string>(s =>
        s.Contains("A venda deve conter pelo menos um item.") &&
        s.Contains("O preço de venda deve ser maior que zero.")
        )), Times.Once);
    }
    [Test]
    public void FinalizarVenda_SemFormaPagamento_NaoDeveSalvar()
    {
        var produtoId = 1;
        var produtoEsperado = new ProdutoModel { Id = produtoId, Descricao = "Coca-Cola", Preco = 5.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionadoId()).Returns(produtoId);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(1);
        _produtoMock.Setup(r => r.RetornarPorId(produtoId)).Returns(produtoEsperado);
        _presenter.AdicionarItem();
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(0);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste sem forma de pagamento");
        _presenter.FinalizarVenda();
        _repositoryMock.Verify(d => d.GravarVendaCompleta(It.IsAny<VendaModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagemErro(It.Is<string>(s =>
        s.Contains("A forma de pagamento deve ser selecionada.")
        )), Times.Once);
    }
    [Test]
    public void FinalizarVenda_VendaValida_DeveChamarIncluirNoDao()
    {
        // Arrange
        _presenter.SetView(_viewMock.Object); // Garante a vinculação
        var produtoId = 1;
        var produtoEsperado = new ProdutoModel { Id = produtoId, Descricao = "Coca-Cola", Preco = 5.00M };

        _viewMock.Setup(v => v.ObterProdutoSelecionadoId()).Returns(produtoId);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(1);
        _produtoMock.Setup(r => r.RetornarPorId(produtoId)).Returns(produtoEsperado);
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(1);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste com itens");
        _presenter.AdicionarItem();
        _presenter.FinalizarVenda();
        _repositoryMock.Verify(d => d.GravarVendaCompleta(It.IsAny<VendaModel>()), Times.Once);
        _viewMock.Verify(v => v.ExibirMensagem("Venda realizada com sucesso!"), Times.Once);
    }
    [Test]
    public void FinalizarVenda_VendaValida_DeveLimparATela()
    {
        _presenter.SetView(_viewMock.Object);
        var produtoId = 1;
        var produtoEsperado = new ProdutoModel { Id = produtoId, Descricao = "Coca-Cola", Preco = 5.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionadoId()).Returns(produtoId);
        _produtoMock.Setup(r => r.RetornarPorId(produtoId)).Returns(produtoEsperado);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(1);
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(1);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste com itens");
        _presenter.AdicionarItem();
        _presenter.FinalizarVenda();
        _viewMock.Verify(v => v.ReiniciarFormulario(), Times.Once);
    }
    [Test]
    public void CancelarVenda_QuandoConfirmado_DeveLimparATela()
    {
        string msg = "Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos.";
        _viewMock.Setup(v => v.ExibirMensagemPerguntar(msg)).Returns(true);
        // Mocks necessários para o Inicializar() que é chamado dentro do CancelarVenda
        _clienteMock.Setup(r => r.Listar()).Returns(new List<ClienteModel>());
        _produtoMock.Setup(r => r.Listar()).Returns(new List<ProdutoModel>());
        _formaPagamentoRepositoryMock.Setup(r => r.Listar()).Returns(new List<FormaPagamentoModel>());
        _presenter.CancelarVenda();
        _viewMock.Verify(v => v.ReiniciarFormulario(), Times.Once);
        _viewMock.Verify(v => v.PreencherComboClientes(It.IsAny<IEnumerable<ClienteModel>>()), Times.AtLeastOnce);
        _viewMock.Verify(v => v.PreencherComboProdutos(It.IsAny<IEnumerable<ProdutoModel>>()), Times.AtLeastOnce);
    }
    [Test]
    public void CancelarVenda_QuandoNaoConfirmado_NaoDeveLimparATela()
    {
        string msg = "Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos.";
        _viewMock.Setup(v => v.ExibirMensagemPerguntar(msg)).Returns(false);
        _presenter.CancelarVenda();
        _viewMock.Verify(v => v.ReiniciarFormulario(), Times.Never);
        _viewMock.Verify(v => v.PreencherComboClientes(It.IsAny<object>()), Times.Exactly(1));
        _viewMock.Verify(v => v.PreencherComboProdutos(It.IsAny<object>()), Times.Exactly(1));
    }
}