using System.Windows.Input;

namespace TicTacToe.Core.Commands;

public class DelegateCommand<TParameter> : ICommand
{
    private readonly Action<TParameter> _execute;
    private readonly Func<TParameter, bool>? _canExecute;

    public DelegateCommand(Action<TParameter> execute, Func<TParameter, bool>? canExecute = null)
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
    public bool CanExecute(TParameter parameter) => _canExecute is null || _canExecute(parameter);

    public bool CanExecute(object? parameter) => parameter is TParameter param && CanExecute(param);

    public void Execute(TParameter parameter) => _execute(parameter);

    public void Execute(object? parameter)
    {
        if (parameter is TParameter param)
        {
            Execute(param);
        }
    }

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
