namespace App;

public class Item
{
    public static int IdCount = 1;
    public int Id;
    public string Name;
    public string Description;

    public Item(string name, string description)
    {
        Id = IdCount++;
        Name = name;
        Description = description; 
    }
}