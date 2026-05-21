namespace MinesweeperGame.Domain;

/// <summary>
/// 
/// </summary>
/// <param name="startPosition"></param>
/// <param name="lives"></param>
public sealed class Player(Position startPosition, int lives)
{
    public Position Position { get; private set; } = startPosition;
    public int Lives { get; private set; } = lives;
    public int MovesTaken { get; private set; }

    public void MoveTo(Position position)
    {
        Position = position;
        MovesTaken++;
    }

    public void LoseLife()
    {
        Lives--;
    }
}
