using MinesweeperGame.Domain.Constants;

namespace MinesweeperGame.Domain;

/// <summary>
///  Position obj as immutable data container
/// </summary>
/// <param name="Row"></param>
/// <param name="Column"></param>
public readonly record struct Position(int Row, int Column)
{
    // Move position in new direction
    public Position Move(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Position(Row - 1, Column),
            Direction.Down => new Position(Row + 1, Column),
            Direction.Left => new Position(Row, Column - 1),
            Direction.Right => new Position(Row, Column + 1),
            _ => this
        };
    }

    public string ToChessNotation()
    {
        char columnLetter = (char)('A' + Column);
        return $"{columnLetter}{Row + 1}";
    }
}
