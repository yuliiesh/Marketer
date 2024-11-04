namespace Marketer.Desktop.Commands;

public interface IReturningCommand<T>
{
    event EventHandler CanExecuteChanged;

    bool CanExecute(object parameter);

    Task<T> ExecuteAsync(object parameter);
}