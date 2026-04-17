using ProjetoGuh.Features.Infraestrutura;
using System;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.View;
using ProjetoGuh.Features.Venda.Repository;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public class CadastroVendaPresenter : ICadastroVendaPresenter
    {
        private IPdvView _view;
        private readonly IVendaRepository _repository;
        private readonly VendaModelValidator _validator;

        public CadastroVendaPresenter(IVendaRepository repository)
        {
            _repository = repository;
            _validator = new VendaModelValidator();
        }

        public void SetView(IPdvView view)
        {
            _view = view;
           /* _view.BotaoSalvarFoiClicado += (s, e) => Salvar();
            _view.BotaoExcluirFoiClicado += (s, e) =>
            {
                var VendaSelecionado = _view.ObterVendaSelecionado();
                if (VendaSelecionado != null)
                {
                    if (ControleDeMensagens.Perguntar("Gostaria de desativar o Venda selecionado?"))
                    {
                        Excluir(VendaSelecionado.Id);
                        _view.LimparFormulario();
                        Inicializar();
                    }
                }
                else
                {
                    ControleDeMensagens.Avisar("Por favor, selecione um Venda na lista para desativar.");
                }
            };*/
        }

        public void Inicializar()
        {
            /*_view.PreencherGrid(_repository.Listar());*/
        }

        public void Salvar()
        {/*
            try
            {
                var Venda = _view.ObterDadosDoFormulario();
                var erros = _validator.Validar(Venda);

                if (erros.Count > 0)
                {
                    string mensagemErro = string.Join("\n", erros);
                    ControleDeMensagens.Avisar(mensagemErro);
                    return;
                }

                if (Venda.Id == 0)
                {
                    _repository.Incluir(Venda);
                    ControleDeMensagens.Informar("Venda cadastrado com sucesso!");
                }
                else
                {
                    _repository.Alterar(Venda);
                    ControleDeMensagens.Informar("Venda atualizado com sucesso!");
                }

                _view.LimparFormulario();
                Inicializar();
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao salvar: {ex.Message}");
            }*/
        }

        public void Excluir(int id)
        {
            try
            {
                _repository.Excluir(id);
                ControleDeMensagens.Informar("Venda excluído com sucesso!");
                Inicializar();
            }
            catch (Exception ex)
            {
                ControleDeMensagens.Avisar($"Erro ao excluir Venda: {ex.Message}");
            }
        }
    }
}
