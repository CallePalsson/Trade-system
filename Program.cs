using System.ComponentModel.Design;
using System.Data.Common;
using System.Diagnostics;
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
// A user needs to be able to request a trade for other users items.                !!
// A user needs to be able to browse trade requests.                                !!
// A user needs to be able to accept a trade request.                               !!
// A user needs to be able to deny a trade request                                  !!
// A user needs to be able to browse completed requests.                            !!
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
    ////Console.Clear();();();
    //Menu for login options
    Console.WriteLine($"Welcome to tradecenter!\n");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Create a account");
    switch (Console.ReadLine())
    {
        //Asking the user for login information 
        case "1":
            ////Console.Clear();();();
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
            ////Console.Clear();();();
            Console.Write("Enter username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();
            users.Add(new User(username, password));
            break;
    }

    while (ActiveUser) //-----------------------------------Logged in--------------------------------------//
    {
        ////Console.Clear();();();
        Console.WriteLine($"Logged in as: {CurrentUser.Username}");
        Console.WriteLine("--------------------Tradecenter------------------");
        Console.WriteLine("1. Logout");
        Console.WriteLine("2. Add a item to tradecenter");
        Console.WriteLine("3. Browse the tradecenter");
        Console.WriteLine("4. Browse your listings");
        Console.WriteLine("5. Browse trade offers");
        Console.WriteLine("6. Completed trades");
        switch (Console.ReadLine())
        {

            case "1": //---------------------------Logout------------------------------
                ActiveUser = false;
                CurrentUser = null;
                Console.WriteLine("Logging out!");


                break;

            case "2": //---------------------------Add Item------------------------------
                ////Console.Clear();();();
                Console.WriteLine("-------------------Add Item---------------------");
                Console.Write("What item type of item would you like to add: ");
                string? item = Console.ReadLine();
                Console.Write("A short description of that item: ");
                string? description = Console.ReadLine();
                Item localitem = new Item(item, description, CurrentUser);
                trade.tradecenter.Add(localitem, CurrentUser);
                break;


            case "3": //---------------------------Browse Tradecenter------------------------------
                ////Console.Clear();();();
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

                    trade.InspectListingTradecenter(CurrentUser, userinput);
                }
                break;

            case "4": //---------------------------Browse Own Listings------------------------------
                trade.ShowUserListing(CurrentUser);
                Console.ReadLine();
                break;

            case "5": //----------------------------Browse Tradeoffers------------------------------
                ////Console.Clear();();();
                trade.ShowTradeOffer(CurrentUser);
                Console.WriteLine("Inspect the request by Entering id: ");
                Console.WriteLine("q to Exit");
                string userinputinspect = Console.ReadLine();
                if (userinputinspect == "q")
                {
                    break;
                }
                else
                {
                    int userinputinspectid = Convert.ToInt32(userinputinspect);
                    foreach (var it in trade.tradecenter)
                        if (userinputinspectid == it.Key.Id && CurrentUser == it.Key.Owner)
                        {
                            Console.WriteLine("1. Accept");
                            Console.WriteLine("2. Deny");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("Accepting Trade!");
                                    trade.AcceptTradeOffer(it.Key, trade.tradecenter);
                                    break;
                                case "2":
                                    trade.DenyTradeOffer(it.Key);
                                    break;
                            }

                        }



                    break;
                }
            case "6":
                foreach (var trades in trade.completedtrades)
                {
                    if (trades.Owner == CurrentUser || trades.Lastowner == CurrentUser)
                        Console.WriteLine($"Item: ({trades.Name}) Trader: {trades.Owner.Username} Requested by: {trades.TradeRequest.Username}  ({trades.status})");
                    Console.ReadLine();
                    break;
                }
                break;
        }

    }

}

