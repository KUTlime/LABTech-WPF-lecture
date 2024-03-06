namespace MVVMFontSizeDemo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private double _fontSize = 20;

    public double FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            RaisePropertyChanged();
        }
    }
}
