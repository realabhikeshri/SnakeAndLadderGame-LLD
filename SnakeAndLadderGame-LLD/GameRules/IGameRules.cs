using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.GameRules
{
    public interface IGameRules
    {
        bool CanRollAgain(int diceValue);
        bool IsValidMove(int from, int to, int boardSize);
    }
}
