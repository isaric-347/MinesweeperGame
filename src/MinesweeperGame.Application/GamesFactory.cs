using MinesweeperGame.Application.Games;
using MinesweeperGame.Domain;
using MinesweeperGame.Domain.Params;
using MinesweeperGame.Domain.Services;

namespace MinesweeperGame.Application;

/// <summary>
/// Factory class to create games
/// </summary>
public static class GamesFactory
{
    /// <summary>
    /// Create game by type
    /// </summary>
    /// <param name="type">Game type</param>
    /// <param name="parameters">Game parameters</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IGameService CreateGame(GameType type, GameParams? parameters)
    {
        return type switch
        {
            GameType.Minefield when parameters is MinefieldParams p => CreateMinefieldGameDefault(p.BoardWidth, p.BoardHeight, p.PlayerLives, p.MineCount, p.StartPosition),
            _ => throw new ArgumentException($"Unsupported game type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Create Minefield game with default setup
    /// </summary>
    /// <param name="boardWidth"></param>
    /// <param name="boardHeight"></param>
    /// <param name="playerLives"></param>
    /// <param name="mineCount"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    private static MinefieldGameService CreateMinefieldGameDefault(
        int boardWidth, 
        int boardHeight, 
        int playerLives,
        int mineCount,
        Position startPosition)
    {
        var board = new Board(
            boardWidth,
            boardHeight,
            mineCount,
            startPosition
           );

        var player = new Player(
            startPosition: new Position(0, 0),
            lives: playerLives);

        return new MinefieldGameService(board, player);
    }
}
