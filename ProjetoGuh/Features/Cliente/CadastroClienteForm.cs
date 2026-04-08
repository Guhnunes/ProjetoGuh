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
                Nome = txtNome.Text,
                // O .Replace remove a formatação para salvar apenas números
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
        private void txtCpfCnpj_TextChanged(object sender, EventArgs e)
        {
            // 1. Pegamos apenas os números
            string numeros = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray());

            // 2. IMPORTANTE: Se o usuário estiver quase estourando o limite do CPF (11 dígitos),
            // nós removemos a máscara temporariamente para permitir que ele digite o 12º.
            if (numeros.Length == 11)
            {
                // Se a máscara atual for a de CPF, e ele tentar digitar mais, 
                // precisamos expandir para CNPJ
                if (txtCpfCnpj.Mask == "000.000.000-00")
                {
                    txtCpfCnpj.Mask = "00.000.000/0000-00";
                    // Ajusta o cursor para o final, senão ele volta para o início
                    txtCpfCnpj.SelectionStart = txtCpfCnpj.Text.Length;
                }
            }
            else if (numeros.Length < 11)
            {
                // Se cair para 10 ou menos (apagando), volta para a máscara de CPF
                if (txtCpfCnpj.Mask != "000.000.000-00")
                {
                    txtCpfCnpj.Mask = "000.000.000-00";
                    txtCpfCnpj.SelectionStart = txtCpfCnpj.Text.Length;
                }
            }
        }
    }
}