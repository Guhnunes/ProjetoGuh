using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Cliente.Presenter;
using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Cliente.View;
using System;

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
        _viewMock.Setup(v => v.ObterId()).Returns(0); // 0 para simular um novo cadastro (Incluir)
        _viewMock.Setup(v => v.ObterNome()).Returns("Caio");
        _viewMock.Setup(v => v.ObterCpfCnpj()).Returns("12345678901");
        _viewMock.Setup(v => v.ObterEmail()).Returns("caio@example.com");
        _viewMock.Setup(v => v.ObterTelefone()).Returns("1234567890");
        _viewMock.Setup(v => v.ObterDataCadastro()).Returns(DateTime.Now);
        _presenter.Salvar();
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ClienteModel>()), Times.Once);
        _viewMock.Verify(v => v.ExibirMensagem("Cliente cadastrado com sucesso!"), Times.Once);
    }

    [Test]
    public void Salvar_ClienteSemNome_NaoDeveIncluirNoBanco()
    {
        _viewMock.Setup(v => v.ObterId()).Returns(0); // 0 para simular um novo cadastro (Incluir)
        _viewMock.Setup(v => v.ObterNome()).Returns("");
        _viewMock.Setup(v => v.ObterCpfCnpj()).Returns("12345678901");
        _viewMock.Setup(v => v.ObterEmail()).Returns("teste@example.com");
        _viewMock.Setup(v => v.ObterTelefone()).Returns("1234567890");
        _viewMock.Setup(v => v.ObterDataCadastro()).Returns(DateTime.Now);
        _presenter.Salvar();
        _repositoryMock.Verify(d => d.Incluir(It.IsAny<ClienteModel>()), Times.Never);
        _viewMock.Verify(v => v.ExibirMensagemErro("Nome é obrigatório."), Times.AtLeastOnce);
    }

    [Test]
    public void Excluir_QuandoConfirmado_DeveExcluirNoBanco()
    {
        var cliente = new ClienteModel { Id = 1, Nome = "Teste" };
        _viewMock.Setup(v => v.ConfirmarExclusao()).Returns(true);
        _presenter.Excluir(cliente.Id);
        _repositoryMock.Verify(d => d.Excluir(cliente.Id), Times.Once);
    }

    [Test]
    public void Excluir_QuandoNaoConfirmado_NaoDeveExcluirNoBanco()
    {
        var cliente = new ClienteModel { Id = 1, Nome = "Teste" };

        _viewMock.Setup(v => v.ConfirmarExclusao()).Returns(false);
        _presenter.Excluir(cliente.Id);
        _repositoryMock.Verify(d => d.Excluir(It.IsAny<int>()), Times.Never);
    }
}