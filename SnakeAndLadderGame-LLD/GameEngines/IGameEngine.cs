using SnakeAndLadderGame_LLD.Boards;
using SnakeAndLadderGame_LLD.Events;
using SnakeAndLadderGame_LLD.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame_LLD.GameEngines
{
    public interface IGameEngine
    {
        void InitializeGame(int boardSize, List<Snake> snakes, List<Ladder> ladders);
        void AddPlayer(Player player);
        void StartGame();
        void RollDiceForPlayer(Player player);
        GameState GetCurrentState();
        event Action<GameEvent> OnGameEvent;
        bool IsGameInProgress { get; }
        Player? GetWinner();
    }
}
