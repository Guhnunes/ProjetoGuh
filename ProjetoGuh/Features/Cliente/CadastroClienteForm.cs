using Newtonsoft.Json;
using ProjetoGuh.Features.Cliente.Presenter;
using ProjetoGuh.Features.Cliente.View;
using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            CarregarEstados();
            txtCpfCnpj.Leave += txtCpfCnpj_Leave;
            txtCep.TextChanged += txtCep_TextChanged;
            txtCep.Leave += txtCep_Leave;

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
        public string ObterCep() => new string(txtCep.Text.Where(char.IsDigit).ToArray());
        public string ObterLogradouro() => txtLogradouro.Text;
        public string ObterNumero() => txtNumero.Text;
        public string ObterBairro() => txtBairro.Text;
        public string ObterCidade() => txtCidade.Text;
        public string ObterUf() => cmbUf.Text;

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
        public void PreencherCampos(int id, string nome, string cpf, string tel, string email, DateTime data, string cep, string logradouro, string numero, string bairro, string cidade, string uf)
        {
            _clienteIdAtual = id;
            txtNome.Text = nome;
            txtCpfCnpj.Text = cpf;
            txtTelefone.Text = tel;
            txtEmail.Text = email;
            dtpDataCadastro.Value = data;
            txtCep.Text = cep;
            txtLogradouro.Text = logradouro;
            txtNumero.Text = numero;
            txtBairro.Text = bairro;
            txtCidade.Text = cidade;
            cmbUf.Text = uf;

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
            txtCep.Clear();
            txtLogradouro.Clear();
            txtNumero.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            cmbUf.SelectedIndex = -1;
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
        private void txtCep_TextChanged(object sender, EventArgs e)
        {
            // 1. Remove o evento para evitar recursão
            txtCep.TextChanged -= txtCep_TextChanged;

            // 2. Mantém apenas números
            string numeros = new string(txtCep.Text.Where(char.IsDigit).ToArray());

            // 3. Limita a 8 dígitos
            if (numeros.Length > 8)
                numeros = numeros.Substring(0, 8);

            string textoFormatado = numeros;

            // 4. Aplica a máscara 00000-000
            if (numeros.Length > 5)
            {
                textoFormatado = textoFormatado.Insert(5, "-");
            }

            // 5. Atualiza o texto e posiciona o cursor no final
            txtCep.Text = textoFormatado;
            txtCep.SelectionStart = txtCep.Text.Length;

            // 6. Reassina o evento
            txtCep.TextChanged += txtCep_TextChanged;
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
        private void CarregarEstados()
        {
            string[] ufs = { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
                     "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                     "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

            cmbUf.Items.Clear();
            cmbUf.Items.AddRange(ufs);
            cmbUf.SelectedIndex = -1; // Deixa vazio por padrão
        }
        private async void txtCep_Leave(object sender, EventArgs e)
        {
            string cep = ObterCep();
            if (cep.Length != 8) return;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        dynamic dados = JsonConvert.DeserializeObject(content);
                        if (dados.erro == "true")
                        {
                            ExibirMensagemErro("CEP não encontrado.");
                            return;
                        }
                        txtLogradouro.Text = (string)dados.logradouro;
                        txtBairro.Text = (string)dados.bairro;
                        txtCidade.Text = (string)dados.localidade;
                        cmbUf.Text = (string)dados.uf;

                        txtNumero.Focus();
                    }
                }
            }
            catch (Exception)
            {
                ExibirMensagemErro("Não foi possível buscar o CEP. Verifique sua conexão.");
            }
        }
        private async Task<bool> ValidarCpfApi(string cpfcnpj)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var token = "25850|kC7oPY3eQGpru2haN0tLi7KH3xNeID4h";
                    var response = await client.GetAsync($"https://api.invertexto.com/v1/validator?token={token}&value={cpfcnpj}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResposta = await response.Content.ReadAsStringAsync();
                        dynamic resultado = JsonConvert.DeserializeObject(jsonResposta);
                        if (resultado.valid == true)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch
            {
                return false; //Se a api não estiver funcionando não vai fazer nada
            }
        }
        private async void txtCpfCnpj_Leave(object sender, EventArgs e)
        {
            string documento = ObterCpfCnpj(); // Pega apenas os números

            // Só dispara se for um CPF completo (11 dígitos)
            if (documento.Length == 11 || documento.Length == 14)
            {
                // Opcional: Mostrar um aviso de "Validando..."
                bool eValido = await ValidarCpfApi(documento);

                if (!eValido)
                {
                    ExibirMensagemErro("O CPF/CNPJ informado não é valido.");
                    txtCpfCnpj.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtCpfCnpj.ForeColor = System.Drawing.Color.Black;
                }
            }
        }
    }
}