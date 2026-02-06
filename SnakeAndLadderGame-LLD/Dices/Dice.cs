using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.Dices
{
    public class Dice : IDice
    {
        private static readonly Random Random = new((int)DateTime.Now.Ticks);

        public int Roll() => Random.Next(1, 7);
    }
}
