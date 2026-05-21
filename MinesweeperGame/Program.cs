using MinesweeperGame.Application;

var game = GameFactory.CreateDefault(8, 8, 3);

Console.WriteLine("=====================================");
Console.WriteLine("        MINEFIELD GAME");
Console.WriteLine("=====================================");
Console.WriteLine("Reach the RIGHT side of the board.");
Console.WriteLine("Avoid hidden mines.");
Console.WriteLine("Controls: U, D, L, R");
Console.WriteLine();

while (!game.IsFinished)
{
    Console.WriteLine(game.GetStatus());

    Console.Write("Move (U/D/L/R): ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Invalid input.");
        continue;
    }

    var result = game.Move(input);

    Console.WriteLine(result.Message);
    Console.WriteLine();
}

Console.WriteLine("=====================================");
Console.WriteLine(game.PlayerWon
    ? $"YOU WON in {game.MovesTaken} moves!"
    : "GAME OVER");
Console.WriteLine("=====================================");