namespace MinesweeperGame.Domain;

public sealed class Board(int width, int height, IEnumerable<Position> mines)
{
    private readonly HashSet<Position> _mines = [.. mines];

    public int Width { get; } = width;
    public int Height { get; } = height;

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
}
