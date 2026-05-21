namespace MinesweeperGame.Domain.Providers;

public interface IRandomProvider
{
    int Next(int min, int max);
}
