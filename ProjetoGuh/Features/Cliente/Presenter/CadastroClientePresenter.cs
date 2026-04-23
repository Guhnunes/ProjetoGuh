using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Cliente.Repository;
using ProjetoGuh.Features.Cliente.View;
using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Cliente.Presenter
{
    public class CadastroClientePresenter : BasePresenter<ICadastroClienteView>, ICadastroClientePresenter
    {
        private readonly IClienteRepository _repository;
        private readonly ClienteModelValidator _validator;
        private bool _carregandoInicial = false;

        public CadastroClientePresenter(IClienteRepository repository)
            : base(null)
        {
            _repository = repository;
            _validator = new ClienteModelValidator();
        }

        public override void SetView(ICadastroClienteView view)
        {
            base.SetView(view);
            view.BotaoSalvarFoiClicado += (s, e) => Salvar();
            view.BotaoCancelarFoiClicado += (s, e) => view.LimparFormulario();
            view.ClienteSelecionadoNaGrid += (s, e) => CarregarClienteSelecionado();
            view.BotaoExcluirFoiClicado += (s, e) =>
            {
                var id = view.ObterIdSelecionadoNaGrid();
                if (id.HasValue) Excluir(id.Value);
                else view.ExibirMensagem("Selecione um cliente na lista para excluir.");
            };
            Inicializar();
        }

        public void Inicializar()
        {
            _carregandoInicial = true;
            try
            {
                var clientes = _repository.Listar();
                View.PreencherGrid(clientes);
                View.LimparFormulario();
                _carregandoInicial = false;
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao carregar clientes: {ex.Message}");
            }
        }

        public void Salvar()
        {
            // O Presenter é o único que conhece o ClienteModel
            var cliente = new ClienteModel
            {
                Id = View.ObterId(),
                Nome = View.ObterNome(),
                CpfCnpj = View.ObterCpfCnpj(),
                Telefone = View.ObterTelefone(),
                Email = View.ObterEmail(),
                DataCadastro = View.ObterDataCadastro(),
                Cep = View.ObterCep(),
                Logradouro = View.ObterLogradouro(),
                Numero = View.ObterNumero(),
                Bairro = View.ObterBairro(),
                Cidade = View.ObterCidade(),
                Uf = View.ObterUf()
            };

            try
            {
                var erros = _validator.Validar(cliente);
                if (erros.Count > 0)
                {
                    View.ExibirMensagemErro(string.Join("\n", erros));
                    return;
                }

                if (cliente.Id == 0)
                {
                    _repository.Incluir(cliente);
                    View.ExibirMensagem("Cliente cadastrado com sucesso!");
                } else {
                    _repository.Alterar(cliente);
                    View.ExibirMensagem("Cliente atualizado com sucesso!");
                }

                View.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao salvar: {ex.Message}");
            }
        }

        public void Excluir(int id)
        {
            try
            {
                if (!View.ConfirmarExclusao()) return;

                _repository.Excluir(id);
                View.ExibirMensagem("Cliente excluído com sucesso!");
                Inicializar();
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao excluir: {ex.Message}");
            }
        }
        private void CarregarClienteSelecionado()
        {
            if (_carregandoInicial) return;
            int? id = View.ObterIdSelecionadoNaGrid();

            if (id.HasValue && id > 0)
            {
                // Use o seu método RetornarPorId (como fizemos no Produto)
                var cliente = _repository.RetornarPorId(id.Value);

                if (cliente != null)
                {
                    View.PreencherCampos(
                        cliente.Id,
                        cliente.Nome,
                        cliente.CpfCnpj,
                        cliente.Telefone,
                        cliente.Email,
                        cliente.DataCadastro,
                        cliente.Cep,
                        cliente.Logradouro,
                        cliente.Numero,
                        cliente.Bairro,
                        cliente.Cidade,
                        cliente.Uf
                    );
                }
            }
        }
    }
}