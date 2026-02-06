using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.GameRules
{
    public class GameRules : IGameRules
    {
        public bool CanRollAgain(int diceValue) => diceValue == 6;

        public bool IsValidMove(int from, int to, int boardSize)
        {
            return from >= 0 && to <= boardSize && to >= from;
        }
    }
}
