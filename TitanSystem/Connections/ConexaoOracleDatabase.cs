using Oracle.ManagedDataAccess.Client;
using System;

namespace TitanSystem.Connections
{
     class ConexaoOracleDatabase
    {
        // String de conexão
        private readonly string connectionString = "User Id=anlucas;Password=182182;Data Source=192.168.10.176:1521/XEPDB1;DBA Privilege=SYSDBA;";

        // Método para abrir a conexão
        public OracleConnection AbrirConexao()
        {
            OracleConnection conexao = new OracleConnection(connectionString);
            try
            {
                
                conexao.Open(); // Abre a conexão
                Console.WriteLine("Conexão com Oracle aberta com sucesso!");
                Console.WriteLine("Conexão aberta");
                return conexao;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir conexão com Oracle: {ex.Message}");
                return null;
            }
        }


        public void FecharConexao(OracleConnection conexaoAberta)
        {
            conexaoAberta.Close();  
            Console.WriteLine("Conexão fechada");
        }

        public string StringConexaoOracleBD()
        {

        return connectionString; 
        
        }
    }
}
