using Moq;
using NUnit.Framework;
using ProjetoGuh.Features.Cliente;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoGuh.Testes.Features.Cliente
{
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
            _presenter = new CadastroClientePresenter(_repositoryMock.Object);
            _presenter.SetView(_viewMock.Object);
        }
        [Test]
        public void TesteDeSanidade()
        {
            Assert.Pass("O motor de testes está funcionando!");
        }
        /*
        [Test]
        public void Salvar_ClienteNovoValido_DeveChamarIncluirNoRepositorio()
        {
            // Arrange: Simulamos um cliente com ID 0 (Novo)
            var cliente = new ClienteModel { Id = 0, Nome = "GUH NUNES", CpfCnpj = "12345678901" };
            _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(cliente);

            // Act: Disparamos o método que você quer testar
            _presenter.Salvar();

            // Assert: Verificamos se o repositório foi avisado para INCLUIR
            _repositoryMock.Verify(r => r.Incluir(It.IsAny<ClienteModel>()), Times.Once);
            _viewMock.Verify(v => v.LimparFormulario(), Times.Once);
        }*/
        /*
        [Test]
        public void Salvar_ClienteExistente_DeveChamarAlterarNoRepositorio()
        {
            // Arrange: Simulamos um cliente com ID > 0 (Existente/Edição)
            var clienteExistente = new ClienteModel { Id = 15, Nome = "GUH EDITADO" };
            _viewMock.Setup(v => v.ObterDadosDoFormulario()).Returns(clienteExistente);

            // Act
            _presenter.Salvar();

            // Assert: Verificamos se o repositório foi avisado para ALTERAR
            _repositoryMock.Verify(r => r.Alterar(It.IsAny<ClienteModel>()), Times.Once);
            _repositoryMock.Verify(r => r.Incluir(It.IsAny<ClienteModel>()), Times.Never);
        }*/
        /*
        [Test]
        public void Excluir_QuandoClienteSelecionado_DeveChamarExcluirNoRepositorio()
        {
            // Arrange: Criamos o cliente que "estaria" selecionado na Grid
            var cliente = new ClienteModel { Id = 5, Nome = "TESTE EXCLUSAO" };

            // Configuramos o Mock para dizer: "Quando o Presenter perguntar quem está selecionado, responda este cliente"
            _viewMock.Setup(v => v.ObterClienteSelecionado()).Returns(cliente);

            // Act: Chamamos o método (agora sem erro de argumento!)
            _presenter.Excluir(cliente.Id);

            // Assert: Verificamos se o repositório recebeu o ID 5 corretamente
            _repositoryMock.Verify(r => r.Excluir(5), Times.Once);
            _viewMock.Verify(v => v.LimparFormulario(), Times.Once);
        }*/
    }
}