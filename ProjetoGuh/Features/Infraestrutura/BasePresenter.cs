using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Infraestrutura
{
    public abstract class BasePresenter<TView>
    {
        protected readonly TView View;

        protected BasePresenter(TView view) => View = view;

        public abstract void Inicializar();
    }
}
