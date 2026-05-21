using MinesweeperGame.Domain;
using MinesweeperGame.Domain.Constants;
using MinesweeperGame.Domain.Services;

namespace MinesweeperGame.Application.Games;

/// <summary>
/// Service to handle Minefield game
/// </summary>
/// <param name="board"></param>
/// <param name="player"></param>
public sealed class MinefieldGameService(Board board, Player player) : IMinefieldGameService
{
    /// <summary>
    /// Player is finished if is won or no more lives
    /// </summary>
    public bool IsFinished => PlayerWon || player.Lives <= 0;
    /// <summary>
    /// Player is win if reach the other side of the board
    /// </summary>
    public bool PlayerWon => player.Position.Column == board.Width - 1;
    /// <summary>
    /// How many moves are taken
    /// </summary>
    public int MovesTaken => player.MovesTaken;

    /// <summary>
    /// Start console game
    /// </summary>
    public void Start()
    {
        Console.WriteLine("=====================================");
        Console.WriteLine("        MINEFIELD GAME");
        Console.WriteLine("=====================================");
        Console.WriteLine("Reach the RIGHT side of the board.");
        Console.WriteLine("Avoid hidden mines.");
        Console.WriteLine("Controls: U, D, L, R");
        Console.WriteLine();

        while (!this.IsFinished)
        {
            Console.WriteLine(this.GetStatus());
            Console.Write("Move (U/D/L/R): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            var result = this.Update(input);
            Console.ForegroundColor = result.Success ? ConsoleColor.White : ConsoleColor.Red;
            Console.WriteLine(result.Message);
            Console.WriteLine();
            Console.ResetColor();
        }

        Console.WriteLine("=====================================");
        Console.ForegroundColor = this.PlayerWon ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(this.PlayerWon
            ? $"YOU WON in {this.MovesTaken} moves!"
            : "GAME OVER");
        Console.WriteLine("=====================================");
        Console.ResetColor();
    }

    /// <summary>
    /// Get current player status
    /// </summary>
    /// <returns></returns>
    public string GetStatus()
    {
        return $"Position: {player.Position.ToChessNotation()} | " +
               $"Lives: {player.Lives} | " +
               $"Moves: {player.MovesTaken}";
    }

    /// <summary>
    /// Updates the player's position based on the specified direction input and handles game state changes such as mine
    /// collisions and win conditions.
    /// </summary>
    /// <param name="input">The direction to move the player.</param>
    /// <returns>A Result indicating the outcome of the move operation, including success or failure and an appropriate message.</returns>
    public Result Update(string input)
    {
        if (!TryParseDirection(input, out var direction))
        {
            return new Result(false, "Invalid direction.");
        }

        var newPosition = player.Position.Move(direction);
        if (!board.IsInsideBoard(newPosition))
        {
            return new Result(false, "Cannot move outside the board.");
        }

        player.MoveTo(newPosition);

        if (board.HasMine(newPosition))
        {
            player.LoseLife();

            if (player.Lives <= 0)
            {
                return new Result(
                    false,
                    $"BOOM! Mine hit at {newPosition.ToChessNotation()}. No lives remaining.");
            }

            return new Result(
                false,
                $"BOOM! Mine hit at {newPosition.ToChessNotation()}. Lives left: {player.Lives}");
        }

        if (PlayerWon)
        {
            return new Result(
                true,
                $"You reached the other side at {newPosition.ToChessNotation()}!");
        }

        return new Result(
            true,
            $"Moved to {newPosition.ToChessNotation()}");
    }

    /// <summary>
    /// Parse console input if it is valid
    /// </summary>
    /// <param name="input"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private static bool TryParseDirection(string input, out Direction direction)
    {
        direction = Direction.Up;
        switch (input.Trim().ToUpper())
        {
            case "U":
                direction = Direction.Up;
                return true;

            case "D":
                direction = Direction.Down;
                return true;

            case "L":
                direction = Direction.Left;
                return true;

            case "R":
                direction = Direction.Right;
                return true;

            default:
                return false;
        }
    }
}
