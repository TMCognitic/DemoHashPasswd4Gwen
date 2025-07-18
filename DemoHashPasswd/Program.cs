// See https://aka.ms/new-console-template for more information
using BStorm.Tools.CommandQuerySeparation.Commands;
using BStorm.Tools.CommandQuerySeparation.Queries;
using BStorm.Tools.CommandQuerySeparation.Results;
using DemoHashPasswd.Commands;
using DemoHashPasswd.Entities;
using DemoHashPasswd.Queries;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DemoHashPasswd;

internal class Program
{
    private static void Main(string[] args)
    {
        string email = "toto2@test.be";
        string passwd = "Test1235=";

        DbConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoHashDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        ICommandHandler<RegisterCommand> registerCommandHandler = new RegisterCommandHandler(connection);
        ICqsResult result = registerCommandHandler.Execute(new RegisterCommand(email, passwd));

        Console.WriteLine($"IsSuccess ? : {result.IsSuccess}");

        if (result.IsFailure)
            Console.WriteLine($"ErrorMessage : {result.ErrorMessage}");

        IQueryHandler<LoginQuery, Utilisateur> loginQueryHandler = new LoginQueryHandler(connection);
        ICqsResult<Utilisateur> queryResult = loginQueryHandler.Execute(new LoginQuery(email, passwd));

        Console.WriteLine($"IsSuccess ? : {queryResult.IsSuccess}");
        if (result.IsSuccess)
            Console.WriteLine($"Email : {queryResult.Content!.Email}");
    }
}
