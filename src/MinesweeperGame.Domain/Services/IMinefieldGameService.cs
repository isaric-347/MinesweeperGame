namespace MinesweeperGame.Domain.Services;

/// <summary>
/// Only for Minfield game
/// </summary>
public interface IMinefieldGameService : IGameService
{
    bool PlayerWon { get; }
    int MovesTaken { get; }
}
