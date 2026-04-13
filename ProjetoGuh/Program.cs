using Autofac;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ProjetoGuh.Features.Cliente;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Menu;
using ProjetoGuh.Features.Migrations;
using ProjetoGuh.Features.Produto;
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
            Application.SetCompatibleTextRenderingDefault(false);

            var connectionString = ConfigurationManager.ConnectionStrings["ProjetoGuh"].ConnectionString;

            try
            {
                AtualizarBancoDeDados(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o banco de dados:\n{ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var container = ConfigurarInjecao(connectionString);

            using (var scope = container.BeginLifetimeScope())
            {
                var presenter = scope.Resolve<MenuPrincipalPresenter>();
                var formPrincipal = (Form)scope.Resolve<IMenuPrincipalView>();

                Application.Run(formPrincipal); //Estamos chamando a tela principal aq
            }
        }

        private static void AtualizarBancoDeDados(string connectionString)
        {
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddFirebird() // Mantendo o seu Firebird
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(M001_CriarTabelaCliente).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }

        private static IContainer ConfigurarInjecao(string connectionString)
        {
            var builder = new ContainerBuilder();

            // Infraestrutura
            builder.RegisterInstance(new FabricaDeConexao(connectionString)).As<IFabricaDeConexao>();

            // --- MENU PRINCIPAL ---
            // Registramos o Presenter e a View do Menu
            builder.RegisterType<MenuPrincipalPresenter>().AsSelf().SingleInstance();
            builder.RegisterType<MenuPrincipalForm>().As<IMenuPrincipalView>().AsSelf().SingleInstance();

            // --- CLIENTE ---
            builder.RegisterType<ClienteDao>().As<IClienteDao>();
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>();
            builder.RegisterType<CadastroClientePresenter>().As<ICadastroClientePresenter>();
            builder.RegisterType<CadastroClienteForm>().AsSelf();

            // --- PRODUTO  ---
            builder.RegisterType<ProdutoDao>().As<IProdutoDao>();
            builder.RegisterType<ProdutoRepository>().As<IProdutoRepository>();
            builder.RegisterType<CadastroProdutoPresenter>().AsSelf();
            builder.RegisterType<CadastroProdutoForm>().As<ICadastroProdutoView>().AsSelf();

            return builder.Build();
        }
    }
}