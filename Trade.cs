namespace App;

public class Trade
{

    public void ShowRequestedItems(Dictionary<Item, User> tradecenter, User CurrentUser)
    {
        Console.WriteLine($"----------------Your Requests-------------------");
        foreach (var listing in tradecenter)
        {
            if (listing.Key.status == TradeStatus.Requested && listing.Value.Username == CurrentUser.Username)
            {
                Console.WriteLine($"Id: {listing.Key.Id} Item: {listing.Key.Name} Requested by: {listing.Key.TradeRequest}");
            }
            else
            {
                continue;
            }
        }
    }
    public void ShowUserItems(Dictionary<Item, User> tradecenter, User CurrentUser)
    {
        Console.WriteLine("----------------Your Listings-------------------");
        foreach (var listing in tradecenter)
        {
            if (CurrentUser.Username == listing.Value.Username)
            {
                continue;
            }
            else
            {
                Console.WriteLine($"Id: {listing.Key.Id} Item: {listing.Key.Name} Status: {listing.Key.status}");
            }
            break;
        }
    }
    public void ShowTradecenter(Dictionary<Item, User> tradecenter, User CurrentUser)
    {
        Console.WriteLine("----------------Tradecenter-------------------");
        foreach (var listing in tradecenter)
        {
            if (CurrentUser.Username == listing.Value.Username)
            {
                Console.WriteLine($"Listing Id: {listing.Key.Id}  Item: {listing.Key.Name} Status: {listing.Value.Username} ");
            }
            else
            {
                continue;
            }
            break;
        }
    }

}