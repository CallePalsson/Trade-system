using System.ComponentModel.Design;
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

//Dictionary to store a complete advertisment
Dictionary<Item, User> tradecenter = new Dictionary<Item, User>();
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
    while (ActiveUser)
    {

        Console.WriteLine($"Logged in as: {CurrentUser.Username}");
        Console.WriteLine("--------------------Tradecenter------------------");
        Console.WriteLine("1. Logout");
        Console.WriteLine("2. Add a item to tradecenter");
        Console.WriteLine("3. Browse the tradecenter");
        Console.WriteLine("4. Browse your listings");
        switch (Console.ReadLine())
        {
            case "1":
                ActiveUser = false;
                CurrentUser = null;
                Console.WriteLine("Logging out!");
                break;
            case "2":
                Console.Write("What item type of item would you like to add: ");
                string? item = Console.ReadLine();
                Console.Write("A short description of that item: ");
                string? description = Console.ReadLine();
                Item localitem = new Item(item, description);
                tradecenter.Add(localitem, CurrentUser);



                // foreach (var i in items)
                // {
                //     Console.WriteLine(i.Name);
                //     Console.WriteLine($"Description\n{i.Description}");

                // }
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("----------------Tradecenter-------------------");
                foreach (var listings in tradecenter)
                {
                    if (CurrentUser.Username == listings.Value.Username)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Listing Id: {listings.Key.Id}  Item: {listings.Key.Name} Trader: {listings.Value.Username} ");
                    }
                }
                Console.WriteLine("Enter the Listing id to inspect:");
                Console.WriteLine("To quit enter q");
                string userinput = Console.ReadLine();
                if (userinput == "q")
                {
                    break;
                }
                else
                {
                    int userinputindex = Convert.ToInt32(userinput);
                    foreach (var listing in tradecenter)
                    {
                        if (userinputindex == listing.Key.Id)
                        {
                            if (CurrentUser.Username == listing.Value.Username)
                            {
                                Console.WriteLine("There is no listing with that id!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"({listing.Key.Name})");
                                Console.WriteLine($"Descritpion\n{listing.Key.Description}");
                                Console.WriteLine("\n\n1.Send trade offer\n2.Back");
                            }


                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("Trade offer sent!");
                                    listing.Key.status = TradeStatus.Requested;
                                    listing.Key.TradeRequest = CurrentUser.Username;
                                    break;
                                case "2":
                                    break;
                            }
                        }
                    }
                    break;
                }
            case "4":
                Console.WriteLine("----------------Your Listings-------------------");
                foreach (var listing in tradecenter)
                {
                    if (listing.Value.Username == CurrentUser.Username)
                    {
                        Console.WriteLine($"Id: {listing.Key.Id} Item: {listing.Key.Name} Status: {listing.Key.status}");
                        if (listing.Key.status == TradeStatus.Requested)
                        {
                            Console.WriteLine($"Trade request sent by {listing.Key.TradeRequest}");
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                break;

        }


    }

    // foreach (var user in users)
    // {
    //     Console.WriteLine(user.Username);
    // }


}