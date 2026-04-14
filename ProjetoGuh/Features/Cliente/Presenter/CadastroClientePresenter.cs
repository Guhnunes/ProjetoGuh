using ProjetoGuh.Features.Infraestrutura;
using System;

namespace ProjetoGuh.Features.Cliente
{
    public class CadastroClientePresenter : ICadastroClientePresenter
    {
        private ICadastroClienteView _view;
        private readonly IClienteRepository _repository;
        private readonly ClienteModelValidator _validator;

        public CadastroClientePresenter(IClienteRepository repository)
        {
            _repository = repository;
            _validator = new ClienteModelValidator();
        }

        public void SetView(ICadastroClienteView view)
        {
            _view = view;
            _view.BotaoSalvarFoiClicado += (s, e) => Salvar();
            _view.BotaoCancelarFoiClicado += (s, e) => _view.LimparFormulario();
            _view.BotaoExcluirFoiClicado += (s, e) =>
            {
                var clienteSelecionado = _view.ObterClienteSelecionado();
                if (clienteSelecionado != null)
                {
                    Excluir(clienteSelecionado.Id);
                }
                else
                {
                    ControleDeMensagens.Avisar("Por favor, selecione um cliente na lista para excluir.");
                }
            };
        }

        public void Inicializar()
        {
            try
            {
                var clientes = _repository.Listar();
                _view.PreencherGrid(clientes);
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao carregar clientes: {ex.Message}");
            }
        }

        public void Salvar()
        {
            try
            {
                var cliente = _view.ObterDadosDoFormulario();
                var erros = _validator.Validar(cliente);

                if (erros.Count > 0)
                {
                    string mensagemErro = string.Join("\n", erros);
                    ControleDeMensagens.Avisar(mensagemErro);
                    return;
                }

                if (cliente.Id == 0)
                {
                    _repository.Incluir(cliente);
                    ControleDeMensagens.Informar("Cliente cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(cliente);
                    ControleDeMensagens.Informar("Cliente atualizado com sucesso!");
                }

                _view.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao salvar: {ex.Message}");
            }
        }

        public void Excluir(int id)
        {
            try
            {
                if (!ControleDeMensagens.Perguntar("Deseja realmente excluir este cliente?"))
                    return;

                _repository.Excluir(id);
                ControleDeMensagens.Informar("Cliente excluído com sucesso!");
                Inicializar();
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao excluir cliente: {ex.Message}");
            }
        }
    }
}