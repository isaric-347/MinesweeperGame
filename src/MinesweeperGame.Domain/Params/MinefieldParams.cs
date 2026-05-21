namespace MinesweeperGame.Domain.Params;

/// <summary>
/// Encapsulates configuration parameters for a minesweeper game board.
/// </summary>
/// <param name="BoardWidth">The width of the minefield, in cells.</param>
/// <param name="BoardHeight">The height of the minefield, in cells.</param>
/// <param name="PlayerLives">The number of lives available to the player.</param>
/// <param name="MineCount">The total number of mines placed on the board.</param>
/// <param name="StartPosition">The initial position of the player on the board.</param>
public sealed record MinefieldParams(
    int BoardWidth,
    int BoardHeight,
    int PlayerLives,
    int MineCount,
    Position StartPosition) : GameParams;