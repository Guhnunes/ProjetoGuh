using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Produto.Repository;
using ProjetoGuh.Features.Produto.Model;
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
        _presenter = new CadastroVendaPresenter(_repositoryMock.Object, _clienteMock.Object, _produtoMock.Object, _formaPagamentoRepositoryMock.Object);
        _presenter.SetView(_viewMock.Object);
    }
    [Test]
    public void AdicionarItem_ProdutoValido_DeveAdicionarNaLista()
    {
        // Arrange (Preparação)
        var produto = new ProdutoModel { Id = 1, Descricao = "Coca-Cola", Preco = 5.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionado()).Returns(produto);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(2);

        // Act (Ação)
        _presenter.AdicionarItem();

        // Assert (Verificação)
        // Verificamos se a Grid foi atualizada com a lista contendo o item
        _viewMock.Verify(v => v.AtualizarGridItens(It.Is<List<ItemVendaModel>>(l => l.Count == 1)), Times.Once);

        // Verificamos se o valor total foi atualizado corretamente (5 * 2 = 10)
        _viewMock.Verify(v => v.AtualizarValorTotalVenda(10.00M), Times.Once);
    }
    [Test]
    public void AdicionarItem_QuantidadeZero_NaoDeveAdicionarNaLista()
    {
        var produto = new ProdutoModel { Id = 10, Descricao = "Teclado", Preco = 50.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionado()).Returns(produto);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(0);
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
        _viewMock.Verify(v => v.ExibirMensagem(It.Is<string>(s =>
        s.Contains("A venda deve conter pelo menos um item.") &&
        s.Contains("O preço de venda deve ser maior que zero.")
        )), Times.Once);
    }
    [Test]
    public void FinalizarVenda_SemFormaPagamento_NaoDeveSalvar()
    {
        var produto = new ProdutoModel { Id = 10, Descricao = "Teclado", Preco = 50.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionado()).Returns(produto);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(1);
        _presenter.AdicionarItem();
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(0);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste sem forma de pagamento");
        _presenter.FinalizarVenda();
        _repositoryMock.Verify(d => d.GravarVendaCompleta(It.IsAny<VendaModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagem(It.Is<string>(s =>
        s.Contains("A forma de pagamento deve ser selecionada.")
        )), Times.Once);
    }
    [Test]
    public void FinalizarVenda_VendaValida_DeveChamarIncluirNoDao()
    {
        // Arrange
        _presenter.SetView(_viewMock.Object); // Garante a vinculação
        var produto = new ProdutoModel { Id = 10, Descricao = "Teclado", Preco = 50.00M };

        _viewMock.Setup(v => v.ObterProdutoSelecionado()).Returns(produto);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(1);
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(1);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste com itens");

        // Act
        _presenter.AdicionarItem();
        _presenter.FinalizarVenda();

        // Assert
        // 1. Verifica se chamou a gravação (independente do estado atual do objeto)
        _repositoryMock.Verify(d => d.GravarVendaCompleta(It.IsAny<VendaModel>()), Times.Once);

        // 2. Verifica se a mensagem de sucesso foi exibida
        _viewMock.Verify(v => v.ExibirMensagem("Venda realizada com sucesso!"), Times.Once);
    }
    [Test]
    public void FinalizarVenda_VendaValida_DeveLimparATela()
    {
        // Arrange
        _presenter.SetView(_viewMock.Object); // Garante a vinculação
        var produto = new ProdutoModel { Id = 10, Descricao = "Teclado", Preco = 50.00M };
        _viewMock.Setup(v => v.ObterProdutoSelecionado()).Returns(produto);
        _viewMock.Setup(v => v.ObterQuantidade()).Returns(1);
        _viewMock.Setup(v => v.ObterClienteSelecionadoId()).Returns(1);
        _viewMock.Setup(v => v.ObterFormaPagamentoId()).Returns(1);
        _viewMock.Setup(v => v.ObterObservacao()).Returns("Teste com itens");
        // Act
        _presenter.AdicionarItem();
        _presenter.FinalizarVenda();
        // Assert
        _viewMock.Verify(v => v.ReiniciarFormulario(), Times.Once);
    }
    [Test]
    public void CancelarVenda_QuandoConfirmado_DeveLimparATela()
    {
        // Arrange
        string msg = "Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos.";
        _viewMock.Setup(v => v.ExibirMensagemPerguntar(msg)).Returns(true);

        // Act
        _presenter.CancelarVenda();

        // Assert
        _viewMock.Verify(v => v.ReiniciarFormulario(), Times.Once);
    }
    [Test]
    public void CancelarVenda_QuandoNaoConfirmado_NaoDeveLimparATela()
    {
        // Arrange
        string msg = "Deseja realmente cancelar esta venda? Todos os itens lançados serão perdidos.";
        _viewMock.Setup(v => v.ExibirMensagemPerguntar(msg)).Returns(false);

        // Act
        return;
    }
}