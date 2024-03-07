using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TicTacToe.Core.ViewModels;

namespace Validation.ViewModels;

public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorsByPropertyName = [];

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => _errorsByPropertyName.Any();

    public IEnumerable GetErrors(string? propertyName) =>
        propertyName is not null && _errorsByPropertyName.TryGetValue(propertyName, out var value) ? value : Enumerable.Empty<string>();

    protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e) => ErrorsChanged?.Invoke(this, e);

    protected void AddError(string error, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
        {
            return;
        }

        if (!_errorsByPropertyName.TryGetValue(propertyName, out var value))
        {
            value = [];
            _errorsByPropertyName[propertyName] = value;
        }

        if (!value.Contains(error))
        {
            value.Add(error);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }

    protected void ClearErrors([CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
        {
            return;
        }

        if (!_errorsByPropertyName.ContainsKey(propertyName))
        {
            return;
        }

        _ = _errorsByPropertyName.Remove(propertyName);
        OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        RaisePropertyChanged(nameof(HasErrors));
    }
}
