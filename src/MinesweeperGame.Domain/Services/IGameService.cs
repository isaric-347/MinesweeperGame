namespace MinesweeperGame.Domain.Services;

public interface IGameService
{
    bool IsFinished { get; }
    void Start();
    string GetStatus();
    Result Update(string input);
}
