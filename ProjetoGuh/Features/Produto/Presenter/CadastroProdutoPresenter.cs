using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Repository;
using System;
using System.Linq;

namespace ProjetoGuh.Features.Produto.Presenter
{
    public class CadastroProdutoPresenter : BasePresenter<ICadastroProdutoView>, ICadastroProdutoPresenter
    {
        private readonly IProdutoRepository _repository;
        private readonly ProdutoModelValidator _validator;

        public CadastroProdutoPresenter(IProdutoRepository repository)
            : base(null)
        {
            _repository = repository;
            _validator = new ProdutoModelValidator();
        }

        public override void SetView(ICadastroProdutoView view)
        {
            base.SetView(view);
            view.BotaoSalvarFoiClicado += (s, e) => Salvar();
            view.BotaoCancelarFoiClicado += (s, e) => view.LimparFormulario();
            view.BotaoExcluirFoiClicado += (s, e) =>
            {
                // Buscamos apenas o ID da View, não o objeto Model
                var id = view.ObterIdSelecionadoNaGrid();

                if (id.HasValue)
                {
                    if (view.ConfirmarExclusao()) // A View decide como perguntar (MessageBox)
                    {
                        Excluir(id.Value);
                    }
                }
                else
                {
                    view.ExibirMensagem("Por favor, selecione um produto na lista para desativar.");
                }
            };
            Inicializar();
        }

        public void Inicializar()
        {
            try
            {
                // Passamos a lista como 'object' para a View não precisar referenciar o Model
                var produtos = _repository.Listar();
                View.PreencherGrid(produtos);
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao carregar produtos: {ex.Message}");
            }
        }

        public void Salvar()
        {
            // O Presenter é o único responsável por instanciar o Model
            var produto = new ProdutoModel
            {
                Id = View.ObterId(),
                Descricao = View.ObterDescricao(),
                Preco = View.ObterPreco(),
                Estoque = View.ObterEstoque(),
                Ativo = View.ObterStatusAtivo()
            };

            try
            {
                var erros = _validator.Validar(produto);

                if (erros.Count > 0)
                {
                    View.ExibirMensagemErro(string.Join("\n", erros));
                    return;
                }

                if (produto.Id == 0)
                {
                    _repository.Incluir(produto);
                    View.ExibirMensagem("Produto cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(produto);
                    View.ExibirMensagem("Produto atualizado com sucesso!");
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
                _repository.Excluir(id);
                View.ExibirMensagem("Produto desativado com sucesso!");
                View.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                View.ExibirMensagemErro($"Erro ao excluir produto: {ex.Message}");
            }
        }
    }
}