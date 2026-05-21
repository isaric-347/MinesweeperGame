using MinesweeperGame.Domain.Providers;

namespace MinesweeperGame.Domain;

/// <summary>
/// Represents a rectangular game board with a specified width and height, containing mines at defined or randomly
/// generated positions.
/// </summary>
/// <remarks>Supports checking for mines and validating whether a position is within the board's boundaries. Mines
/// can be initialized explicitly or generated randomly, excluding a designated starting position.</remarks>
public sealed class Board
{
    private readonly HashSet<Position> _mines;
    private readonly IRandomProvider _randomProvider;

    public int Width { get; }
    public int Height { get; }

    public Board(
        int width,
        int height,
        IEnumerable<Position> mines)
    {
        Width = width;
        Height = height;
        _mines = [.. mines];
        _randomProvider = new RandomProvider();
    }

    public Board(
        int width,
        int height,
        int mineCount,
        Position startPosition,
        IRandomProvider randomProvider)
    {
        Width = width;
        Height = height;
        _randomProvider = randomProvider;

        _mines = GenerateMines(mineCount, startPosition);
    }

    /// <summary>
    /// Validate if postiion has mine
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool HasMine(Position position)
    {
        return _mines.Contains(position);
    }

    /// <summary>
    /// Validate if postiion is in board
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsInsideBoard(Position position)
    {
        return position.Row >= 0 &&
               position.Row < Height &&
               position.Column >= 0 &&
               position.Column < Width;
    }

    /// <summary>
    /// Generates unique mine positions for the board, excluding the specified starting position.
    /// </summary>
    /// <param name="mineCount">The number of mines to generate.</param>
    /// <param name="startPosition">The position on the board where a mine must not be placed.</param>
    /// <returns>A set of positions representing mine locations.</returns>
    private HashSet<Position> GenerateMines(
        int mineCount,
        Position startPosition)
    {
        var mines = new HashSet<Position>();
        while (mines.Count < mineCount)
        {
            int row = _randomProvider.Next(0, Height);
            int column = _randomProvider.Next(0, Width);
            var minePosition = new Position(row, column);
            // Prevent mine on starting cell
            if (minePosition == startPosition)
            {
                continue;
            }

            mines.Add(minePosition);
        }

        return mines;
    }
}
