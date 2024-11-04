using System.Windows.Input;

namespace Marketer.Desktop.Commands;

public class ReturningCommand<T> : IReturningCommand<T>
{
    private readonly Func<object, bool> _canExecute;
    private readonly Func<object, Task<T>> _execute;

    public ReturningCommand(Func<object, Task<T>> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? (_ => true);
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute(parameter);

    public async Task<T> ExecuteAsync(object parameter) => await _execute(parameter);
}