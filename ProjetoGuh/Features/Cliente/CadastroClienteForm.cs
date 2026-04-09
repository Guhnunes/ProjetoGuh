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
        private int _clienteIdAtual = 0; //Tive que fazer isso pra pegar o Id no evento de click pro Alterar()

        public CadastroClienteForm(ICadastroClientePresenter presenter)
        {
            InitializeComponent();
            txtCpfCnpj.MaxLength = 18;
            txtCpfCnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCpfCnpj.TextChanged += txtCpfCnpj_TextChanged;
            txtTelefone.MaxLength = 15;
            txtTelefone.TextChanged += txtTelefone_TextChanged;

            _presenter = presenter;
            _presenter.SetView(this);

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            this.Load += (s, e) => _presenter.Inicializar(); //Chama o método da CadastroClientePresenter Inicializar() e dentro tem o Listar() do repository.
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha (e não no cabeçalho/header)
            if (e.RowIndex >= 0)
            {
                // Usa o método que você já tem para pegar o cliente da linha selecionada
                var clienteSelecionado = ObterClienteSelecionado();

                if (clienteSelecionado != null)
                {
                    // Usa o método que você já tem para jogar os dados nos campos
                    PreencherFormulario(clienteSelecionado);
                }
            }
        }
        public ClienteModel ObterDadosDoFormulario()
        {
            return new ClienteModel
            {
                Id = _clienteIdAtual,
                Nome = txtNome.Text,
                CpfCnpj = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray()).Trim(),
                Telefone = new string(txtTelefone.Text.Where(char.IsDigit).ToArray()),
                Email = txtEmail.Text,
                DataCadastro = dtpDataCadastro.Value
            };
        }
        public void PreencherFormulario(ClienteModel cliente)
        {
            _clienteIdAtual = cliente.Id;
            txtNome.Text = cliente.Nome;
            txtCpfCnpj.Text = cliente.CpfCnpj;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;
            dtpDataCadastro.Value = cliente.DataCadastro;
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

        public void PreencherGrid(List<ClienteModel> clientes)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientes;
            dataGridView1.Columns["Id"].HeaderText = "CÓDIGO";
            dataGridView1.Columns["Nome"].HeaderText = "NOME";
            dataGridView1.Columns["CpfCnpj"].HeaderText = "CPF/CNPJ";
            dataGridView1.Columns["Telefone"].HeaderText = "CONTATO";
            dataGridView1.Columns["Email"].HeaderText = "E-MAIL";
            dataGridView1.Columns["DataCadastro"].HeaderText = "DATA DE CADASTRO";
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
            // 1. Remove o evento para evitar recursão
            txtCpfCnpj.TextChanged -= txtCpfCnpj_TextChanged;

            // 2. Pega apenas os números
            string numeros = new string(txtCpfCnpj.Text.Where(char.IsDigit).ToArray());

            // Limita a 14 dígitos (tamanho máximo de um CNPJ)
            if (numeros.Length > 14) numeros = numeros.Substring(0, 14);

            // 3. Aplica a formatação visual manualmente
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