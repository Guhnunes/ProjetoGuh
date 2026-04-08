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
                // 1. Coleta os dados que estão na View (Form)
                var cliente = _view.ObterDadosDoFormulario();

                // 2. Validar com a sua classe ClienteModelValidator
                // (Como você não está usando FluentValidation, o método chama-se 'Validar')
                var erros = _validator.Validar(cliente);

                if (erros.Count > 0)
                {
                    // Se houver erros, junta tudo em uma string e avisa o usuário
                    string mensagemErro = string.Join("\n", erros);
                    ControleDeMensagens.Avisar(mensagemErro);
                    return; // Para a execução aqui
                }

                // 3. Chamar Incluir ou Alterar no Repository
                if (cliente.Id == 0)
                {
                    _repository.Incluir(cliente);
                    // 4. Mostrar feedback de sucesso
                    ControleDeMensagens.Informar("Cliente cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(cliente);
                    // 4. Mostrar feedback de sucesso
                    ControleDeMensagens.Informar("Cliente atualizado com sucesso!");
                }

                // Limpa a tela e atualiza a lista após salvar
                _view.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro de banco de dados, por exemplo
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