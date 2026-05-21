namespace MinesweeperGame.Domain.Services;

/// <summary>
/// Common for all services
/// </summary>
public interface IGameService
{
    bool IsFinished { get; }
    void Start();
    string GetStatus();
    Result Update(string input);
}
