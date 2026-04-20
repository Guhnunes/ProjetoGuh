using Autofac;
using ProjetoGuh.Features.Cliente;
using ProjetoGuh.Features.Produto;
using ProjetoGuh.Features.Venda;
using System;


namespace ProjetoGuh.Features.Menu
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
            // Resolve o formulário pelo Autofac (garante que o VendaConsultaPresenter seja injetado)
            var form = _scope.Resolve<VendaConsultaForm>();
            _view.AdicionarAba("Consulta de Vendas", form);
        }
        private void AbrirVenda(object sender, EventArgs e)
        {
            var frm = _scope.Resolve<PdvForm>();

            _view.AdicionarAba("Venda (PDV)", frm);
        }

        private void AbrirCliente(object sender, EventArgs e)
        {
            // Resolvemos o formulário diretamente do escopo principal 
            // ou criamos um escopo que não seja descartado imediatamente.
            // Para WinForms simples, podemos resolver direto do _scope:

            var frm = _scope.Resolve<CadastroClienteForm>();

            // Em vez de ShowDialog, mandamos a View encaixar o form na aba
            _view.AdicionarAba("Cadastro de Clientes", frm);
        }
        private void AbrirProduto(object sender, EventArgs e)
        {
            var frm = _scope.Resolve<CadastroProdutoForm>();
            _view.AdicionarAba("Cadastro de Produtos", frm);
        }
    }
}