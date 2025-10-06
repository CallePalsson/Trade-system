namespace App;

public class Filemanager
{
    private string Userfile = "users.txt";
    private string Tradefile = "trades.txt";
    public void LoadUsers(List<User> users)
    {
        string[] lines = File.ReadAllLines(Userfile);

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "")
            {
                continue;
            }
            else
            {
                string[] parts = lines[i].Split(',');
                string username = parts[0];
                string password = parts[1];
                bool IsLoggedIn = bool.Parse(parts[2]);
                users.Add(new User(username, password, IsLoggedIn));
            }

        }

    }
    public void SaveUsers(List<User> users)
    {
        List<string> linestowrite = new List<string>();

        for (int i = 0; i < users.Count; i++)
        {
            string line = users[i].Username + "," + users[i].Password + "," + users[i].IsLoggedIn;
            linestowrite.Add(line);
        }
        File.WriteAllLines(Userfile, linestowrite.ToArray());
    }
    public void Showtxt()
    {
        string[] userlines = File.ReadAllLines(Userfile);
        foreach (string line in userlines)
        {
            Console.WriteLine(line);
        }
    }

}
