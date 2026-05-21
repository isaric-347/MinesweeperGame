using MinesweeperGame.Domain.Providers;

namespace MinefieldGame.Tests;

public class FakeRandomProvider : IRandomProvider
{
    private readonly Queue<int> _values;

    public FakeRandomProvider(params int[] values)
    {
        _values = new Queue<int>(values);
    }

    public int Next(int minValue, int maxValue)
    {
        return _values.Dequeue();
    }
}