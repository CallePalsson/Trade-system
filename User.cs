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
    public void ShowCurrentUser()
    {
        Console.WriteLine(Username);
    }
    public bool Trylogin(string username, string password)
    {
        return username == Username && password == Password;

    }
}