
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace App;

public class Trade
{


    public Dictionary<Item, User> tradecenter = new Dictionary<Item, User>();
    public List<Item> completedtrades = new List<Item>();


    //browse tradecenter 
    public void ShowTradecenter(User CurrentUser)
    {


        Console.WriteLine("----------------Tradecenter-------------------");
        //loops thru tradecenter
        foreach (var listings in tradecenter)
        {
            if (CurrentUser.Username == listings.Value.Username)
            {
                continue;
            }
            else
            {
                Console.WriteLine($"Listing Id: {listings.Key.Id}  Item: {listings.Key.Name} Status: {listings.Key.status} ");
            }
        }
    }
    public void ShowUserListing(User CurrentUser)
    {
        Console.WriteLine("----------------Your Listings-------------------");
        //loops thru your listings
        foreach (var listing in tradecenter)
            if (CurrentUser == listing.Key.Owner)
            {
                Console.WriteLine($"Listing Id: {listing.Key.Id}  Item: ({listing.Key.Name}) Description: {listing.Key.Description} ");
            }
            else
            {
                continue;
            }
    }
    public void ShowTradeOffer(User CurrentUser)
    {
        Console.WriteLine("----------------Trade Offers-------------------");
        //loops thru trade offers
        foreach (var listing in tradecenter)
        {

            if (listing.Key.status == TradeStatus.Requested && CurrentUser.Username == listing.Value.Username)
            {
                Console.WriteLine($"Id: {listing.Key.Id} ({listing.Key.Name}) Trade request sent by {listing.Key.TradeRequest.Username}");
            }

            else
            {
                continue;
            }
        }
    }

    public void InspectListingTradecenter(User CurrentUser, string userinput)
    {
        //converts input to int for choosing id
        int userinputindex = Convert.ToInt32(userinput);
        foreach (var listing in tradecenter)
        {
            //looking for id with input
            if (userinputindex == listing.Key.Id)
            {
                //Checks if currentuser is the owner
                if (CurrentUser.Username == listing.Key.Owner.Username)
                {
                    //if it is then this
                    Console.WriteLine("There is no listing with that id!");
                    break;
                }
                else
                { // else writes out info
                    Console.WriteLine($"({listing.Key.Name})");
                    Console.WriteLine($"Descritpion\n{listing.Key.Description}");
                    Console.WriteLine("\n\n1.Send trade offer\n2.Back");
                }


                switch (Console.ReadLine())
                {
                    case "1":
                        SendTradeOffer(listing.Key, CurrentUser);
                        break;
                    case "2":
                        break;
                }
            }

        }

    }
    public void SendTradeOffer(Item item, User CurrentUser)
    {
        Console.WriteLine("Trade offer sent!");

        //sets status to requested
        item.status = TradeStatus.Requested;

        //sets traderequest slot to current user
        item.TradeRequest = CurrentUser;
        Console.ReadLine();
    }

    public void AcceptTradeOffer(Item item, Dictionary<Item, User> tradecenter)
    {
        //sets status to approved
        item.status = TradeStatus.Approved;

        //sets owner to last owner
        item.Lastowner = item.Owner;

        //sets the requester to owner
        item.Owner = item.TradeRequest;


        //sets the user in tradecenter to its owner
        tradecenter[item] = item.Owner;
        Console.WriteLine("trade approved!");

        //removes item from tradecenter
        tradecenter.Remove(item);
        Console.WriteLine("item deleted from tradecenter");

        //adds item to completed trades history
        completedtrades.Add(item);
        Console.WriteLine($"Item added to yours and {item.Owner.Username} trades history");

    }



    public void DenyTradeOffer(Item item)
    {
        //sets status to denied dosent do anything becuase it goes to waiting right after
        item.status = TradeStatus.Denied;
        Console.WriteLine("Trade offer denied and will be put up on tradecenter again!");
        item.status = TradeStatus.Waiting;
    }

}
