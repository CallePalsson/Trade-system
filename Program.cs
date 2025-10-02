using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Transactions;
using App;

// A user needs to be able to register an account                                   !!
// A user needs to be able to log out.                                              !!
// A user needs to be able to log in.                                               !!
// A user needs to be able to upload information about the item they wish to trade. !!
// A user needs to be able to browse a list of other users items.                   !!
// A user needs to be able to request a trade for other users items.                !

// A user needs to be able to browse trade requests.

// A user needs to be able to accept a trade request.
// A user needs to be able to deny a trade request.
// A user needs to be able to browse completed requests.
//Program needs to be able to save trades
//program needs to be able to save users

//List of users and items to store users inputs
List<User> users = new List<User>();
List<Item> items = new List<Item>();
Trade trade = new Trade();

//Dictionary to store a complete advertisment
User? CurrentUser = null;
users.Add(new User("a", "a"));
bool Running = true;

while (Running)
{
    bool ActiveUser = false; //ActiveUser false becuase no one is logged in

    //Menu for login options
    Console.WriteLine($"Welcome to tradecenter!\n");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Create a account");
    switch (Console.ReadLine())
    {
        //Asking the user for login information 
        case "1":
            Console.Write("Enter username: ");
            string? L_username = Console.ReadLine();
            Console.Write("Enter password: ");
            string? L_password = Console.ReadLine();

            //Looping through users list and checking if there is a user with the users inputs
            foreach (var user in users)
            {

                if (user.Trylogin(L_username, L_password) == true)
                {
                    ActiveUser = true;
                    CurrentUser = user;
                    break;
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
    while (ActiveUser) //-----------------------------------Logged in--------------------------------------//
    {

        Console.WriteLine($"Logged in as: {CurrentUser.Username}");
        Console.WriteLine("--------------------Tradecenter------------------");
        Console.WriteLine("1. Logout");
        Console.WriteLine("2. Add a item to tradecenter");
        Console.WriteLine("3. Browse the tradecenter");
        Console.WriteLine("4. Browse your listings");
        Console.WriteLine("5. Browse trade offers");
        switch (Console.ReadLine())
        {
            case "1": //---------------------------Logout------------------------------
                ActiveUser = false;
                CurrentUser = null;
                Console.WriteLine("Logging out!");
                break;

            case "2": //---------------------------Add Item------------------------------
                Console.Write("What item type of item would you like to add: ");
                string? item = Console.ReadLine();
                Console.Write("A short description of that item: ");
                string? description = Console.ReadLine();
                Item localitem = new Item(item, description);
                trade.tradecenter.Add(localitem, CurrentUser);
                break;

            case "3": //---------------------------Browse Tradecenter------------------------------
                trade.ShowTradecenter(CurrentUser);
                Console.WriteLine("Enter the Listing id to inspect:");
                Console.WriteLine("To quit enter q");
                string userinput = Console.ReadLine();
                if (userinput == "q")
                {
                    break;
                }
                else
                {
                    trade.InspectListing(CurrentUser, userinput);
                }
                break;

            case "4": //---------------------------Browse Own Listings------------------------------
                trade.ShowUserListing(CurrentUser);
                break;
            case "5":
                trade.ShowTradeOffer(CurrentUser);
                Console.WriteLine("Enter id of the item you would want to inspect:");
                Console.WriteLine("q to Exit");
                string userinputinspect = Console.ReadLine();
                int userinputinspectid = Convert.ToInt32(userinputinspect);

                break;
        }

    }

}

