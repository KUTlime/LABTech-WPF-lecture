using MVVMDemo.Commands;
using System.Windows.Input;

namespace MVVMDemo.ViewModels;

public class MainViewModel : ViewModelBase
{
    private bool _isImageVisible;

    public MainViewModel() => ToggleImageCommand = new DelegateCommand(ToggleImage);

    public bool IsImageVisible
    {
        get => _isImageVisible;
        set
        {
            _isImageVisible = value;
            RaisePropertyChanged();
        }
    }

    public ICommand ToggleImageCommand { get; set; }

    private void ToggleImage(object? obj) =>
        IsImageVisible = !IsImageVisible;
}
