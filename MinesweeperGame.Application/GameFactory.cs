using MinesweeperGame.Domain;

namespace MinesweeperGame.Application;

public static class GameFactory
{
    public static MinefieldGameEngine CreateDefault(int boardWidth, int boardHeight, int playerLives)
    {
        var board = new Board(
            width: boardWidth,
            height: boardHeight,
            mines:
            [
            new(1,1),
            new(2,3),
            new(4,4),
            new(5,2),
            new(6,6),
            new(3,5)
            ]);

        var player = new Player(
            startPosition: new Position(0, 0),
            lives: playerLives);

        return new MinefieldGameEngine(board, player);
    }
}
