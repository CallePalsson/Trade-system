using System.ComponentModel.Design;

namespace App;

public class User
{
    public string Username;
    public string Password;
    public bool IsLoggedIn;

    public User(string username, string password, bool isloggedin)
    {
        Username = username;
        Password = password;
        IsLoggedIn = isloggedin;
    }
    public bool Trylogin(string username, string password)
    {
        if (username == Username && password == Password)
        {
            IsLoggedIn = true;
            return true;
        }
        else
        {
            return false;
        }


    }
    public void Logout()
    {
        IsLoggedIn = false;
        Console.WriteLine($"{Username} logging out!");
    }
}