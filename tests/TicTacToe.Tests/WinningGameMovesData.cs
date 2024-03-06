using System.Collections;

namespace TicTacToe.Tests;

internal class WinningGameMovesData : IEnumerable<object[]>
{
    private List<object[]> Data => new()
    {
         new object[] { new byte[][] { [0, 0], [1, 1], [2, 2] } }, // X 0 0 | 0 X 0 | 0 X 0
         new object[] { new byte[][] { [2, 2], [1, 1], [0, 0] } },
         new object[] { new byte[][] { [2, 0], [1, 1], [0, 2] } }, // 0 0 X | 0 X 0 | X 0 0
         new object[] { new byte[][] { [0, 2], [1, 1], [2, 0] } },
         new object[] { new byte[][] { [0, 0], [0, 1], [0, 2] } }, // X X X | 0 0 0 | 0 0 0
         new object[] { new byte[][] { [0, 2], [0, 1], [0, 0] } },
         new object[] { new byte[][] { [0, 0], [1, 0], [2, 0] } }, // X 0 0 | X 0 0 | X 0 0
         new object[] { new byte[][] { [2, 0], [1, 0], [0, 0] } },
         new object[] { new byte[][] { [2, 0], [2, 1], [2, 2] } }, // 0 0 0 | 0 0 0 | X X X
         new object[] { new byte[][] { [2, 2], [2, 1], [2, 0] } },
    };

    public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
