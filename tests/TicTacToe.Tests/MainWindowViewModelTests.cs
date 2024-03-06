using FluentAssertions;
using TicTacToe.Core.ViewModels;
using TicTacToe.Tests;

namespace TicTacToe.Core.Tests;

public static class MainWindowViewModelTests
{
    public class CellUpdatedTests
    {
        [Fact(DisplayName = "X player should have turn after exactly one turn with default settings")]
        public void XPlayersTurnAfterOneTurnInDefaultSettingsTest()
        {
            var vm = new MainWindowViewModel();

            vm.CellUpdated.Execute(new byte[] { 0, 0 });

            _ = vm.SettingsEnabled.Should().BeFalse();
            _ = vm.GameInProgress.Should().BeTrue();
            _ = vm.TurnCounter.Should().Be(1);
            _ = vm.Notes.Should().Be("❌ player's turn");
        }

        [Fact(DisplayName = "O player should have turn after two turns with default settings")]
        public void OPlayerPlaysAfterTwoTurnsTest()
        {
            var vm = new MainWindowViewModel();

            vm.CellUpdated.Execute(new byte[] { 0, 0 });
            vm.CellUpdated.Execute(new byte[] { 0, 1 });

            _ = vm.SettingsEnabled.Should().BeFalse();
            _ = vm.GameInProgress.Should().BeTrue();
            _ = vm.TurnCounter.Should().Be(2);
            _ = vm.Notes.Should().Be("⭕ player's turn");
        }

        [Fact(DisplayName = "X player should have turn after three turns with default settings")]
        public void XPlayerPlaysAfterThreeTurnsTest()
        {
            var vm = new MainWindowViewModel();

            vm.CellUpdated.Execute(new byte[] { 0, 0 });
            vm.CellUpdated.Execute(new byte[] { 0, 1 });
            vm.CellUpdated.Execute(new byte[] { 1, 1 });

            _ = vm.SettingsEnabled.Should().BeFalse();
            _ = vm.GameInProgress.Should().BeTrue();
            _ = vm.TurnCounter.Should().Be(3);
            _ = vm.Notes.Should().Be("❌ player's turn");
        }


        [Fact(DisplayName = "O player should have turn after three turns with default settings")]
        public void OPlayerPlaysAfterFourTurnsTest()
        {
            var vm = new MainWindowViewModel();

            vm.CellUpdated.Execute(new byte[] { 0, 0 });
            vm.CellUpdated.Execute(new byte[] { 0, 1 });
            vm.CellUpdated.Execute(new byte[] { 1, 1 });
            vm.CellUpdated.Execute(new byte[] { 2, 1 });

            _ = vm.SettingsEnabled.Should().BeFalse();
            _ = vm.GameInProgress.Should().BeTrue();
            _ = vm.TurnCounter.Should().Be(4);
            _ = vm.Notes.Should().Be("⭕ player's turn");
        }

        [Theory(DisplayName = "Cell should have exactly two byte arguments")]
        [InlineData(new byte[] { 0 })]
        [InlineData(new byte[] { 0, 0, 0 })]
        [InlineData(new byte[] { 0, 0, 0, 0 })]
        [InlineData(new byte[] { 1 })]
        [InlineData(new byte[] { 1, 1, 1 })]
        [InlineData(new byte[] { 1, 1, 1, 1 })]
        [InlineData(new byte[] { 0, 1, 0 })]
        [InlineData(new byte[] { 1, 0, 1, 0 })]
        public void InvalidNumberOfArgumentsForCellUpdatedTest(byte[] input)
        {
            var vm = new MainWindowViewModel();

            bool result = vm.CellUpdated.CanExecute(input);

            _ = result.Should().BeFalse();
            _ = vm.TurnCounter.Should().Be(0);
        }

        [Theory(DisplayName = "Cell should have exactly two byte arguments")]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 0, 0, 0 })]
        [InlineData(new int[] { 0, 0, 0, 0 })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 1, 1 })]
        [InlineData(new int[] { 1, 1, 1, 1 })]
        [InlineData(new int[] { 0, 1, 0 })]
        [InlineData(new int[] { 1, 0, 1, 0 })]
        [InlineData(new int[] { 0, 0 })]
        [InlineData(new double[] { 0, 0 })]
        [InlineData(new uint[] { 0, 0 })]
        [InlineData(new long[] { 0, 0 })]
        [InlineData(new ulong[] { 0, 0 })]
        public void InvalidTypeOfArgumentsForCellUpdatedTest(object? input)
        {
            var vm = new MainWindowViewModel();

            bool result = vm.CellUpdated.CanExecute(input);

            _ = result.Should().BeFalse();
            _ = vm.TurnCounter.Should().Be(0);
        }

        [Theory(DisplayName = "Permited values for cell update function is 0, 1 or 2")]
        [InlineData(new byte[] { 3, 3 })]
        [InlineData(new byte[] { 1, 3 })]
        [InlineData(new byte[] { 3, 1 })]
        [InlineData(new byte[] { 4, 4 })]
        [InlineData(new byte[] { 1, 4 })]
        [InlineData(new byte[] { 4, 1 })]
        public void InvalidArgumentValuesForCellUpdatedTest(byte[] input)
        {
            var vm = new MainWindowViewModel();

            bool result = vm.CellUpdated.CanExecute(input);

            _ = result.Should().BeFalse();
            _ = vm.TurnCounter.Should().Be(0);
        }

        [Theory(DisplayName = "Valid turns wins the game")]
        [ClassData(typeof(WinningGameMovesData))]
        public void GameWinTest(byte[][] moves)
        {
            var vm = new MainWindowViewModel();
            foreach (byte[] winningMove in moves)
            {
                vm.CellUpdated.Execute(winningMove);
            }

            _ = vm.TurnCounter.Should().Be((byte)moves.Length);
            _ = vm.GameInProgress.Should().BeFalse();
            _ = vm.SettingsEnabled.Should().BeTrue();
            _ = vm.IsThereAWinner.Should().BeTrue();
        }
    }
}
