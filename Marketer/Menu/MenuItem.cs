namespace Marketer.Menu;

public class MenuItem
{
    public required string Title { get; set; }
    public Func<Task> Action { get; set; }
    public IReadOnlyList<MenuItem> SubItems { get; set; }

    public async Task Execute()
    {
        await Action();
    }
}