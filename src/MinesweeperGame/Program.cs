using MinesweeperGame.Application;
using MinesweeperGame.Domain;
using MinesweeperGame.Domain.Params;

// Create and start Minefield game with predefined params
var game = GamesFactory.CreateGame(GameType.Minefield, parameters: new MinefieldParams(8, 8, 3, 10, new(0, 0)));
game.Start();