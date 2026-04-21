using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Repository;
using System;
using System.Linq;

namespace ProjetoGuh.Features.Produto.Presenter
{
    public class CadastroProdutoPresenter : ICadastroProdutoPresenter
    {
        private ICadastroProdutoView _view;
        private readonly IProdutoRepository _repository;
        private readonly ProdutoModelValidator _validator;

        public CadastroProdutoPresenter(IProdutoRepository repository)
        {
            _repository = repository;
            _validator = new ProdutoModelValidator();
        }

        public void SetView(ICadastroProdutoView view)
        {
            _view = view;
            _view.BotaoSalvarFoiClicado += (s, e) => Salvar();
            _view.BotaoCancelarFoiClicado += (s, e) => _view.LimparFormulario();
            _view.BotaoExcluirFoiClicado += (s, e) =>
            {
                // Buscamos apenas o ID da View, não o objeto Model
                var id = _view.ObterIdSelecionadoNaGrid();

                if (id.HasValue)
                {
                    if (_view.ConfirmarExclusao()) // A View decide como perguntar (MessageBox)
                    {
                        Excluir(id.Value);
                    }
                }
                else
                {
                    _view.ExibirMensagem("Por favor, selecione um produto na lista para desativar.");
                }
            };
        }

        public void Inicializar()
        {
            try
            {
                // Passamos a lista como 'object' para a View não precisar referenciar o Model
                var produtos = _repository.Listar();
                _view.PreencherGrid(produtos);
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao carregar produtos: {ex.Message}");
            }
        }

        public void Salvar()
        {
            // O Presenter é o único responsável por instanciar o Model
            var produto = new ProdutoModel
            {
                Id = _view.ObterId(),
                Descricao = _view.ObterDescricao(),
                Preco = _view.ObterPreco(),
                Estoque = _view.ObterEstoque(),
                Ativo = _view.ObterStatusAtivo()
            };

            try
            {
                var erros = _validator.Validar(produto);

                if (erros.Count > 0)
                {
                    _view.ExibirMensagemErro(string.Join("\n", erros));
                    return;
                }

                if (produto.Id == 0)
                {
                    _repository.Incluir(produto);
                    _view.ExibirMensagem("Produto cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(produto);
                    _view.ExibirMensagem("Produto atualizado com sucesso!");
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
                _repository.Excluir(id);
                _view.ExibirMensagem("Produto desativado com sucesso!");
                _view.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro($"Erro ao excluir produto: {ex.Message}");
            }
        }
    }
}