namespace MinesweeperGame.Domain.Services;

public interface IMinefieldGameService : IGameService
{
    bool PlayerWon { get; }
    int MovesTaken { get; }
}
