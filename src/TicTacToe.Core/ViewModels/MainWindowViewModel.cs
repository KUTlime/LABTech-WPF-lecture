using System.Windows.Input;
using TicTacToe.Core.Commands;

namespace TicTacToe.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static readonly char[] _playersMarks = ['⭕', '❌'];
    private readonly char?[,] _boardState = new char?[3, 3];
    private BoardCellColors _boardCellColors;
    private bool _gameInProgress;
    private char? _winner;
    private byte _turnCounter;
    private int _playerStartingPosition;
    private string _notes = string.Empty;
    private string _label00 = string.Empty;
    private string _label01 = string.Empty;
    private string _label02 = string.Empty;
    private string _label10 = string.Empty;
    private string _label11 = string.Empty;
    private string _label12 = string.Empty;
    private string _label20 = string.Empty;
    private string _label21 = string.Empty;
    private string _label22 = string.Empty;

    public MainWindowViewModel()
    {
        CellUpdated = new DelegateCommand<byte[]>(CellUpdatedCommand, a => a.Length == 2 && a[0] >= 0 && a[0] <= 2 && a[1] >= 0 && a[1] <= 2);
        ResetBoard = new RelayCommand(ResetBoardCommand);
        CloseCommand = new RelayCommand(CloseApplicationCommand);
        SetStartingPlayer();
        _boardCellColors = new BoardCellColors();
    }

    public ICommand CellUpdated { get; set; }

    public ICommand ResetBoard { get; set; }

    public ICommand CloseCommand { get; set; }

    public byte TurnCounter
    {
        get => _turnCounter;
        set
        {
            _turnCounter = value;
            RaisePropertyChanged();
        }
    }

    public string Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            RaisePropertyChanged();
        }
    }

    public bool GameInProgress
    {
        get => _gameInProgress;
        set
        {
            _gameInProgress = value;
            RaisePropertyChanged();
        }
    }

    public bool SettingsEnabled
    {
        get => !_gameInProgress;
        set
        {
            _gameInProgress = !value;
            RaisePropertyChanged();
        }
    }

    public char? Winner
    {
        get => _winner;
        set
        {
            _winner = value;
            RaisePropertyChanged();
        }
    }

    public BoardCellColors BoardCellColors
    {
        get => _boardCellColors;
        set
        {
            _boardCellColors = value;
            RaisePropertyChanged();
        }
    }

    public string Label00
    {
        get => _label00;
        set
        {
            _label00 = value;
            RaisePropertyChanged();
        }
    }

    public string Label01
    {
        get => _label01;
        set
        {
            _label01 = value;
            RaisePropertyChanged();
        }
    }

    public string Label02
    {
        get => _label02;
        set
        {
            _label02 = value;
            RaisePropertyChanged();
        }
    }

    public string Label10
    {
        get => _label10;
        set
        {
            _label10 = value;
            RaisePropertyChanged();
        }
    }

    public string Label11
    {
        get => _label11;
        set
        {
            _label11 = value;
            RaisePropertyChanged();
        }
    }

    public string Label12
    {
        get => _label12;
        set
        {
            _label12 = value;
            RaisePropertyChanged();
        }
    }

    public string Label20
    {
        get => _label20;
        set
        {
            _label20 = value;
            RaisePropertyChanged();
        }
    }

    public string Label21
    {
        get => _label21;
        set
        {
            _label21 = value;
            RaisePropertyChanged();
        }
    }

    public string Label22
    {
        get => _label22;
        set
        {
            _label22 = value;
            RaisePropertyChanged();
        }
    }

    private void CellUpdatedCommand(byte[] coordinates)
    {
        if (_boardState[coordinates[0], coordinates[1]] is null)
        {
            _boardState[coordinates[0], coordinates[1]] = GetPlayerCharacter();
            UpdateLabel(coordinates);

            if (IsThereAWinner())
            {
                UpdateColors(coordinates);
                GameInProgress = false;
                SettingsEnabled = true;
                Winner = GetWinner()[0];
                Notes = GetWinner();
            }

            GameInProgress = true;
            SettingsEnabled = false;
            Notes = GetNotesBasedOnTurn();
            _turnCounter += 1;
        }
    }

    private void ResetBoardCommand(object? obj)
    {
        GameInProgress = false;
        SettingsEnabled = true;
        ResetBoardState();
        SetStartingPlayer();
        TurnCounter = 0;
        Label00 = string.Empty;
        Label01 = string.Empty;
        Label02 = string.Empty;
        Label10 = string.Empty;
        Label11 = string.Empty;
        Label12 = string.Empty;
        Label20 = string.Empty;
        Label21 = string.Empty;
        Label22 = string.Empty;
    }

    private void ResetSettingsCommand(object? obj)
    {
        _ = obj;
        _playerStartingPosition = 0;
    }

    private void CloseApplicationCommand(object? obj)
    {
        _ = obj;
        _ = obj;
    }

    private void UpdateLabel(byte[] coordinates) => _ = coordinates switch
    {
        [0, 0] => Label00 = GetPlayerCharacter().ToString(),
        [0, 1] => Label01 = GetPlayerCharacter().ToString(),
        [0, 2] => Label02 = GetPlayerCharacter().ToString(),
        [1, 0] => Label10 = GetPlayerCharacter().ToString(),
        [1, 1] => Label11 = GetPlayerCharacter().ToString(),
        [1, 2] => Label12 = GetPlayerCharacter().ToString(),
        [2, 0] => Label20 = GetPlayerCharacter().ToString(),
        [2, 1] => Label21 = GetPlayerCharacter().ToString(),
        [2, 2] => Label22 = GetPlayerCharacter().ToString(),
        _ => string.Empty,
    };

    private bool IsThereAWinner() => _boardState switch
    {
        { [], [], [] } => true,
        _ => false,
    };

    private string GetWinner() => "X wins!";

    private void UpdateColors(byte[] coordinates) => BoardCellColors = BoardCellColors[coordinates];

    private void ResetBoardState()
    {
        _boardState[0, 0] = null;
        _boardState[0, 1] = null;
        _boardState[0, 2] = null;
        _boardState[1, 0] = null;
        _boardState[1, 1] = null;
        _boardState[1, 2] = null;
        _boardState[2, 0] = null;
        _boardState[2, 1] = null;
        _boardState[2, 2] = null;
    }

    private void SetStartingPlayer() => Notes = $"{_playersMarks[_playerStartingPosition]} player starts";

    private char GetPlayerCharacter() => _playersMarks[(_turnCounter % 2) + _playerStartingPosition];

    private string GetNotesBasedOnTurn() => $"{(GetPlayerCharacter() == _playersMarks[0] ? _playersMarks[1] : _playersMarks[0])} player's turn";
}
