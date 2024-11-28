namespace ContactListApp.Services;

public class MenuDialogs
{
    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine($"{"1.",-5} Create");
        Console.WriteLine($"{"2.",-5} View");
        Console.WriteLine("-------------------------");
        Console.Write("Choose your menu option: ");
        var option = Console.ReadLine();
    }
}

