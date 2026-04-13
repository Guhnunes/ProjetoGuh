using Autofac;
using ProjetoGuh.Features.Cliente;
using System;
using System.Windows.Forms;

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
    }
}