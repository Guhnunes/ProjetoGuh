using Autofac;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ProjetoGuh.Features.Cliente;
using ProjetoGuh.Features.Infraestrutura;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace ProjetoGuh
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            var container = ConfigurarInjecao();
            var form = container.Resolve<CadastroClienteForm>();
            Application.Run(form);
        }

        private static IContainer ConfigurarInjecao()
        {
            var builder = new ContainerBuilder();

            // Infraestrutura
            var connectionString = ConfigurationManager.ConnectionStrings["ProjetoGuh"].ConnectionString;
            builder.RegisterInstance(new FabricaDeConexao(connectionString)).As<IFabricaDeConexao>();

            // Cliente
            builder.RegisterType<ClienteDao>().As<IClienteDao>();
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>();
            builder.RegisterType<CadastroClientePresenter>().As<ICadastroClientePresenter>();
            builder.RegisterType<CadastroClienteForm>().AsSelf();

            return builder.Build();
        }
    }
}