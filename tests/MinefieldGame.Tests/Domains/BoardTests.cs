using MinesweeperGame.Domain;

namespace MinefieldGame.Tests.Domains;

public class BoardTests
{
    [Fact]
    public void HasMine_Should_Return_True_When_Mine_Exists()
    {
        // Arrange
        var mines = new List<Position>
        {
            new(2, 2)
        };
        var board = new Board(5, 5, mines);

        // Act
        var result = board.HasMine(new Position(2, 2));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasMine_Should_Return_False_When_No_Mine_Exists()
    {
        // Arrange
        var random = new FakeRandomProvider(
        1, 1,
        2, 2,
        3, 3);
        var board = new Board(5, 5, 0, new(0, 0), random);

        // Act
        var result = board.HasMine(new Position(1, 1));

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 4)]
    [InlineData(2, 3)]
    public void IsInsideBoard_Should_Return_True_For_Valid_Positions(
        int row,
        int column)
    {
        // Arrange
        var random = new FakeRandomProvider(
        1, 1,
        2, 2,
        3, 3);
        var board = new Board(5, 5, 2, new(0, 0), random);

        // Act
        var result = board.IsInsideBoard(new Position(row, column));

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(5, 0)]
    [InlineData(0, 5)]
    public void IsInsideBoard_Should_Return_False_For_Invalid_Positions(
        int row,
        int column)
    {
        // Arrange
        var random = new FakeRandomProvider(
        1, 1,
        2, 2,
        3, 3);
        var board = new Board(5, 5, 2, new(0, 0), random);

        // Act
        var result = board.IsInsideBoard(new Position(row, column));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Board_Should_Generate_Expected_Mines()
    {
        // Arrange
        var random = new FakeRandomProvider(
            1, 1,
            2, 2,
            3, 3);

        var board = new Board(
            width: 5,
            height: 5,
            mineCount: 3,
            startPosition: new Position(0, 0),
            randomProvider: random);

        // Assert
        Assert.True(board.HasMine(new Position(1, 1)));
        Assert.True(board.HasMine(new Position(2, 2)));
        Assert.True(board.HasMine(new Position(3, 3)));
    }
}