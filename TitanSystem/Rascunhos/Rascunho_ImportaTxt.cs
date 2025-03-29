using System;
using TitanSystem.Entidades;
using TitanSystem.Connections;
using System.Data;
using System.IO;
using System.Windows.Forms;

using Oracle.ManagedDataAccess.Client;


namespace TitanSystem.Services
{
    class R_ImportaDadosCliente : Cliente
    {
        private static DataTable _tbImportacaoVirtual = new DataTable();

        public void ImportarTxt()

        {
            // adicionando colunas a tabela virtual
            _tbImportacaoVirtual.Columns.Add("Nome", typeof(string));
            _tbImportacaoVirtual.Columns.Add("Nascimento", typeof(DateTime));

            DataTable tbArquivo = LeituraArquivoTxt(_tbImportacaoVirtual);


            if (tbArquivo.Rows.Count == 0)
            {
                Console.WriteLine("Nenhum dado encontrado para importar.");
                return;
            }

            ConexaoOracleDatabase ConexaoBD = new ConexaoOracleDatabase();
            // OracleConnection conexao = conexaoBD.AbrirConexao();

            using (OracleConnection conexao = new OracleConnection(ConexaoBD.StringConexaoOracleBD()))
            {
                try
                {
                    conexao.Open();
                    Console.WriteLine("Conexão com o banco de dados aberta!");

                    // Iniciar transação
                    using (var transacao = conexao.BeginTransaction())
                    {
                        string queryInserirDados = "INSERT INTO pessoas (NOME, DT_NASCIMENTO) VALUES (:nome, :nascimento)";
                        using (OracleCommand command = new OracleCommand(queryInserirDados, conexao))
                        {

                            command.BindByName = true; // Garantir que os nomes dos parâmetros sejam correspondidos corretamente

                            foreach (DataRow dr in tbArquivo.Rows)
                            {
                                command.Parameters.Clear(); // Limpar parâmetros anteriores
                                command.Parameters.Add(new OracleParameter("nome", dr["Nome"]));
                                command.Parameters.Add(new OracleParameter("nascimento", dr["Nascimento"]));

                                command.ExecuteNonQuery();
                            }
                        }

                        transacao.Commit();
                        Console.WriteLine("Todos os dados foram importados com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro durante a importação: {ex.Message}");
                }

            }


        }


        static DataTable LeituraArquivoTxt(DataTable tbVirtual)
        {
            DataTable TbVirtual = tbVirtual;
            // DataTable TbArquivo = new DataTable();
            string dirArquivoTxt = @"C:\Users\andlu\source\repos\TitanSystem\Input_File_Exemple.txt";

            if (!File.Exists(dirArquivoTxt))
            {
                MessageBox.Show("Arquivo não localizado");
                return TbVirtual;
            }

            try
            {
                string[] linhasArquivo = File.ReadAllLines(dirArquivoTxt);

                for (int i = 0; i < linhasArquivo.Length; i++)
                {
                    string linhaAtual = linhasArquivo[i];

                    if (linhaAtual.StartsWith("001"))
                    {
                        string infoNome = linhaAtual.Substring(3, 40).Trim();
                        string dataNasc = linhaAtual.Substring(43, 8).Trim();
                        DateTime InfoNasc = DateTime.ParseExact(dataNasc, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DataRow TbNovaLinha = TbVirtual.NewRow();

                        TbNovaLinha["Nome"] = infoNome;
                        TbNovaLinha["Nascimento"] = InfoNasc;
                        TbVirtual.Rows.Add(TbNovaLinha);
                    }
                    else
                    {
                        Console.WriteLine(" Linha {i} do arquivo ignorada");
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o arquivo: {ex.Message}");
            }

            return TbVirtual;
        }

    }
}
