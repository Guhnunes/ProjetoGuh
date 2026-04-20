using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Cliente.View;
using ProjetoGuh.Features.Cliente.Presenter;

[TestFixture]
public class ClientePresenterTests
{
    private Mock<ICadastroClienteView> _viewMock;
    private Mock<IClienteRepository> _repositoryMock;
    private CadastroClientePresenter _presenter;

    [SetUp]
    public void Setup()
    {
        _viewMock = new Mock<ICadastroClienteView>();
        _repositoryMock = new Mock<IClienteRepository>();
        _presenter = new CadastroClientePresenter(_repositoryMock.Object);
        _presenter.SetView(_viewMock.Object);
    }

    [Test]
    public void Salvar_ClienteValido_DeveIncluirNoBanco()
    {
        // Arrange: Configura a view para retornar um cliente preenchido
        var cliente = new ClienteModel { Nome = "Caio", CpfCnpj = "12345678901", Email = "caio@example.com" , Telefone = "1234567890" };
        _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(cliente);

        // Act: Dispara a ação de salvar
        _presenter.Salvar();

        // Assert: Verifica se o método Incluir do DAO foi chamado exatamente uma vez
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ClienteModel>()), Times.Once);
        _viewMock.Verify(v => v.ExibirMensagem("Cliente cadastrado com sucesso!"), Times.Once);
    }

    [Test]
    public void Salvar_ClienteSemNome_NaoDeveIncluirNoBanco()
    {
        // Arrange: Cliente com nome vazio
        var cliente = new ClienteModel { Nome = "", CpfCnpj = "12345678901", Email = "caio@example.com", Telefone = "1234567890" };
        _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(cliente);

        // Act
        _presenter.Salvar();

        // Assert: O DAO nunca deve ser chamado se os dados forem inválidos
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ClienteModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagemErro("Nome é obrigatório."), Times.AtLeastOnce);
    }

    [Test]
    public void Excluir_QuandoConfirmado_DeveExcluirNoBanco()
    {
        // Arrange: Simula que o usuário clicou em "Sim" na caixa de confirmação
        var cliente = new ClienteModel { Id = 1, Nome = "Teste" };
        _viewMock.Setup(v => v.ConfirmarExclusao()).Returns(true);
        _viewMock.Setup(v => v.ObterClienteSelecionado()).Returns(cliente);

        // Act
        _presenter.Excluir(cliente.Id);

        // Assert
        _repositoryMock.Verify(d => d.Excluir(cliente.Id), Times.Once);
    }

    [Test]
    public void Excluir_QuandoNaoConfirmado_NaoDeveExcluirNoBanco()
    {
        var cliente = new ClienteModel { Id = 1, Nome = "Teste" };
        // Arrange: Simula que o usuário clicou em "Não"
        _viewMock.Setup(v => v.ConfirmarExclusao()).Returns(false);

        // Act
        _presenter.Excluir(cliente.Id);

        // Assert
        _repositoryMock.Verify(d => d.Excluir(It.IsAny<int>()), Times.Never);
    }
}