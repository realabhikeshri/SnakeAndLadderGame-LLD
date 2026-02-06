using SnakeAndLadderGame_LLD.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.GameEngines
{
    public record GameState(
     bool IsGameOver,
     Player? Winner,
     IReadOnlyList<Player> Players,
     int CurrentPlayerIndex,
     int TotalMovesPlayed,
     IReadOnlyDictionary<Player, int> PlayerPositions
 );
}
