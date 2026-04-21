using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Linq;
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
        private bool _carregandoInicial = true;

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
            _view.ClienteSelecionadoNaGrid += (s, e) => CarregarClienteSelecionado();
            _view.BotaoExcluirFoiClicado += (s, e) =>
            {
                var id = _view.ObterIdSelecionadoNaGrid();
                if (id.HasValue) Excluir(id.Value);
                else _view.ExibirMensagem("Selecione um cliente na lista para excluir.");
            };
        }

        public void Inicializar()
        {
            _carregandoInicial = true;
            try
            {
                var clientes = _repository.Listar();
                _view.PreencherGrid(clientes);
                _view.LimparFormulario();
                _carregandoInicial = false;
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao carregar clientes: {ex.Message}");
            }
        }

        public void Salvar()
        {
            // O Presenter é o único que conhece o ClienteModel
            var cliente = new ClienteModel
            {
                Id = _view.ObterId(),
                Nome = _view.ObterNome(),
                CpfCnpj = _view.ObterCpfCnpj(),
                Telefone = _view.ObterTelefone(),
                Email = _view.ObterEmail(),
                DataCadastro = _view.ObterDataCadastro()
            };

            try
            {
                var erros = _validator.Validar(cliente);
                if (erros.Count > 0)
                {
                    _view.ExibirMensagemErro(string.Join("\n", erros));
                    return;
                }

                if (cliente.Id == 0)
                {
                    _repository.Incluir(cliente);
                    _view.ExibirMensagem("Cliente cadastrado com sucesso!");
                } else {
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
                if (!_view.ConfirmarExclusao()) return;

                _repository.Excluir(id);
                _view.ExibirMensagem("Cliente excluído com sucesso!");
                Inicializar();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao excluir: {ex.Message}");
            }
        }
        private void CarregarClienteSelecionado()
        {
            if (_carregandoInicial) return;
            int? id = _view.ObterIdSelecionadoNaGrid();

            if (id.HasValue && id > 0)
            {
                // Use o seu método RetornarPorId (como fizemos no Produto)
                var cliente = _repository.RetornarPorId(id.Value);

                if (cliente != null)
                {
                    _view.PreencherCampos(
                        cliente.Id,
                        cliente.Nome,
                        cliente.CpfCnpj,
                        cliente.Telefone,
                        cliente.Email,
                        cliente.DataCadastro
                    );
                }
            }
        }
    }
}