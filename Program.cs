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


            var categories = connection.Query<Category>("Select [Id], [Title] FROM [Category]");
            Console.WriteLine("Acessando com Dapper");

            foreach (var Category in categories)
            {
                Console.WriteLine($"{Category.Id} - {Category.Title}");
            }
        }

    }

    static void usandoAdoNET()
    {

        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        using (var connection = new SqlConnection(connectionString))
        {
            Console.WriteLine("Conectado");
            connection.Open();

            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select [Id], [Title] FROM [Category]";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                }
            }
        }
    }

}

