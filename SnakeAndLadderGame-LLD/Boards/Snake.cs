using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Boards
{
    public record Snake(int Mouth, int Tail) : ISnake;
}
