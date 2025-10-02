using System.ComponentModel.Design;

namespace App;

public class User
{
    public string Username;
    string Password;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public bool Trylogin(string username, string password)
    {
        return username == Username && password == Password;

    }
    public void Logout(bool ActiveUser, User CurrentUser)
    {
        ActiveUser = false;
        CurrentUser = null;
        Console.WriteLine("Logging out!");
    }
}