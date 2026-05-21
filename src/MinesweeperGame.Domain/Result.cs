namespace MinesweeperGame.Domain;

/// <summary>
/// Result pattern
/// </summary>
/// <param name="Success"></param>
/// <param name="Message"></param>
public readonly record struct Result(bool Success, string Message);