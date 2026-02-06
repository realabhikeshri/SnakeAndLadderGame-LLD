using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Events
{
    public interface IGameEventListener
    {
        void OnGameEvent(GameEvent gameEvent);
    }
}
