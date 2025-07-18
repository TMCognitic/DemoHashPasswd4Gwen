// See https://aka.ms/new-console-template for more information
using BStorm.Tools.CommandQuerySeparation.Commands;
using BStorm.Tools.CommandQuerySeparation.Results;
using BStorm.Tools.Database;
using DemoHashPasswd.Tools;
using System.Data.Common;

namespace DemoHashPasswd.Commands;

class RegisterCommand : ICommandDefinition
{
    public string Email { get; }
    public byte[] Passwd { get; }

    public RegisterCommand(string email, string passwd)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwd, nameof(passwd));

        Email = email;
        Passwd = passwd.Hash();
    }
}

class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly DbConnection _dbConnection;

    public RegisterCommandHandler(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _dbConnection.Open();
    }

    public ICqsResult Execute(RegisterCommand command)
    {
        try
        {
            _dbConnection.ExecuteNonQuery("CSP_Register", true, command);
            _dbConnection.Close();
            return ICqsResult.Success();
        }
        catch (Exception ex)
        {
            return ICqsResult.Failure(ex.Message, ex);
        }
    }
}
