namespace Marketer.Menu;

public class Menu : IMenu
{
    private readonly IReadOnlyList<MenuItem> _items;

    public Menu(IReadOnlyList<MenuItem> items)
    {
        _items = items;
    }

    public void Display()
    {
        Console.WriteLine("Welcome to Product Company!");
        ShowItems(_items);
        HandleUserInput();
    }

    private void ShowItems(IEnumerable<MenuItem> items)
    {
        int i = 1;
        foreach (var item in items)
        {
            Console.WriteLine($"{i++}. {item.Text}");
        }
    }

    private void HandleUserInput()
    {
        Console.WriteLine("Enter Number: ");
        string userInput = Console.ReadLine();
        int input;
        while (!(int.TryParse(userInput, out input) && input >= 0 && input <= _items.Count))
        {
            Console.WriteLine("You pick the wrong number. Try again.: ");
            userInput = Console.ReadLine();
        }

        _items[input-1].Execute();
    }
}

public class MenuItem
{
    public required string Text { get; set; }
    public required Action Action { get; set; }
    public IReadOnlyCollection<MenuItem> SubItems { get; set; }

    public void Execute()
    {
        Action();
    }
}