namespace MinesweeperGame.Domain.Providers;

/// <summary>
/// Provides methods for generating random numbers.
/// </summary>
public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new();
    public int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
}
