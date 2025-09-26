using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Transactions;
using App;

// A user needs to be able to register an account !!
// A user needs to be able to log out.!!
// A user needs to be able to log in.!!
// A user needs to be able to upload information about the item they wish to trade.
// A user needs to be able to browse a list of other users items.
// A user needs to be able to request a trade for other users items.
// A user needs to be able to browse trade requests.
// A user needs to be able to accept a trade request.
// A user needs to be able to deny a trade request.
// A user needs to be able to browse completed requests.
List<User> users = new List<User>();
List<Item> tradecenter = new List<Item>();


users.Add(new User("Calle" , "123"));

bool running = true;

while (running)
{

    bool ActiveUser = false;
    Console.WriteLine($"Welcome to tradecenter!\n\n");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Create a account");
    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Enter username: ");
            string? L_username = Console.ReadLine();
            Console.Write("Enter password: ");
            string? L_password = Console.ReadLine();
            foreach (var user in users)
            {
                if (user.Trylogin(L_username, L_password) == true)
                {
                    ActiveUser = true;
                    string CurrentUser = user.Username;
                }
            }
            break;
        case "2":
            Console.Write("Enter username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();
            users.Add(new User(username, password));
            break;
    }
    while (ActiveUser)
    {

        Console.WriteLine("Logged in as: ");
        Console.WriteLine("--------------------Tradecenter------------------");
        Console.WriteLine("1. Logout");
        Console.WriteLine("2. Add a item to tradecenter");
        Console.WriteLine("3. ");
        switch (Console.ReadLine())
        {
            case "1":
                ActiveUser = false;
                Console.WriteLine("Logging out!");
                Console.ReadLine();
                break;
            case "2":
                break;
        }


    }

    // foreach (var user in users)
    // {
    //     Console.WriteLine(user.Username);
    // }


}