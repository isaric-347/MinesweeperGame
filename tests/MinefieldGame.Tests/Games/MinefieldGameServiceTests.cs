using MinesweeperGame.Application.Games;
using MinesweeperGame.Domain;

namespace MinefieldGame.Tests.Games;

public class MinefieldGameServiceTests
{
    [Fact]
    public void Move_Right_Should_Update_Position()
    {
        // Arrange
        var random = new FakeRandomProvider(
        1, 1,
        2, 2,
        3, 3);
        var board = new Board(5, 5, 2, new(0, 0), random);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameService(board, player);

        // Act
        game.Update("R");

        // Assert
        Assert.Contains("B1", game.GetStatus());
    }

    [Fact]
    public void Move_Into_Mine_Should_Lose_Life()
    {
        // Arrange
        var mines = new List<Position>
    {
        new(0,1)
    };

        var board = new Board(5, 5, mines);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameService(board, player);

        // Act
        var result = game.Update("R");

        // Assert
        Assert.Contains("BOOM", result.Message);
        Assert.Contains("Lives: 2", game.GetStatus());
    }

    [Fact]
    public void Moving_Outside_Board_Should_Fail()
    {
        // Arrange
        var board = new Board(5, 5, []);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameService(board, player);

        // Act
        var result = game.Update("L");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Reaching_Last_Column_Should_Win_Game()
    {
        // Arrange
        var board = new Board(2, 2, []);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameService(board, player);

        // Act
        game.Update("R");

        // Assert
        Assert.True(game.PlayerWon);
    }

    [Fact]
    public void Invalid_Input_Should_Return_Error()
    {
        // Arrange
        var board = new Board(5, 5, []);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameService(board, player);

        // Act
        var result = game.Update("X");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid direction.", result.Message);
    }

    [Theory]
    [InlineData("u")]
    [InlineData("U")]
    [InlineData("d")]
    [InlineData("D")]
    [InlineData("l")]
    [InlineData("L")]
    [InlineData("r")]
    [InlineData("R")]
    public void Update_Should_Accept_Case_Insensitive_Input(string input)
    {
        // Arrange
        var board = new Board(5, 5, []);

        var player = new Player(
            new Position(2, 2),
            3);

        var game = new MinefieldGameService(board, player);

        // Act
        var result = game.Update(input);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void Winning_Update_Should_End_Game()
    {
        // Arrange
        var board = new Board(2, 2, []);

        var player = new Player(
            new Position(0, 0),
            3);

        var game = new MinefieldGameService(board, player);

        // Act
        game.Update("R");

        // Assert
        Assert.True(game.IsFinished);
        Assert.True(game.PlayerWon);
    }
}