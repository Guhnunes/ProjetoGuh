using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Cliente;

[TestFixture]
public class CadastroClientePresenterTestes
{
    private Mock<ICadastroClienteView> _viewMock;
    private Mock<IClienteRepository> _repositoryMock;
    private CadastroClientePresenter _presenter;

    [SetUp]
    public void Configurar()
    {
        _viewMock = new Mock<ICadastroClienteView>();
        _repositoryMock = new Mock<IClienteRepository>();

        // Ajuste aqui para passar os dois mocks se o seu construtor pedir ambos
        _presenter = new CadastroClientePresenter(_repositoryMock.Object);
    }

    [Test]
    public void Salvar_ClienteNovoValido_DeveChamarIncluirNoRepositorio()
    {
        // Arrange
        var cliente = new ClienteModel { Id = 0, Nome = "GUH NUNES", CpfCnpj = "12345678901" };
        _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(cliente);

        // Act
        _presenter.Salvar();

        // Assert
        _repositoryMock.Verify(r => r.Incluir(It.IsAny<ClienteModel>()), Times.Once);
        _viewMock.Verify(v => v.LimparFormulario(), Times.Once);
    }

    [Test]
    public void Salvar_ClienteExistente_DeveChamarAlterarNoRepositorio()
    {
        // Arrange
        var clienteExistente = new ClienteModel { Id = 10, Nome = "GUH EDITADO" };
        _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(clienteExistente);

        // Act
        _presenter.Salvar();

        // Assert
        _repositoryMock.Verify(r => r.Alterar(It.IsAny<ClienteModel>()), Times.Once);
        _repositoryMock.Verify(r => r.Incluir(It.IsAny<ClienteModel>()), Times.Never);
    }

    [Test]
    public void Excluir_QuandoUmClienteEstaSelecionado_DeveChamarExcluirNoRepositorio()
    {
        // Arrange
        var cliente = new ClienteModel { Id = 1, Nome = "CLIENTE PARA DELETAR" };
        _viewMock.Setup(v => v.ObterClienteSelecionado()).Returns(cliente);

        // Act
        _presenter.Excluir(cliente.Id); // Certifique-se que o nome do método no seu Presenter é Excluir

        // Assert
        _repositoryMock.Verify(r => r.Excluir(5), Times.Once);
    }
}