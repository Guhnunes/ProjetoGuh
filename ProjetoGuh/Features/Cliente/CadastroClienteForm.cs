using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProjetoGuh.Features.Cliente.View;
using ProjetoGuh.Features.Cliente.Presenter;

namespace ProjetoGuh.Features.Cliente
{
    public partial class CadastroClienteForm : Form, ICadastroClienteView
    {
        public event EventHandler BotaoSalvarFoiClicado;
        public event EventHandler BotaoCancelarFoiClicado;
        public event EventHandler BotaoExcluirFoiClicado;
        public event EventHandler ClienteSelecionadoNaGrid;

        private int _clienteIdAtual = 0;

        public CadastroClienteForm(ICadastroClientePresenter presenter)
        {
            InitializeComponent();

            dtpDataCadastro.Enabled = false;

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            dataGridView1.CellClick += (s, e) => {
                if (e.RowIndex >= 0) // Garante que não clicou no cabeçalho
                    ClienteSelecionadoNaGrid?.Invoke(this, EventArgs.Empty);
            };

            // Configurações de Máscara
            txtCpfCnpj.MaxLength = 18;
            txtCpfCnpj.TextChanged += txtCpfCnpj_TextChanged;
            txtTelefone.MaxLength = 15;
            txtTelefone.TextChanged += txtTelefone_TextChanged;

            // Injeção e Inicialização
            presenter.SetView(this);
            this.Load += (s, e) => presenter.Inicializar();

        }

        // --- MÉTODOS DE EXTRAÇÃO (DADOS BRUTOS) ---
        public int ObterId() => _clienteIdAtual;
        public string ObterNome() => txtNome.Text;
        public string ObterCpfCnpj() => new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray()).Trim();
        public string ObterTelefone() => new string(txtTelefone.Text.Where(char.IsDigit).ToArray());
        public string ObterEmail() => txtEmail.Text;
        public DateTime ObterDataCadastro() => DateTime.Now;

        public int? ObterIdSelecionadoNaGrid()
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Certifique-se que o nome da coluna de ID na Grid é "Id" ou use o índice [0]
                return Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            }
            return null;
        }

        // --- MÉTODOS DE INJEÇÃO (VINDO DO PRESENTER) ---
        public void PreencherCampos(int id, string nome, string cpf, string tel, string email, DateTime data)
        {
            _clienteIdAtual = id;
            txtNome.Text = nome;
            txtCpfCnpj.Text = cpf;
            txtTelefone.Text = tel;
            txtEmail.Text = email;
            dtpDataCadastro.Value = data;

            if (data < dtpDataCadastro.MinDate || data == DateTime.MinValue)
            {
                dtpDataCadastro.Value = DateTime.Now;
            }
            else
            {
                dtpDataCadastro.Value = data;
            }
        }

        public void PreencherGrid(object dataSource)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dataSource;
            dataGridView1.ClearSelection();

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "CÓDIGO";
                dataGridView1.Columns["Nome"].HeaderText = "NOME";
            }
        }

        public void LimparFormulario()
        {
            _clienteIdAtual = 0;
            txtNome.Clear();
            txtCpfCnpj.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            dtpDataCadastro.Value = DateTime.Today;
        }

        // --- EVENTOS DE INTERAÇÃO ---
        private void btnSalvar_Click(object sender, EventArgs e) => BotaoSalvarFoiClicado?.Invoke(sender, e);
        private void btnCancelar_Click(object sender, EventArgs e) => BotaoCancelarFoiClicado?.Invoke(sender, e);
        private void btnExcluir_Click(object sender, EventArgs e) => BotaoExcluirFoiClicado?.Invoke(sender, e);

        // --- MENSAGENS E DIÁLOGOS ---
        public void ExibirMensagem(string mensagem) => ControleDeMensagens.Informar(mensagem);
        public void ExibirMensagemErro(string mensagemErro) => ControleDeMensagens.Avisar(mensagemErro);
        public bool ConfirmarExclusao() => ControleDeMensagens.Perguntar("Deseja realmente excluir este cliente?");

        private void txtCpfCnpj_TextChanged(object sender, EventArgs e)
        {
            txtCpfCnpj.TextChanged -= txtCpfCnpj_TextChanged;
            string numeros = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray());
            if (numeros.Length > 14) numeros = numeros.Substring(0, 14);
            string textoFormatado = numeros;

            if (numeros.Length <= 11) // Formato CPF
            {
                if (numeros.Length >= 4) textoFormatado = textoFormatado.Insert(3, ".");
                if (numeros.Length >= 7) textoFormatado = textoFormatado.Insert(7, ".");
                if (numeros.Length >= 10) textoFormatado = textoFormatado.Insert(11, "-");
            }
            else // Formato CNPJ
            {
                if (numeros.Length >= 3) textoFormatado = textoFormatado.Insert(2, ".");
                if (numeros.Length >= 6) textoFormatado = textoFormatado.Insert(6, ".");
                if (numeros.Length >= 9) textoFormatado = textoFormatado.Insert(10, "/");
                if (numeros.Length >= 13) textoFormatado = textoFormatado.Insert(15, "-");
            }

            // 4. Atualiza o campo e mantém o cursor no final
            txtCpfCnpj.Text = textoFormatado;
            txtCpfCnpj.SelectionStart = txtCpfCnpj.Text.Length;

            // 5. Reatribui o evento
            txtCpfCnpj.TextChanged += txtCpfCnpj_TextChanged;
        }
        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {
            // 1. Remove o evento para evitar que o código entre em loop ao alterar o texto
            txtTelefone.TextChanged -= txtTelefone_TextChanged;

            // 2. Pega apenas os números
            string numeros = new string(txtTelefone.Text.Where(char.IsDigit).ToArray());

            // Limita a 11 dígitos (máximo para celular com DDD)
            if (numeros.Length > 11) numeros = numeros.Substring(0, 11);

            string textoFormatado = numeros;

            // 3. Aplica a formatação dinâmica
            if (numeros.Length > 0)
            {
                // Adiciona os parênteses do DDD: (11
                textoFormatado = textoFormatado.Insert(0, "(");

                if (numeros.Length >= 3)
                    textoFormatado = textoFormatado.Insert(3, ") "); // (11) 

                if (numeros.Length <= 10) // Telefone Fixo: (11) 4444-4444
                {
                    if (numeros.Length >= 7)
                        textoFormatado = textoFormatado.Insert(9, "-");
                }
                else // Celular: (11) 99999-9999
                {
                    if (numeros.Length >= 8)
                        textoFormatado = textoFormatado.Insert(10, "-");
                }
            }

            // 4. Atualiza o campo e joga o cursor para o final
            txtTelefone.Text = textoFormatado;
            txtTelefone.SelectionStart = txtTelefone.Text.Length;

            // 5. Reatribui o evento
            txtTelefone.TextChanged += txtTelefone_TextChanged;
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