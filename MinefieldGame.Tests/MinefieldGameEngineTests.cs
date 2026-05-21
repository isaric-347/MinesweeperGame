using MinesweeperGame.Application;
using MinesweeperGame.Domain;

namespace MinefieldGame.Tests;

public class MinefieldGameEngineTests
{
    [Fact]
    public void Move_Right_Should_Update_Position()
    {
        // Arrange
        var board = new Board(5, 5, new List<Position>());
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameEngine(board, player);

        // Act
        game.Move("R");

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

        var game = new MinefieldGameEngine(board, player);

        // Act
        var result = game.Move("R");

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

        var game = new MinefieldGameEngine(board, player);

        // Act
        var result = game.Move("L");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Reaching_Last_Column_Should_Win_Game()
    {
        // Arrange
        var board = new Board(2, 2, []);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameEngine(board, player);

        // Act
        game.Move("R");

        // Assert
        Assert.True(game.PlayerWon);
    }

    [Fact]
    public void Invalid_Input_Should_Return_Error()
    {
        // Arrange
        var board = new Board(5, 5, []);
        var player = new Player(new Position(0, 0), 3);

        var game = new MinefieldGameEngine(board, player);

        // Act
        var result = game.Move("X");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid direction.", result.Message);
    }
}