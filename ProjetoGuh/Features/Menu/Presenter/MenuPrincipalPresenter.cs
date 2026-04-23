using Autofac;
using ProjetoGuh.Features.Cliente;
using ProjetoGuh.Features.Cliente.Presenter;
using ProjetoGuh.Features.Produto.Presenter;
using ProjetoGuh.Features.Menu.View;
using ProjetoGuh.Features.Produto;
using ProjetoGuh.Features.Venda;
using ProjetoGuh.Features.Venda.Presenter;
using System;


namespace ProjetoGuh.Features.Menu.Presenter
{
    public class MenuPrincipalPresenter
    {
        private readonly IMenuPrincipalView _view;
        private readonly ILifetimeScope _scope;

        public MenuPrincipalPresenter(IMenuPrincipalView view, ILifetimeScope scope)
        {
            _view = view;
            _scope = scope;

            // Assina os eventos da View
            _view.AbrirClienteClick += AbrirCliente;
            _view.AbrirProdutoClick += AbrirProduto;
            _view.AbrirVendaClick += AbrirVenda;
            _view.AbrirConsultaVendaClick += (s, e) => AbrirConsultaVendas();
        }
        private void AbrirConsultaVendas()
        {
            var form = _scope.Resolve<VendaConsultaForm>();
            _view.AdicionarAba("Consulta de Vendas", form);
        }
        private void AbrirVenda(object sender, EventArgs e)
        {
            var frm = _scope.Resolve<PdvForm>();
            var presenter = _scope.Resolve<ICadastroVendaPresenter>();
            presenter.SetView(frm);
            _view.AdicionarAba("Venda (PDV)", frm);
        }

        private void AbrirCliente(object sender, EventArgs e)
        {
            var frm = _scope.Resolve<CadastroClienteForm>();
            var presenter = _scope.Resolve<ICadastroClientePresenter>();
            presenter.SetView(frm);
            _view.AdicionarAba("Cadastro de Clientes", frm);
        }
        private void AbrirProduto(object sender, EventArgs e)
        {
            var frm = _scope.Resolve<CadastroProdutoForm>();
            var presenter = _scope.Resolve<ICadastroProdutoPresenter>();
            presenter.SetView(frm);
            _view.AdicionarAba("Cadastro de Produtos", frm);
        }
    }
}