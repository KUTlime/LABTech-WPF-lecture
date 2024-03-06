using System.Windows.Input;

namespace MVVMFontSizeDemo.Commands;

public class DelegateCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// The _canExecute is null when command can be executed every single time, so default value is null that case.
    /// </summary>
    /// <param name="parameter">An input parameter.</param>
    /// <returns>A boolean value that indicates either command can be or cannot be executed.</returns>
    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

    public void Execute(object? parameter) => _execute(parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
