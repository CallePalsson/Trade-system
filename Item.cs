namespace App;

public class Item
{
    public static int IdCount = 1;
    public int Id;
    public string Name;
    public string Description;
    public TradeStatus status;
    public string TradeRequest;

    public Item(string name, string description)
    {
        Id = IdCount++;
        Name = name;
        Description = description;
    }
    // public static void ShowItem(string Name,int Id,TradeStatus status)
    // {
    //     Console.WriteLine($"Id: ({Id}) Item: ({Name}) Status: ({status})");
    // }
}


