using ProjetoGuh.Features.Infraestrutura;
using System.Collections.Generic;
using Dapper;
using ProjetoGuh.Features.Cliente.Model;

namespace ProjetoGuh.Features.Cliente.Dao
{
    public class ClienteDao : IClienteDao
    {
        private readonly IFabricaDeConexao _fabricaDeConexao;

        public ClienteDao(IFabricaDeConexao fabricaDeConexao)
        {
            _fabricaDeConexao = fabricaDeConexao;
        }

        public void Incluir(ClienteModel cliente)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                conexao.Execute(@"INSERT INTO CLIENTE (NOME, CPF_CNPJ, TELEFONE, EMAIL, DATA_CADASTRO)
                              VALUES (@Nome, @CpfCnpj, @Telefone, @Email, @DataCadastro)", cliente);
            }
        }

        // Implementar os demais métodos seguindo o mesmo padrão
        public void Alterar(ClienteModel cliente)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                conexao.Execute(@"UPDATE CLIENTE SET NOME = @Nome,CPF_CNPJ = @CpfCnpj,TELEFONE = @Telefone, EMAIL = @Email WHERE ID = @Id", cliente);
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                conexao.Execute("DELETE FROM CLIENTE WHERE ID = @Id", new { Id = id });
            }
        }

        public ClienteModel RetornarPorId(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                var sql = @"SELECT 
                        ID, 
                        NOME, 
                        CPF_CNPJ AS CpfCnpj, 
                        TELEFONE, 
                        EMAIL, 
                        DATA_CADASTRO AS DataCadastro 
                    FROM CLIENTE 
                    WHERE ID = @Id";
                conexao.Open();
                return conexao.QueryFirstOrDefault<ClienteModel>(
                    sql, new { Id = id });
            }
        }
        public List<ClienteModel> Listar()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                var sql = @"SELECT 
                        id, 
                        nome, 
                        cpf_cnpj AS CpfCnpj, 
                        telefone, 
                        email, 
                        data_cadastro AS DataCadastro 
                    FROM Cliente"; //Tive que colocar um Alias por conta do underline. O dapper não consegue mapear
                conexao.Open();
                return conexao.Query<ClienteModel>(sql).AsList();
            }
        }
    }
}
