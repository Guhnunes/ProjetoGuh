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
        public event EventHandler BotaoExcluirFoiClicado;

        private readonly ICadastroClientePresenter _presenter;

        public CadastroClienteForm(ICadastroClientePresenter presenter)
        {
            InitializeComponent();
            txtCpfCnpj.Mask = "";
            txtCpfCnpj.MaxLength = 18;
            txtCpfCnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCpfCnpj.TextChanged += txtCpfCnpj_TextChanged;

            _presenter = presenter;
            _presenter.SetView(this);

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            this.Load += (s, e) => _presenter.Inicializar(); //Chama o método da CadastroClientePresenter Inicializar() e dentro tem o Listar() do repository.
        }

        public ClienteModel ObterDadosDoFormulario()
        {
            return new ClienteModel
            {
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
            var resultado = ControleDeMensagens.Perguntar("Tem certeza que deseja excluir este cliente?");
            BotaoExcluirFoiClicado?.Invoke(sender, e);
        }

        private void txtCpfCnpj_TextChanged(object sender, EventArgs e)
        {
            // Remove tudo que não for número
            string numeros = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray());

            // Guarda a posição do cursor
            int posicaoCursor = txtCpfCnpj.SelectionStart;
            int totalCaracteresAntes = txtCpfCnpj.Text.Length;

            if (numeros.Length <= 11)
            {
                if (txtCpfCnpj.Mask != "000.000.000-00")
                {
                    txtCpfCnpj.Mask = "000.000.000-00";
                    txtCpfCnpj.Text = numeros; // Reatribui para encaixar na máscara
                }
            }
            else
            {
                if (txtCpfCnpj.Mask != "00.000.000/0000-00")
                {
                    txtCpfCnpj.Mask = "00.000.000/0000-00";
                    txtCpfCnpj.Text = numeros; // Reatribui para encaixar na máscara
                }
            }

            // Ajusta o cursor para ele não pular para o início do campo
            int totalCaracteresDepois = txtCpfCnpj.Text.Length;
            posicaoCursor += (totalCaracteresDepois - totalCaracteresAntes);

            if (posicaoCursor >= 0)
                txtCpfCnpj.SelectionStart = posicaoCursor;
        }
        private void txtCpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            string numeros = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray());

            // Se já tem 11 dígitos e o usuário digitar outro número, removemos a máscara 
            // temporariamente para o TextChanged poder capturar o 12º dígito.
            if (numeros.Length == 11 && char.IsDigit((char)e.KeyCode))
            {
                txtCpfCnpj.Mask = "";
                txtCpfCnpj.Text = numeros;
                txtCpfCnpj.SelectionStart = txtCpfCnpj.Text.Length;
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
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Verifica se estamos na coluna de CPF/CNPJ pelo DataPropertyName
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CpfCnpj" && e.Value != null)
            {
                // 2. Limpa o valor para garantir que temos apenas números
                string valor = new string(e.Value.ToString().Where(char.IsDigit).ToArray());

                if (string.IsNullOrEmpty(valor)) return;

                // 3. Aplica a máscara baseada na quantidade de números
                if (valor.Length == 11) // CPF
                {
                    e.Value = double.Parse(valor).ToString(@"000\.000\.000-00");
                    e.FormattingApplied = true;
                }
                else if (valor.Length == 14) // CNPJ
                {
                    e.Value = double.Parse(valor).ToString(@"00\.000\.000\/0000-00");
                    e.FormattingApplied = true;
                }
            }
        }
    }
}