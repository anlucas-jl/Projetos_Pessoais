using System;
using TitanSystem.Entidades;
using TitanSystem.Connections;
using System.Data;
using System.IO;
using System.Windows.Forms;

using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;


namespace TitanSystem.Services
{
     class ImportaDadosCliente : Cliente
    {
          public void ImportarTxt()

        {

            var(nomes, datas) = LeituraArquivoTxt();


            if (nomes.Length == 0 || datas.Length == 0)
            {
                Console.WriteLine("Nenhum dado encontrado para importar.");
                return;
            }

            ConexaoOracleDatabase ConexaoBD = new ConexaoOracleDatabase();

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
                            //Contando quantos registros serão processados Array Binding
                            command.ArrayBindCount = nomes.Length; 

                            // Adicionar os parâmetros com arrays de valores
                            command.Parameters.Add(new OracleParameter("nome", OracleDbType.Varchar2)
                            {
                                Value = nomes
                            });

                            command.Parameters.Add(new OracleParameter("nascimento", OracleDbType.Date)
                            {
                                Value = datas
                            });

                            // Executar a inserção em lote
                            int registrosInseridos = command.ExecuteNonQuery();
                            Console.WriteLine($"{registrosInseridos} registros inseridos com sucesso!");

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


        private (string[] nomes, DateTime[] datas) LeituraArquivoTxt()
        {
            
           //Desenhar solução para buscar de uma arquivo settings.json
            string dirArquivoTxt = @"C:\Users\andlu\source\repos\TitanSystem\Input_File_Exemple.txt";

            if (!File.Exists(dirArquivoTxt))
            {
                Console.WriteLine("Arquivo não localizado.");
                return (Array.Empty<string>(), Array.Empty<DateTime>());
            }

            try
            {
                string[] linhasArquivo = File.ReadAllLines(dirArquivoTxt);

                // Listas temporárias para armazenar os dados
                var listaNomes = new List<string>();
                var listaDatas = new List<DateTime>();

                for (int i = 0; i < linhasArquivo.Length; i++)
                {
                    string linhaAtual = linhasArquivo[i];

                    if (linhaAtual.StartsWith("001")) 
                    {
                        string nome = linhaAtual.Substring(3, 40).Trim();
                        string dataString = linhaAtual.Substring(43, 8).Trim();

                        if (DateTime.TryParseExact(dataString, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime data))
                        {
                            listaNomes.Add(nome);
                            listaDatas.Add(data);
                        }
                        else
                        {
                            Console.WriteLine($"Data inválida na linha {i + 1}: {linhaAtual}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Linha {i + 1} ignorada: Formato inválido ou não começa com '001'.");
                    }
                }

                return (listaNomes.ToArray(), listaDatas.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o arquivo: {ex.Message}");
                return (Array.Empty<string>(), Array.Empty<DateTime>());
            }
        }

    }
}
