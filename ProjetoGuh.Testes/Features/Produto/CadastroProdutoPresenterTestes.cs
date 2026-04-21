using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Presenter;
using ProjetoGuh.Features.Produto.Repository;

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
        _viewMock.Setup(v => v.ObterDescricao()).Returns("Produto Teste");
        _viewMock.Setup(v => v.ObterPreco()).Returns(100.0M);
        _viewMock.Setup(v => v.ObterEstoque()).Returns(10);
        _viewMock.Setup(v => v.ObterStatusAtivo()).Returns('S');
        _presenter.Salvar();
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ProdutoModel>()), Times.Once);
        _viewMock.Verify(v => v.ExibirMensagem("Produto cadastrado com sucesso!"), Times.Once);
    }

    [Test]
    public void Salvar_ProdutoSemDescricao_NaoDeveIncluirNoBanco()
    {
        _viewMock.Setup(v => v.ObterDescricao()).Returns("");
        _viewMock.Setup(v => v.ObterPreco()).Returns(100.0M);
        _viewMock.Setup(v => v.ObterEstoque()).Returns(10);
        _viewMock.Setup(v => v.ObterStatusAtivo()).Returns('S');
        _presenter.Salvar();
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ProdutoModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagemErro("Descrição do produto é obrigatória."), Times.AtLeastOnce);
    }

    [Test]
    public void Excluir_QuandoConfirmado_DeveExcluirNoBanco()
    {
        var produto = new ProdutoModel { Id = 1, Descricao = "Teste" };
        _viewMock.Setup(v => v.ConfirmarExclusao()).Returns(true);
        _presenter.Excluir(produto.Id);
        _repositoryMock.Verify(d => d.Excluir(produto.Id), Times.Once);
    }
}
