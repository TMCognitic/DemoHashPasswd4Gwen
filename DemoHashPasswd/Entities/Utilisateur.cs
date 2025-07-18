// See https://aka.ms/new-console-template for more information
namespace DemoHashPasswd.Entities;

class Utilisateur
{
    public int Id { get; }
    public string Email { get; }
    public Utilisateur(int id, string email)
    {
        Id = id;
        Email = email;
    }
}
