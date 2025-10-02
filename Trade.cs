using System.Security.Cryptography.X509Certificates;

namespace App;

public class Trade
{
    public Dictionary<Item, User> tradecenter = new Dictionary<Item, User>();
    public List<Item> traderequests = new List<Item>();


    public void TradecenterAdd(Item item, User user)
    {
        tradecenter.Add(item, user);
    }
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
            if (CurrentUser.Username == listing.Value.Username)
            {
                Console.WriteLine($"Listing Id: {listing.Key.Id}  Item: {listing.Key.Name} Trader: {listing.Value.Username} ");
            }
            else
            {
                continue;
            }
    }
    public void ShowTradeOffer(User CurrentUser)
    {
        Console.WriteLine("----------------Trade Requests-------------------");
        foreach (var listing in tradecenter)
        {

            if (listing.Key.status == TradeStatus.Requested && CurrentUser.Username != listing.Value.Username)
            {
                Console.WriteLine($"Id: {listing.Key.Id} ({listing.Key.Name}) Trade request sent by {listing.Key.TradeRequest}");
            }

            else
            {
                continue;
            }
        }
    }
    public void ShowTradeRequests()
    {

    }
    public void InspectListing(User CurrentUser, string userinput)
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

    }
    public void SendTradeOffer()
    {

    }

    public void AcceptTradeOffer()
    {

    }

    public void DenyTradeOffer()
    {

    }
}