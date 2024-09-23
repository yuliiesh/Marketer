namespace Marketer.Menu;

public class MenuBuilder
{
    public IReadOnlyList<MenuItem> Build()
    {
        IReadOnlyList<MenuItem> items =
        [
            new()
            {
                Text = "text",
                Action = () => Console.WriteLine("action1")
            },
            new()
            {
                Text = "hhhh",
                Action = () => Console.WriteLine("action2")
            },
            new()
            {
                Text = "pchol",
                Action = () => Environment.Exit(0)
            }
        ];

        return items;
    }
}