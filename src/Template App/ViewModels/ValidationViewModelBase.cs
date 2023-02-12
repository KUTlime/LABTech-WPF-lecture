namespace TemplateApp.ViewModels;

public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();

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

        if (!_errorsByPropertyName.ContainsKey(propertyName))
        {
            _errorsByPropertyName[propertyName] = new List<string>();
        }

        if (_errorsByPropertyName[propertyName].Contains(error))
        {
            return;
        }

        _errorsByPropertyName[propertyName].Add(error);
        OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        RaisePropertyChanged(nameof(HasErrors));
    }

    protected void ClearErrors([CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
        {
            return;
        }

        if (_errorsByPropertyName.Remove(propertyName))
        {
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }
}
