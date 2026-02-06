using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Players
{
    public interface IPlayer
    {
        string Name { get; }
        string Id { get; }
    }
}
