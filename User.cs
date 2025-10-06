using System.ComponentModel.Design;

namespace App;

public class User
{
    //used for constructor
    public string Username;
    public string Password;
    public bool IsLoggedIn;

    // constructor
    public User(string username, string password, bool isloggedin)
    {
        Username = username;
        Password = password;
        IsLoggedIn = isloggedin;
    }

    // compares the inputs to users to se if there is a account with that login
    public bool Trylogin(string username, string password)
    {
        // if there is then IsLoggedIn is set to true
        if (username == Username && password == Password)
        {
            IsLoggedIn = true;
            // also returns a bool that is true
            return true;
        }
        else
        {
            // if not found is set to false
            return false;
        }


    }
    //(didnt use this) Used to logout user
    public void Logout()
    {
        IsLoggedIn = false;
        Console.WriteLine($"{Username} logging out!");
    }
}