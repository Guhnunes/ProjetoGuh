using ProjetoGuh.Features.Infraestrutura;
using System;

namespace ProjetoGuh.Features.Produto
{
    public class CadastroProdutoPresenter : ICadastroProdutoPresenter   
    {
        private ICadastroProdutoView _view;
        private readonly IProdutoRepository _repository;
        private readonly ProdutoModelValidator _validator;

        public CadastroProdutoPresenter(IProdutoView view, IProdutoRepository repository)
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
                var produtoSelecionado = _view.ObterProdutoSelecionado();
                if (produtoSelecionado != null)
                {
                    Excluir(produtoSelecionado.Id);
                }
                else
                {
                    ControleDeMensagens.Avisar("Por favor, selecione um produto na lista para excluir.");
                }
            };
        }
        public void Inicializar()
        {
            try
            {
                var produtos = _repository.Listar();
                _view.PreencherGrid(produtos);
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao carregar produtos: {ex.Message}");
            }
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
                ControleDeMensagens.Avisar($"Erro ao salvar produto: {ex.Message}");
            }
        }
        public void Excluir(int id)
        {
            try
            {
                if (!ControleDeMensagens.Perguntar("Deseja realmente excluir este produto?"))
                    return;

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
