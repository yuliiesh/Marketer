namespace Marketer.Menu;

public class Menu : IMenu
{
    private readonly Stack<IReadOnlyList<MenuItem>> _menuStack = new(2);

    private int _currentSelection;

    public Func<bool> LogoutPressed { get; set; }

    public Menu(IReadOnlyList<MenuItem> mainMenuItems)
    {
        if (mainMenuItems is null ||  !mainMenuItems.Any())
        {
            throw new ArgumentException("Main menu items cannot be null or empty.");
        }

        _menuStack.Push(mainMenuItems);
    }

    public async Task Display()
    {
        ConsoleKeyInfo keyInfo;
        do
        {
            if (LogoutPressed is not null && LogoutPressed())
            {
                LogoutPressed = null;
                _currentSelection = 0;
                break;
            }

            RenderMenu();

            keyInfo = Console.ReadKey(true);
            await HandleInput(keyInfo);
        } while (keyInfo.Key != ConsoleKey.Escape || _menuStack.Count >= 1);
    }

    private async Task HandleInput(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                _currentSelection = (_currentSelection == 0) ? GetCurrentMenuItems().Count - 1 : _currentSelection - 1;
                break;
            case ConsoleKey.DownArrow:
                _currentSelection = (_currentSelection + 1) % GetCurrentMenuItems().Count;
                break;
            case ConsoleKey.Enter:
                await ExecuteCurrentSelection();
                break;
            case ConsoleKey.Escape:
                ExitSubMenu();
                break;
        }
    }

    private async Task ExecuteCurrentSelection()
    {
        var selectedItem = GetCurrentMenuItems()[_currentSelection];
        if (selectedItem?.SubItems?.Count > 0)
        {
            EnterSubMenu(selectedItem.SubItems);
        }
        else
        {
            await selectedItem?.Execute();
        }
    }

    private void EnterSubMenu(IReadOnlyList<MenuItem> subItems)
    {
        if (subItems is null || !subItems.Any())
        {
            return;
        }

        _menuStack.Push(subItems);
        _currentSelection = 0;
    }

    private void ExitSubMenu()
    {
        if (_menuStack.Count <= 1)
        {
            return;
        }

        _menuStack.Pop();
        _currentSelection = 0;
    }

    private IReadOnlyList<MenuItem> GetCurrentMenuItems() => _menuStack.Peek();

    private void RenderMenu()
    {
        Console.Clear();
        var menuItems = GetCurrentMenuItems();

        for (var i = 0; i < menuItems.Count; i++)
        {
            if (i == _currentSelection)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.ResetColor();
            }

            Console.WriteLine(menuItems[i].Title);
        }

        Console.ResetColor();
    }
}