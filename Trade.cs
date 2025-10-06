
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
        int userinputindex = Convert.ToInt32(userinput);
        foreach (var listing in tradecenter)
        {
            if (userinputindex == listing.Key.Id)
            {
                if (CurrentUser.Username == listing.Key.Owner.Username)
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
        item.status = TradeStatus.Requested;
        item.TradeRequest = CurrentUser;
        Console.ReadLine();
    }

    public void AcceptTradeOffer(Item item, Dictionary<Item, User> tradecenter)
    {
        //Console.WriteLine("Trade offer Accepted!");

        item.status = TradeStatus.Approved;

        item.Lastowner = item.Owner;
        item.Owner = item.TradeRequest;



        tradecenter[item] = item.Owner;
        item.status = TradeStatus.Approved;
        //Console.WriteLine($"{item.Owner.Username} + {item.Name} + {item.TradeRequest.Username} + {item.status}");
        Console.WriteLine("trade approved!");
        tradecenter.Remove(item);
        Console.WriteLine("item deleted from tradecenter");
        completedtrades.Add(item);
        Console.WriteLine($"Item added to yours and {item.Owner.Username} trades history");
        Console.ReadLine();

        /*if (item.status == TradeStatus.Approved && CurrentUser.Username == item.TradeRequest && localid == id)
        {
            item.Username = CurrentUser.Username;
            Console.WriteLine("Trade Approved");
        }
*/

    }



    public void DenyTradeOffer(Item item)
    {
        item.status = TradeStatus.Denied;
        Console.WriteLine("Trade offer denied and will be put up on tradecenter again!");
        item.status = TradeStatus.Waiting;
    }

}
