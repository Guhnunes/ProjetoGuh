using Autofac;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ProjetoGuh.Features.Cliente;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Migrations;
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

            // 1. Pega a conexão do App.config
            var connectionString = ConfigurationManager.ConnectionStrings["ProjetoGuh"].ConnectionString;

            // 2. Executa a atualização do banco de dados antes de tudo
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
                var form = scope.Resolve<CadastroClienteForm>();
                Application.Run(form);
            }
        }

        private static void AtualizarBancoDeDados(string connectionString)
        {
            // Configura os serviços do FluentMigrator
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddFirebird()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(M001_CriarTabelaCliente).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            // Executa as migrações
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

            // Cliente
            builder.RegisterType<ClienteDao>().As<IClienteDao>();
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>();
            builder.RegisterType<CadastroClientePresenter>().As<ICadastroClientePresenter>();

            // Registramos o Form como Self para o Autofac conseguir criá-lo com o Presenter injetado
            builder.RegisterType<CadastroClienteForm>().AsSelf();

            return builder.Build();
        }
    }
}