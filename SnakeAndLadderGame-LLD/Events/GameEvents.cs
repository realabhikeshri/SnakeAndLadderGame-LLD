using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Events
{
    public enum GameEventType
    {
        GameStarted,
        PlayerMoved,
        PlayerWon,
        GameEnded
    }

    public record GameEvent(GameEventType Type, string Message);
}
