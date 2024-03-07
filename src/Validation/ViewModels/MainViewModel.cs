namespace Validation.ViewModels;

public class MainViewModel : ValidationViewModelBase
{
    private string _numericValue = "10";

    public string NumericValue
    {
        get => _numericValue;
        set
        {
            if (_numericValue == value)
            {
                return;
            }

            _numericValue = value;
            RaisePropertyChanged();

            if (!double.TryParse(value, out _))
            {
                AddError($"Invalid numeric value '{value}'");
            }
            else
            {
                ClearErrors();
            }
        }
    }
}
