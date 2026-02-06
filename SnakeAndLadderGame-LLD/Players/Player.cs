using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Players
{
    public record Player(string Name, string Id) : IPlayer
    {
        public Player(string name) : this(name, Guid.NewGuid().ToString("N")[..8]) { }
    }
}
