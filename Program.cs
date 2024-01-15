// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Data.Common;
using System.Reflection;
using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
class Program
{
    static void Main()
    {

        //usandoAdoNET();

        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        using (var connection = new SqlConnection(connectionString))
        {
            //UpdateCategory(connection);
            //ListCategories(connection);
            //CreateCategory(connection);
            //CreateCategory(connection);
            ExecuteProcedure(connection);


        }
    }

    static void ListCategories(SqlConnection connection)
    {

        var categories = connection.Query<Category>("Select [Id], [Title] FROM [Category]");
        Console.WriteLine("Acessando com Dapper");

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void CreateCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS 1";
        category.Url = "Amazon1";
        category.Description = "Categoria destinada a serviço AWS _";
        category.Order = 10;
        category.Summary = "AWS Cloud_";
        category.Featured = false;

        var category2 = new Category();
        category2.Id = Guid.NewGuid();
        category2.Title = "Categoria Nova";
        category2.Url = "Categoria Nova";
        category2.Description = "Categoria nova";
        category2.Order = 9;
        category2.Summary = "Categoria Nova";
        category2.Featured = false;

        var insertSql = @"INSERT INTO 
        [Category]
         VALUES (
         @Id, 
         @Title, 
         @Url, 
        @Summary,
        @Order,
        @Description,
        @featured)";

        var rows = connection.Execute(insertSql, new[] {new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        }, new
        {
            category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        }
        });
        Console.WriteLine($"{rows} linhas inseridas");
    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = "UPDATE [Category] SET [Title]=@title where [Id]=@id";
        var rows = connection.Execute(updateQuery, new
        {
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
            title = "Front End 2023"
        });
        Console.WriteLine($"{rows} registros atualizados!");
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

    static void DeleteCategory(SqlConnection connection)
    {
        var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
        var rows = connection.Execute(deleteQuery, new
        {
            id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
        });

        Console.WriteLine($"{rows} registros excluídos");
    }

    static void GetCategory(SqlConnection connection)
    {
        var category = connection
            .QueryFirstOrDefault<Category>(
                "SELECT TOP 1 [Id], [Title] FROM [Category] WHERE [Id]=@id",
                new
                {
                    id = "af3407aa-11ae-4621-a2ef-2028b85507c4"
                });
        Console.WriteLine($"{category.Id} - {category.Title}");

    }

    static void ExecuteProcedure(SqlConnection connection)
    {
        var procedure = "[spDeleteStudent]";
        var pars = new { StudentId = "2bdd3a43-d8bd-4e7f-86d5-14257dc5ebd1" };
        var affectedRows = connection.Execute(procedure, pars, commandType: System.Data.CommandType.StoredProcedure);
        Console.WriteLine($"{affectedRows} linhas afetadas");
    }
}

