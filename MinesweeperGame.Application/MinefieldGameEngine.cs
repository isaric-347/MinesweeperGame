using MinesweeperGame.Domain;

namespace MinesweeperGame.Application;

public class MinefieldGameEngine(Board board, Player player)
{
    private readonly Board _board = board;
    private readonly Player _player = player;

    public bool IsFinished => PlayerWon || _player.Lives <= 0;

    public bool PlayerWon =>
        _player.Position.Column == _board.Width - 1;

    public int MovesTaken => _player.MovesTaken;

    public string GetStatus()
    {
        return $"Position: {_player.Position.ToChessNotation()} | " +
               $"Lives: {_player.Lives} | " +
               $"Moves: {_player.MovesTaken}";
    }

    public Result Move(string input)
    {
        if (!TryParseDirection(input, out var direction))
        {
            return new Result(false, "Invalid direction.");
        }

        var newPosition = _player.Position.Move(direction);

        if (!_board.IsInsideBoard(newPosition))
        {
            return new Result(false, "Cannot move outside the board.");
        }

        _player.MoveTo(newPosition);

        if (_board.HasMine(newPosition))
        {
            _player.LoseLife();

            if (_player.Lives <= 0)
            {
                return new Result(
                    true,
                    $"BOOM! Mine hit at {newPosition.ToChessNotation()}. No lives remaining.");
            }

            return new Result(
                true,
                $"BOOM! Mine hit at {newPosition.ToChessNotation()}. Lives left: {_player.Lives}");
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
