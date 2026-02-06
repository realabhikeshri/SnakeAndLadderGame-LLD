using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Boards
{
    public interface ISnake
    {
        int Mouth { get; }
        int Tail { get; }
    }
}
