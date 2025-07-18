// See https://aka.ms/new-console-template for more information
using BStorm.Tools.CommandQuerySeparation.Queries;
using BStorm.Tools.CommandQuerySeparation.Results;
using BStorm.Tools.Database;
using DemoHashPasswd.Entities;
using DemoHashPasswd.Mappers;
using DemoHashPasswd.Tools;
using System.Data.Common;

namespace DemoHashPasswd.Queries;

class LoginQuery : IQueryDefinition<Utilisateur>
{
    public string Email { get; }
    public byte[] Passwd { get; }

    public LoginQuery(string email, string passwd)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwd, nameof(passwd));

        Email = email;
        Passwd = passwd.Hash();
    }
}

class LoginQueryHandler : IQueryHandler<LoginQuery, Utilisateur>
{
    private readonly DbConnection _dbConnection;

    public LoginQueryHandler(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _dbConnection.Open();
    }

    public ICqsResult<Utilisateur> Execute(LoginQuery query)
    {
        try
        {
            Utilisateur? utilisateur = _dbConnection.ExecuteReader("CSP_Login", dr => dr.ToUtilisateur(), true, query).SingleOrDefault();
            _dbConnection.Close();

            if (utilisateur is null)
                return ICqsResult<Utilisateur>.Failure("Erreur email ou mot de passe", null);

            return ICqsResult<Utilisateur>.Success(utilisateur);
        }
        catch (Exception ex)
        {
            return ICqsResult<Utilisateur>.Failure(ex.Message, ex);
        }
    }
}
