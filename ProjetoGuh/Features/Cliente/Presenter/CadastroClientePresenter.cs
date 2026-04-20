using ProjetoGuh.Features.Infraestrutura;
using System;
using ProjetoGuh.Features.Cliente.View;
using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Cliente.Repository;

namespace ProjetoGuh.Features.Cliente.Presenter
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
                    _view.ExibirMensagem("Por favor, selecione um cliente na lista para excluir.");
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
                _view.ExibirMensagemErro($"Erro ao carregar clientes: {ex.Message}");
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
                    _view.ExibirMensagemErro(mensagemErro);
                    return;
                }

                if (cliente.Id == 0)
                {
                    _repository.Incluir(cliente);
                    _view.ExibirMensagem("Cliente cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(cliente);
                    _view.ExibirMensagem("Cliente atualizado com sucesso!");
                }

                _view.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao salvar: {ex.Message}");
            }
        }

        public void Excluir(int id)
        {
            try
            {
                if (!_view.ConfirmarExclusao())
                    return;

                _repository.Excluir(id);
                _view.ExibirMensagem("Cliente excluído com sucesso!");
                Inicializar();
            }
            catch (Exception ex)
            {
               _view.ExibirMensagemErro($"Erro ao excluir cliente: {ex.Message}");
            }
        }
    }
}