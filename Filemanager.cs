using System.Reflection.Metadata.Ecma335;

namespace App;


//handles user and trades data thru text files
public class Filemanager
{
    private string Userfile = "users.txt";

    // isnt used
    private string Tradefile = "trades.txt";

    private string CompletedTradefile = "completedtrades.txt";

    //loading users
    public void LoadUsers(List<User> users)
    {
        //reading all lines in text file
        string[] lines = File.ReadAllLines(Userfile);

        //loops thru text file
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "")
            {
                continue;
            }
            else
            {
                // sets a pattern for the file
                string[] parts = lines[i].Split(',');
                string username = parts[0];
                string password = parts[1];
                bool IsLoggedIn = bool.Parse(parts[2]);
                // adds the user from the text file to list when loading in
                users.Add(new User(username, password, IsLoggedIn));
            }

        }

    }

    // saves a new user
    public void SaveUsers(List<User> users)
    {
        //list to make a string that the text file can store
        List<string> linestowrite = new List<string>();

        for (int i = 0; i < users.Count; i++)
        {
            //sets pattern of how text file looks
            string line = users[i].Username + "," + users[i].Password + "," + users[i].IsLoggedIn;
            linestowrite.Add(line);
        }

        File.WriteAllLines(Userfile, linestowrite.ToArray());
    }

    //for bug fixing to show if it gets added new users in text file
    public void ShowUserFile()
    {
        string[] userlines = File.ReadAllLines(Userfile);
        foreach (string line in userlines)
        {
            Console.WriteLine(line);
        }
    }
    // updates state of users
    public void UpdateUser(List<User> users, string username, string password, bool isloggedin, User CurrentUser)
    {
        bool UserFound = false;
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Username == username && users[i].Password == password || users[i].Username == CurrentUser.Username && users[i].Password == CurrentUser.Password)
            {
                users[i].IsLoggedIn = isloggedin;
                UserFound = true;
                Console.WriteLine($"user {username} updated!");
                break;
            }
        }
        if (UserFound)
        {
            SaveUsers(users);
        }
        else
        {
            Console.WriteLine("no user to update!");
        }

    }
    // looking if there is duplicates of users when trying to create a new user
    public bool CheckDuplicates(List<User> users, string username, string password)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Username == username && users[i].Password == password)
            {
                return true;
            }

        }
        return false;
    }
    // saves completed trades to text file
    public void SaveCompletedTrades(List<Item> completedtrades)
    {
        List<string> lines = new List<string>();
        foreach (var trade in completedtrades)
        {
            //sets pattern to store
            string line = $"{trade.Name},{trade.Owner.Username},{trade.Lastowner.Username},{trade.TradeRequest.Username},{trade.status}";
            lines.Add(line);
        }
        // adds the above
        File.WriteAllLines("completedtrades.txt", lines);
        Console.WriteLine("Saved trade");
        Console.ReadLine();
    }

    //load trades by looking at text file to make new user to those items and re adding them to completed trades list
    public void LoadCompletedTrades(List<Item> completedtrades, List<User> users)
    {
        string[] lines = File.ReadAllLines("completedtrades.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "")
            {
                continue;
            }
            string[] parts = lines[i].Split(',');
            if (parts.Length < 5)
            {
                continue;
            }
            string name = parts[0];
            string ownername = parts[1];
            string lastownername = parts[2];
            string requestername = parts[3];
            string status = parts[4];

            User owner = new User(ownername, "", false);
            User lastowner = new User(lastownername, "", false);
            User requester = new User(requestername, "", false);

            Item item = new Item(name, "", owner);
            item.Lastowner = lastowner;
            item.TradeRequest = requester;
            item.status = TradeStatus.Approved;

            completedtrades.Add(item);
            Console.WriteLine("Trades Loaded");
            Console.ReadLine();
        }
    }

}
