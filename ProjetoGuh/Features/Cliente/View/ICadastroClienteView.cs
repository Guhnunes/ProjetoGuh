using System;
using System.Collections.Generic;
using ProjetoGuh.Features.Cliente.Model;

namespace ProjetoGuh.Features.Cliente.View
{
    public interface ICadastroClienteView
    {
        event EventHandler BotaoSalvarFoiClicado;
        event EventHandler BotaoCancelarFoiClicado;
        event EventHandler BotaoExcluirFoiClicado;
        event EventHandler ClienteSelecionadoNaGrid;
        //event EventHandler Load;

        // Métodos para o Presenter extrair dados brutos
        int ObterId();
        string ObterNome();
        string ObterCpfCnpj();
        string ObterTelefone();
        string ObterEmail();
        DateTime ObterDataCadastro();
        string ObterCep();
        string ObterLogradouro();
        string ObterNumero();
        string ObterBairro();
        string ObterCidade();
        string ObterUf();

        // Métodos para o Presenter injetar dados na tela
        void PreencherCampos(int id, string nome, string cpf, string tel, string email, DateTime data, string cep, string logradouro, string numero, string bairro, string cidade, string uf);
        void LimparFormulario();

        // Grid e Mensagens
        void PreencherGrid(object dataSource);
        int? ObterIdSelecionadoNaGrid();
        void ExibirMensagem(string mensagem);
        void ExibirMensagemErro(string mensagemErro);
        bool ConfirmarExclusao();
    }
}
