using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Events
{
    public class GameEventListener : IGameEventListener
    {
        public Action<GameEvent>? OnEventReceived { get; set; }

        public void OnGameEvent(GameEvent gameEvent)
        {
            OnEventReceived?.Invoke(gameEvent);
        }
    }
}
