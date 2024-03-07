namespace TicTacToe.Core.ViewModels;

public record BoardCellLabels(
    string Label00 = "",
    string Label01 = "",
    string Label02 = "",
    string Label10 = "",
    string Label11 = "",
    string Label12 = "",
    string Label20 = "",
    string Label21 = "",
    string Label22 = "")
{
    public BoardCellLabels this[byte[] coordinates, char label] => coordinates switch
    {
        [0, 0] => this with { Label00 = label.ToString() },
        [0, 1] => this with { Label01 = label.ToString() },
        [0, 2] => this with { Label02 = label.ToString() },
        [1, 0] => this with { Label10 = label.ToString() },
        [1, 1] => this with { Label11 = label.ToString() },
        [1, 2] => this with { Label12 = label.ToString() },
        [2, 0] => this with { Label20 = label.ToString() },
        [2, 1] => this with { Label21 = label.ToString() },
        [2, 2] => this with { Label22 = label.ToString() },
        _ => this,
    };
}
