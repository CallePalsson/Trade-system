namespace App;

public class Item
{
    public static int IdCount = 1;
    public int Id;
    public string Name;
    public string Description;
    public TradeStatus status;
    public User TradeRequest;
    public User Owner;
    public User Lastowner;

    public Item(string name, string description, User CurrentUser)
    {
        Id = IdCount++;
        Name = name;
        Description = description;
        Owner = CurrentUser;

    }
    public bool IsOwner(User user)
    {
        return Owner == user;
    }
    // public static void ShowItem(string Name,int Id,TradeStatus status)
    // {
    //     Console.WriteLine($"Id: ({Id}) Item: ({Name}) Status: ({status})");
    // }
}


