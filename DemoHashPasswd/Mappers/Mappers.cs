// See https://aka.ms/new-console-template for more information
using DemoHashPasswd.Entities;
using System.Data;

namespace DemoHashPasswd.Mappers;

internal static class Mappers
{
    internal static Utilisateur ToUtilisateur(this IDataRecord record)
    {
        return new Utilisateur((int)record["Id"], (string)record["Email"]);
    }
}
