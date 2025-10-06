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
//Program needs to be able to save trades                                           !!
//program needs to be able to save users                                            !!


//List of users and items to store users inputs
List<User> users = new List<User>();
List<Item> items = new List<Item>();

//instance of Trade class and Filemanager class
Trade trade = new Trade();
Filemanager fm = new Filemanager();

//Loading users and completedtrades
fm.LoadUsers(users);
fm.LoadCompletedTrades(trade.completedtrades, users);


bool ActiveUser = false; //ActiveUser false becuase no one is logged in

//CurrentUser set to null 
User? CurrentUser = null;

// looping thru saved users if a user is logged in then it opens as logged in
for (int i = 0; i < users.Count; i++)
{
    if (users[i].IsLoggedIn)
    {   
        //if a user is logged in it sets the user as Currentuser
        CurrentUser = users[i];
        //This is to get in to logged in state
        ActiveUser = true;
    }
}

bool Running = true;

while (Running)
{
    //if there is not a active user do this
    if (!ActiveUser)
    {
        Console.Clear();
        //Menu for login options
        Console.WriteLine($"Welcome to tradecenter!\n");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Create a account");
        switch (Console.ReadLine())
        {
            //Asking the user for login information 
            case "1": //----------------------------Login Menu----------------------------
                Console.Clear();
                Console.WriteLine("-------------------Login Menu---------------------");
                Console.Write("Enter username: ");
                string? L_username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? L_password = Console.ReadLine();

                //Looping through users list and checking if there is a user with the users inputs
                foreach (var user in users)
                {

                    if (user.Trylogin(L_username, L_password) == true)
                    {
                        // This goes in the logged in state
                        ActiveUser = true;
                        CurrentUser = user;
                        bool isloggedin = true;

                        // Updates the users state in the txt file
                        fm.UpdateUser(users, L_username, L_password, isloggedin, CurrentUser);
                        break;
                    }

                }


                break;
            case "2": // ----------------Create Account--------------
                //Asking for inputs
                Console.Clear();
                Console.WriteLine("----------Create Account---------");
                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();

                //Checking the account the user is trying to create already exists
                if (fm.CheckDuplicates(users, username, password))
                {
                    // if it does then it will write this
                    Console.WriteLine("User already exist");

                    // pauses the code
                    Console.ReadLine();
                    break;
                }
                else
                {
                    // Creating user with user inputs above
                    users.Add(new User(username, password, false));
                    Console.WriteLine($"{username} has been created!");

                    // Save Users to users.txt so that they later can be loaded in
                    fm.SaveUsers(users);
                }
                break;
        }

    }
    else
    {
        //-----------------------------------Logged in--------------------------------------//
        {
            // logged in menu
            Console.Clear();
            fm.ShowUserFile();
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
                    bool isloggedin = false;

                    // Updates the user in the txt file
                    fm.UpdateUser(users, CurrentUser.Username, CurrentUser.Password, isloggedin, CurrentUser);
                    Console.WriteLine("Logging out!");
                    CurrentUser = null;
                    break;

                case "2": //---------------------------Add Item------------------------------
                    // add item menu
                    Console.Clear();
                    Console.WriteLine("-------------------Add Item---------------------");
                    Console.Write("What item would you like to add: ");
                    string? item = Console.ReadLine();
                    Console.Write("A short description of that item: ");
                    string? description = Console.ReadLine();

                    // saves item localy to the put up on tradecenter dictionary
                    Item localitem = new Item(item, description, CurrentUser);
                    trade.tradecenter.Add(localitem, CurrentUser);
                    break;

                case "3": //---------------------------Browse Tradecenter------------------------------
                    Console.Clear();

                    //Shows tradecenter
                    trade.ShowTradecenter(CurrentUser);
                    Console.WriteLine("Enter the Listing id to inspect:");
                    Console.WriteLine("To quit enter q");
                    string userinput = Console.ReadLine();

                    //try except to prevent crasches
                    try
                    {
                        if (userinput == "q")
                        {
                            break;
                        }
                        else
                        {
                            //lets user choose and inspect listing with input above
                            trade.InspectListingTradecenter(CurrentUser, userinput);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input");
                    }

                    break;

                case "4": //---------------------------Browse Own Listings------------------------------
                    Console.Clear();
                    trade.ShowUserListing(CurrentUser);
                    Console.ReadLine();
                    break;

                case "5": //----------------------------Browse Tradeoffers------------------------------
                    Console.Clear();
                    trade.ShowTradeOffer(CurrentUser);
                    Console.WriteLine("Inspect the request by Entering id: ");
                    Console.WriteLine("q to Exit");
                    string userinputinspect = Console.ReadLine();

                    //try except to prevent crasches
                    try
                    {
                        if (userinputinspect == "q")
                        {
                            break;
                        }
                        else
                        {
                            //converts input to int for choosing id
                            int userinputinspectid = Convert.ToInt32(userinputinspect);
                            foreach (var it in trade.tradecenter)

                                //looking for id with input
                                if (userinputinspectid == it.Key.Id && CurrentUser == it.Key.Owner)
                                {
                                    Console.WriteLine("1. Accept");
                                    Console.WriteLine("2. Deny");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":// Accepting trade
                                            Console.WriteLine("Accepting Trade!");

                                            trade.AcceptTradeOffer(it.Key, trade.tradecenter);
                                            fm.SaveCompletedTrades(trade.completedtrades);
                                            break;
                                        case "2":
                                            trade.DenyTradeOffer(it.Key);
                                            fm.SaveCompletedTrades(trade.completedtrades);
                                            break;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("There is no request with that id!");
                                    break;
                                }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input");
                    }
                    break;

                case "6":
                    Console.Clear();
                    foreach (var trades in trade.completedtrades)
                    {
                        if (trades.Owner.Username == CurrentUser.Username || trades.Lastowner.Username == CurrentUser.Username)
                            Console.WriteLine($"Item: ({trades.Name}) Trader: {trades.Lastowner.Username} Requested by: {trades.TradeRequest.Username}  ({trades.status})");


                    }
                    // pauses the code
                    Console.ReadLine();
                    break;
            }

        }

    }
}