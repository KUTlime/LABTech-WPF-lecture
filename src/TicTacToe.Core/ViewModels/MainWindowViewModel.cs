using System.Windows.Input;
using TicTacToe.Core.Commands;

namespace TicTacToe.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static readonly char[] _playersMarks = ['⭕', '❌'];
    private readonly char?[,] _boardState = new char?[3, 3];
    private BoardCellColors _boardCellColors;
    private BoardCellLabels _boardCellLabels;
    private bool _gameInProgress = true;
    private bool _showSettings = true;
    private char? _winner;
    private byte _turnCounter;
    private int _playerStartingPosition;
    private string _notes = string.Empty;

    public MainWindowViewModel()
    {
        CellUpdated = new DelegateCommand<byte[]>(CellUpdatedCommand, a => a.Length == 2 && a[0] >= 0 && a[0] <= 2 && a[1] >= 0 && a[1] <= 2);
        ResetBoard = new RelayCommand(ResetBoardCommand);
        CloseCommand = new RelayCommand(CloseApplicationCommand);
        SetStartingPlayer();
        _boardCellColors = new BoardCellColors();
        _boardCellLabels = new BoardCellLabels();
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
        get => _showSettings;
        set
        {
            _showSettings = value;
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

    public BoardCellLabels BoardCellLabels
    {
        get => _boardCellLabels;
        set
        {
            _boardCellLabels = value;
            RaisePropertyChanged();
        }
    }

    private void CellUpdatedCommand(byte[] coordinates)
    {
        if (_boardState[coordinates[0], coordinates[1]] is null && GameInProgress)
        {
            _boardState[coordinates[0], coordinates[1]] = GetPlayerCharacter();
            BoardCellLabels = BoardCellLabels[coordinates, GetPlayerCharacter()];

            if (IsThereAWinner())
            {
                GameInProgress = false;
                SettingsEnabled = true;
                Winner = GetWinner()[0];
                Notes = GetWinner();
                return;
            }

            if (TurnCounter == 8)
            {
                BoardCellColors = BoardCellColors.NoWinBoardCellState();
                GameInProgress = false;
                SettingsEnabled = true;
                Notes = "Nobody won";
                return;
            }

            GameInProgress = true;
            SettingsEnabled = false;
            Notes = GetNotesBasedOnTurn();
            _turnCounter += 1;
        }
    }

    private void ResetBoardCommand(object? obj)
    {
        GameInProgress = true;
        SettingsEnabled = true;
        ResetBoardState();
        BoardCellColors = new BoardCellColors();
        BoardCellLabels = new BoardCellLabels();
        SetStartingPlayer();
        TurnCounter = 0;
        Winner = null;
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

    private bool IsThereAWinner()
    {
        if (_boardState[0, 0] is not null && _boardState[0, 0] == _boardState[1, 1] && _boardState[1, 1] == _boardState[2, 2])
        {
            _winner = _boardState[0, 0];
            UpdateColors([[0, 0], [1, 1], [2, 2]]);
            return true;
        }

        if (_boardState[0, 2] is not null && _boardState[0, 2] == _boardState[1, 1] && _boardState[1, 1] == _boardState[2, 0])
        {
            _winner = _boardState[0, 2];
            UpdateColors([[0, 2], [1, 1], [2, 0]]);
            return true;
        }

        if (_boardState[0, 0] is not null && _boardState[0, 0] == _boardState[1, 0] && _boardState[1, 0] == _boardState[2, 0])
        {
            _winner = _boardState[0, 0];
            UpdateColors([[0, 0], [1, 0], [2, 0]]);
            return true;
        }

        if (_boardState[0, 0] is not null && _boardState[0, 0] == _boardState[0, 1] && _boardState[0, 1] == _boardState[0, 2])
        {
            _winner = _boardState[0, 0];
            UpdateColors([[0, 0], [0, 1], [0, 2]]);
            return true;
        }

        if (_boardState[2, 0] is not null && _boardState[2, 0] == _boardState[2, 1] && _boardState[2, 1] == _boardState[2, 2])
        {
            _winner = _boardState[2, 0];
            UpdateColors([[2, 0], [2, 1], [2, 2]]);
            return true;
        }

        if (_boardState[0, 2] is not null && _boardState[0, 2] == _boardState[1, 2] && _boardState[1, 2] == _boardState[2, 2])
        {
            _winner = _boardState[0, 2];
            UpdateColors([[0, 2], [1, 2], [2, 2]]);
            return true;
        }

        if (_boardState[0, 1] is not null && _boardState[0, 1] == _boardState[1, 1] && _boardState[1, 1] == _boardState[2, 1])
        {
            _winner = _boardState[0, 1];
            UpdateColors([[0, 1], [1, 1], [2, 1]]);
            return true;
        }

        if (_boardState[1, 0] is not null && _boardState[1, 0] == _boardState[1, 1] && _boardState[1, 1] == _boardState[1, 2])
        {
            _winner = _boardState[1, 0];
            UpdateColors([[1, 0], [1, 1], [1, 2]]);
            return true;
        }

        return false;
    }

    private string GetWinner() => $"{Winner} wins!";

    private void UpdateColors(byte[][] coordinates) => BoardCellColors = BoardCellColors[coordinates];

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
