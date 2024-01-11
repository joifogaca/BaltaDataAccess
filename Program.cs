// See https://aka.ms/new-console-template for more information

using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;
class Program
{
    static void Main()
    {

        //usandoAdoNET();

        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        using (var connection = new SqlConnection(connectionString))
        {


            var categories = connection.Query<Category>("Select [Id], [Title] FROM [Category]");  // O método Query sempre retorna uma lista
            Console.WriteLine("Acessando com Dapper");

            foreach (var Category in categories)
            {
                Console.WriteLine($"{Category.Id} - {Category.Title}");
            }
        }

    }

    static void usandoAdoNET()
    {
        //Microsoft.Data.SqlClient
        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";
        /*  var connection = new SqlConnection();
         connection.Open();
         //Fazer todos os comandos de uma vez, nao abrir e fechar para cada comando;
         connection.Close(); // Não esquecer de fechar a conexão, mas o garbage colletor vai fechar, mais é melhor o programador ter proatividade
         connection.Dispose(); // destroi o objeto, será necessario abrir a conexão de novo
  */


        using (var connection = new SqlConnection(connectionString)) // Já fecha a conexão ao terminar de usar
        {
            Console.WriteLine("Conectado");
            connection.Open();

            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select [Id], [Title] FROM [Category]";

                var reader = command.ExecuteReader(); // Leitura

                while (reader.Read()) // SqlDAtaReader é um cursor só vai para frente
                {
                    Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                }
            }
        }
    }

}

