namespace MinesweeperGame.Domain;

public sealed class Board
{
    private readonly HashSet<Position> _mines;

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
    }

    public Board(
        int width,
        int height,
        int mineCount,
        Position startPosition)
    {
        Width = width;
        Height = height;

        _mines = GenerateMines(mineCount, startPosition);
    }

    public bool HasMine(Position position)
    {
        return _mines.Contains(position);
    }

    public bool IsInsideBoard(Position position)
    {
        return position.Row >= 0 &&
               position.Row < Height &&
               position.Column >= 0 &&
               position.Column < Width;
    }

    private HashSet<Position> GenerateMines(
        int mineCount,
        Position startPosition)
    {
        var mines = new HashSet<Position>();
        var random = new Random();

        while (mines.Count < mineCount)
        {
            int row = random.Next(0, Height);
            int column = random.Next(0, Width);

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
