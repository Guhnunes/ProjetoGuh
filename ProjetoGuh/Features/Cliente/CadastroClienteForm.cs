using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Cliente
{
    public partial class CadastroClienteForm : Form, ICadastroClienteView
    {
        public event EventHandler BotaoSalvarFoiClicado;
        public event EventHandler BotaoCancelarFoiClicado;

        private readonly ICadastroClientePresenter _presenter;

        public CadastroClienteForm(ICadastroClientePresenter presenter)
        {
            InitializeComponent();
            _presenter.SetView(this);
            _presenter.Inicializar();
        }

        public ClienteModel ObterDadosDoFormulario()
        {
            return new ClienteModel
            {
                Nome = txtNome.Text,
                CpfCnpj = txtCpfCnpj.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text,
                DataCadastro = dtpDataCadastro.Value
            };
        }

        public void PreencherFormulario(ClienteModel cliente)
        {
            txtNome.Text = cliente.Nome;
            txtCpfCnpj.Text = cliente.CpfCnpj;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;
            dtpDataCadastro.Value = cliente.DataCadastro;
        }

        public void LimparFormulario()
        {
            txtNome.Clear();
            txtCpfCnpj.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            dtpDataCadastro.Value = DateTime.Today;
        }

        public void PreencherGrid(List<ClienteModel> clientes)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientes;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            BotaoSalvarFoiClicado?.Invoke(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            BotaoCancelarFoiClicado?.Invoke(sender, e);
        }
    }
}