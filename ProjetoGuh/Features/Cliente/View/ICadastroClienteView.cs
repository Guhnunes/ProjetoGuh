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

        // Métodos para o Presenter extrair dados brutos
        int ObterId();
        string ObterNome();
        string ObterCpfCnpj();
        string ObterTelefone();
        string ObterEmail();
        DateTime ObterDataCadastro();

        // Métodos para o Presenter injetar dados na tela
        void PreencherCampos(int id, string nome, string cpf, string tel, string email, DateTime data);
        void LimparFormulario();

        // Grid e Mensagens
        void PreencherGrid(object dataSource);
        int? ObterIdSelecionadoNaGrid();
        void ExibirMensagem(string mensagem);
        void ExibirMensagemErro(string mensagemErro);
        bool ConfirmarExclusao();
    }
}
