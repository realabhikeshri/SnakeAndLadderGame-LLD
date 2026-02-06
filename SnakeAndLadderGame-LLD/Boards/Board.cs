using SnakeAndLadderGame_LLD.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Boards
{
    public class Board
    {
        public int Size { get; private set; }
        private readonly Dictionary<int, Snake> _snakesByMouth = new();
        private readonly Dictionary<int, Ladder> _laddersByBottom = new();

        public void Initialize(int size, List<Snake> snakes, List<Ladder> ladders)
        {
            if (size < 10)
                throw new ArgumentOutOfRangeException(nameof(size), size, "Board size must be >= 10");

            Size = size;
            _snakesByMouth.Clear();
            _laddersByBottom.Clear();

            foreach (var snake in snakes)
            {
                ValidateSnake(snake);
                _snakesByMouth[snake.Mouth] = snake;
            }

            foreach (var ladder in ladders)
            {
                ValidateLadder(ladder);
                _laddersByBottom[ladder.Bottom] = ladder;
            }
        }

        private static void ValidateSnake(Snake snake)
        {
            if (snake.Mouth <= snake.Tail || snake.Mouth > 100 || snake.Tail <= 0)
                throw new ArgumentException($"Invalid snake: {snake.Mouth} -> {snake.Tail}");
        }

        private static void ValidateLadder(Ladder ladder)
        {
            if (ladder.Bottom >= ladder.Top || ladder.Bottom <= 0 || ladder.Top > 100)
                throw new ArgumentException($"Invalid ladder: {ladder.Bottom} -> {ladder.Top}");
        }

        public int Move(Player player, int currentPosition, int diceValue)
        {
            var tempPosition = Math.Min(currentPosition + diceValue, Size);

            // Ladder takes precedence
            if (_laddersByBottom.TryGetValue(tempPosition, out var ladder))
            {
                return ladder.Top;
            }

            // Snake bite
            if (_snakesByMouth.TryGetValue(tempPosition, out var snake))
            {
                return snake.Tail;
            }

            return tempPosition;
        }
    }
}
