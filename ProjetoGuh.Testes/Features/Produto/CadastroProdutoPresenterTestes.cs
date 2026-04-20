using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Presenter;
using ProjetoGuh.Features.Produto.Repository;
using ProjetoGuh.Features.Produto.View;

[TestFixture]
public class ProdutoPresenterTests
{
    private Mock<ICadastroProdutoView> _viewMock;
    private Mock<IProdutoRepository> _repositoryMock;
    private CadastroProdutoPresenter _presenter;
    [SetUp]
    public void Setup()
    {
        _viewMock = new Mock<ICadastroProdutoView>();
        _repositoryMock = new Mock<IProdutoRepository>();
        _presenter = new CadastroProdutoPresenter(_repositoryMock.Object);
        _presenter.SetView(_viewMock.Object);
    }
    [Test]
    public void Salvar_ProdutoValido_DeveIncluirNoBanco()
    {
        // Arrange: Configura a view para retornar um produto preenchido
        var produto = new ProdutoModel { Descricao = "Produto Teste", Preco = 100.0M, Estoque = 10 };
        _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(produto);

        // Act: Dispara a ação de salvar
        _presenter.Salvar();

        // Assert: Verifica se o método Incluir do DAO foi chamado exatamente uma vez
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ProdutoModel>()), Times.Once);
        _viewMock.Verify(v => v.ExibirMensagem("Produto cadastrado com sucesso!"), Times.Once);
    }

    [Test]
    public void Salvar_ProdutoSemDescricao_NaoDeveIncluirNoBanco()
    {
        // Arrange: Produto com descrição vazia
        var produto = new ProdutoModel { Descricao = "", Preco = 100.0M, Estoque = 10 };
        _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(produto);

        // Act
        _presenter.Salvar();

        // Assert: O DAO nunca deve ser chamado se os dados forem inválidos
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ProdutoModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagemErro("Descrição do produto é obrigatória."), Times.AtLeastOnce);
    }

    [Test]
    public void Excluir_QuandoConfirmado_DeveExcluirNoBanco()
    {
        // Arrange: Simula que o usuário clicou em "Sim" na caixa de confirmação
        var produto = new ProdutoModel { Id = 1, Descricao = "Teste" };
        _viewMock.Setup(v => v.ConfirmarExclusao()).Returns(true);
        _viewMock.Setup(v => v.ObterProdutoSelecionado()).Returns(produto);

        // Act
        _presenter.Excluir(produto.Id);

        // Assert
        _repositoryMock.Verify(d => d.Excluir(produto.Id), Times.Once);
    }
}
