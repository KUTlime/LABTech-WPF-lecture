namespace TicTacToe.Core.ViewModels;

public record BoardCellColors(
    BoardCellBackgroundState Color00 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color01 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color02 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color10 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color11 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color12 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color20 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color21 = BoardCellBackgroundState.Normal,
    BoardCellBackgroundState Color22 = BoardCellBackgroundState.Normal)
{
    public BoardCellColors this[byte[] coordinates] => coordinates switch
    {
        [0, 0] => this with { Color00 = BoardCellBackgroundState.WinMove },
        [0, 1] => this with { Color01 = BoardCellBackgroundState.WinMove },
        [0, 2] => this with { Color02 = BoardCellBackgroundState.WinMove },
        [1, 0] => this with { Color10 = BoardCellBackgroundState.WinMove },
        [1, 1] => this with { Color11 = BoardCellBackgroundState.WinMove },
        [1, 2] => this with { Color12 = BoardCellBackgroundState.WinMove },
        [2, 0] => this with { Color20 = BoardCellBackgroundState.WinMove },
        [2, 1] => this with { Color21 = BoardCellBackgroundState.WinMove },
        [2, 2] => this with { Color22 = BoardCellBackgroundState.WinMove },
        _ => this,
    };

    public BoardCellColors this[byte[][] winMoves] => winMoves switch
    {
        [[0, 0], [1, 1], [2, 2]] => this with { Color00 = BoardCellBackgroundState.WinMove, Color11 = BoardCellBackgroundState.WinMove, Color22 = BoardCellBackgroundState.WinMove },
        [[0, 2], [1, 1], [2, 0]] => this with { Color02 = BoardCellBackgroundState.WinMove, Color11 = BoardCellBackgroundState.WinMove, Color20 = BoardCellBackgroundState.WinMove },
        [[0, 0], [0, 1], [0, 2]] => this with { Color00 = BoardCellBackgroundState.WinMove, Color01 = BoardCellBackgroundState.WinMove, Color02 = BoardCellBackgroundState.WinMove },
        [[0, 0], [1, 0], [2, 0]] => this with { Color00 = BoardCellBackgroundState.WinMove, Color10 = BoardCellBackgroundState.WinMove, Color20 = BoardCellBackgroundState.WinMove },
        [[2, 0], [2, 1], [2, 2]] => this with { Color20 = BoardCellBackgroundState.WinMove, Color21 = BoardCellBackgroundState.WinMove, Color22 = BoardCellBackgroundState.WinMove },
        [[0, 2], [1, 2], [2, 2]] => this with { Color02 = BoardCellBackgroundState.WinMove, Color12 = BoardCellBackgroundState.WinMove, Color22 = BoardCellBackgroundState.WinMove },
        [[0, 1], [1, 1], [2, 1]] => this with { Color01 = BoardCellBackgroundState.WinMove, Color11 = BoardCellBackgroundState.WinMove, Color21 = BoardCellBackgroundState.WinMove },
        [[1, 0], [1, 1], [1, 2]] => this with { Color10 = BoardCellBackgroundState.WinMove, Color11 = BoardCellBackgroundState.WinMove, Color12 = BoardCellBackgroundState.WinMove },
        _ => this,
    };

    public static BoardCellColors NoWinBoardCellState() => new(
        Color00: BoardCellBackgroundState.NoWin,
        Color01: BoardCellBackgroundState.NoWin,
        Color02: BoardCellBackgroundState.NoWin,
        Color10: BoardCellBackgroundState.NoWin,
        Color11: BoardCellBackgroundState.NoWin,
        Color12: BoardCellBackgroundState.NoWin,
        Color20: BoardCellBackgroundState.NoWin,
        Color21: BoardCellBackgroundState.NoWin,
        Color22: BoardCellBackgroundState.NoWin);
}
