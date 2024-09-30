namespace Marketer.Menu;

public interface IMenu
{
    Task Display();

    Func<bool> LogoutPressed { get; set; }
}