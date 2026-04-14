using ProjetoGuh.Features.Infraestrutura;
using System;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.View;
using ProjetoGuh.Features.Produto.Repository;

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
            _view.BotaoExcluirFoiClicado += (s, e) =>
            {
                var produtoSelecionado = _view.ObterProdutoSelecionado();
                if (produtoSelecionado != null)
                {
                    if (ControleDeMensagens.Perguntar("Gostaria de desativar o produto selecionado?"))
                    {
                        Excluir(produtoSelecionado.Id);
                        _view.LimparFormulario();
                        Inicializar();
                    }
                }
                else
                {
                    ControleDeMensagens.Avisar("Por favor, selecione um produto na lista para desativar.");
                }
            };
        }

        public void Inicializar()
        {
            _view.PreencherGrid(_repository.Listar());
        }

        public void Salvar()
        {
            try
            {
                var produto = _view.ObterDadosDoFormulario();
                var erros = _validator.Validar(produto);

                if (erros.Count > 0)
                {
                    string mensagemErro = string.Join("\n", erros);
                    ControleDeMensagens.Avisar(mensagemErro);
                    return;
                }

                if (produto.Id == 0)
                {
                    _repository.Incluir(produto);
                    ControleDeMensagens.Informar("Produto cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(produto);
                    ControleDeMensagens.Informar("Produto atualizado com sucesso!");
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
                _repository.Excluir(id);
                ControleDeMensagens.Informar("Produto excluído com sucesso!");
                Inicializar();
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao excluir produto: {ex.Message}");
            }
        }
    }
}
