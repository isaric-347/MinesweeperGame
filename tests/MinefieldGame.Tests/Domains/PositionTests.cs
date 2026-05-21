using MinesweeperGame.Domain;

namespace MinefieldGame.Tests.Domains;

public class PositionTests
{
    [Fact]
    public void Move_Up_Should_Decrease_Row()
    {
        // Arrange
        var position = new Position(3, 3);

        // Act
        var result = position.Move(Direction.Up);

        // Assert
        Assert.Equal(new Position(2, 3), result);
    }

    [Fact]
    public void Move_Down_Should_Increase_Row()
    {
        // Arrange
        var position = new Position(3, 3);

        // Act
        var result = position.Move(Direction.Down);

        // Assert
        Assert.Equal(new Position(4, 3), result);
    }

    [Fact]
    public void Move_Left_Should_Decrease_Column()
    {
        // Arrange
        var position = new Position(3, 3);

        // Act
        var result = position.Move(Direction.Left);

        // Assert
        Assert.Equal(new Position(3, 2), result);
    }

    [Fact]
    public void Move_Right_Should_Increase_Column()
    {
        // Arrange
        var position = new Position(3, 3);

        // Act
        var result = position.Move(Direction.Right);

        // Assert
        Assert.Equal(new Position(3, 4), result);
    }

    [Theory]
    [InlineData(0, 0, "A1")]
    [InlineData(0, 1, "B1")]
    [InlineData(1, 0, "A2")]
    [InlineData(7, 7, "H8")]
    public void ToChessNotation_Should_Return_Expected_Value(
        int row,
        int column,
        string expected)
    {
        // Arrange
        var position = new Position(row, column);

        // Act
        var result = position.ToChessNotation();

        // Assert
        Assert.Equal(expected, result);
    }
}
