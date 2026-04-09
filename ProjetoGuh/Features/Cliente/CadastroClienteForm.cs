using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Cliente
{
    public partial class CadastroClienteForm : Form, ICadastroClienteView
    {
        public event EventHandler BotaoSalvarFoiClicado;
        public event EventHandler BotaoCancelarFoiClicado;
        // 1. Novo evento para exclusão
        public event EventHandler BotaoExcluirFoiClicado;

        private readonly ICadastroClientePresenter _presenter;

        public CadastroClienteForm(ICadastroClientePresenter presenter)
        {
            InitializeComponent();
            txtCpfCnpj.Mask = "";
            txtCpfCnpj.MaxLength = 14;
            txtCpfCnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCpfCnpj.TextChanged += txtCpfCnpj_TextChanged;

            _presenter = presenter;
            _presenter.SetView(this);
        }

        public ClienteModel ObterDadosDoFormulario()
        {
            return new ClienteModel
            {
                // Certifique-se de que seu Model tenha o ID para que o Presenter saiba quem excluir
                Nome = txtNome.Text,
                CpfCnpj = txtCpfCnpj.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim(),
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

        // 2. Método disparado pelo clique do botão físico btnExcluir
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                BotaoExcluirFoiClicado?.Invoke(sender, e);
            }
        }

        private void txtCpfCnpj_TextChanged(object sender, EventArgs e)
        {
            string numeros = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray());

            if (numeros.Length == 11)
            {
                if (txtCpfCnpj.Mask == "000.000.000-00")
                {
                    txtCpfCnpj.Mask = "00.000.000/0000-00";
                    txtCpfCnpj.SelectionStart = txtCpfCnpj.Text.Length;
                }
            }
            else if (numeros.Length < 11)
            {
                if (txtCpfCnpj.Mask != "000.000.000-00")
                {
                    txtCpfCnpj.Mask = "000.000.000-00";
                    txtCpfCnpj.SelectionStart = txtCpfCnpj.Text.Length;
                }
            }
        }
        public ClienteModel ObterClienteSelecionado()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Retorna o objeto vinculado à linha selecionada no Grid
                return dataGridView1.SelectedRows[0].DataBoundItem as ClienteModel;
            }

            return null;
        }
    }
}