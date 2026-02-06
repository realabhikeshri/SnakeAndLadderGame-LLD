using SnakeAndLadderGame_LLD.Boards;
using SnakeAndLadderGame_LLD.Dices;
using SnakeAndLadderGame_LLD.Events;
using SnakeAndLadderGame_LLD.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeAndLadderGame_LLD.GameEngines
{
    public class GameEngine : IGameEngine
    {
        private readonly Board _board;
        private readonly Dice _dice;
        private readonly List<Player> _players = new();
        private readonly List<GameEventListener> _listeners = new();
        private int _currentPlayerIndex = 0;
        private GameState _currentState;
        private bool _gameStarted = false;
        private readonly Dictionary<Player, int> _mutablePositions = new();  // ✅ ADDED: Mutable backing field

        public event Action<GameEvent>? OnGameEvent;

        public GameEngine(int boardSize, List<Snake> snakes, List<Ladder> ladders)
        {
            _board = new Board();
            _dice = new Dice();
            _currentState = new GameState(false, null!, Array.Empty<Player>(), 0, 0, new Dictionary<Player, int>());
            InitializeBoard(boardSize, snakes, ladders);
        }

        private void InitializeBoard(int boardSize, List<Snake> snakes, List<Ladder> ladders)
        {
            _board.Initialize(boardSize, snakes, ladders);
        }

        public void InitializeGame(int boardSize, List<Snake> snakes, List<Ladder> ladders)
        {
            InitializeBoard(boardSize, snakes, ladders);
            _mutablePositions.Clear();
            foreach (var player in _players)
            {
                _mutablePositions[player] = 0;
            }
        }

        public void AddPlayer(Player player)
        {
            ArgumentNullException.ThrowIfNull(player);
            if (_gameStarted) throw new InvalidOperationException("Cannot add player after game started");
            if (_players.Contains(player)) throw new ArgumentException("Player already exists");

            _players.Add(player);
            _mutablePositions[player] = 0;  // ✅ Use mutable dictionary
            UpdateGameState();
        }

        public void AddEventListener(IGameEventListener listener)
        {
            _listeners.Add(listener as GameEventListener ?? throw new ArgumentException("Invalid listener"));
        }

        public void StartGame()
        {
            if (_players.Count < 2) throw new InvalidOperationException("At least 2 players required");
            if (_gameStarted) throw new InvalidOperationException("Game already started");

            _gameStarted = true;
            _currentPlayerIndex = 0;
            NotifyAll(new GameEvent(GameEventType.GameStarted, $"Game started with {_players.Count} players"));
        }

        public void RollDiceForPlayer(Player player)
        {
            ValidateGameState(player);

            var diceValue = _dice.Roll();
            var oldPosition = _mutablePositions.GetValueOrDefault(player, 0);
            var newPosition = _board.Move(player, oldPosition, diceValue);

            _mutablePositions[player] = newPosition;  // ✅ Update mutable backing field
            UpdateGameState();  // ✅ Refresh readonly state

            NotifyPlayerMoved(player, oldPosition, newPosition, diceValue);

            if (newPosition == _board.Size)
            {
                EndGame(player);
                return;
            }

            AdvanceTurn();
        }

        private void ValidateGameState(Player player)
        {
            if (!_gameStarted) throw new InvalidOperationException("Game not started");
            if (!_players.Contains(player)) throw new ArgumentException("Player not in game");
        }

        private void AdvanceTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            UpdateGameState();
        }

        private void EndGame(Player winner)
        {
            _currentState = new GameState(true, winner, _players.AsReadOnly(), _currentPlayerIndex, _mutablePositions.Count,
                new Dictionary<Player, int>(_mutablePositions));  // ✅ Create new readonly copy
            NotifyAll(new GameEvent(GameEventType.GameEnded, $"{winner.Name} wins the game! 🎉"));
        }

        private void NotifyPlayerMoved(Player player, int oldPos, int newPos, int diceValue)
        {
            var message = $"{player.Name} rolled {diceValue}, moved from {oldPos} to {newPos}";
            NotifyAll(new GameEvent(GameEventType.PlayerMoved, message));
        }

        private void NotifyAll(GameEvent gameEvent)
        {
            OnGameEvent?.Invoke(gameEvent);
            foreach (var listener in _listeners)
            {
                listener.OnGameEvent(gameEvent);
            }
        }

        private void UpdateGameState()
        {
            _currentState = new GameState(
                IsGameOver: false,
                Winner: null,
                Players: _players.AsReadOnly(),
                CurrentPlayerIndex: _currentPlayerIndex,
                TotalMovesPlayed: _mutablePositions.Count,
                PlayerPositions: new Dictionary<Player, int>(_mutablePositions)  // ✅ NEW readonly copy each time
            );
        }

        public GameState GetCurrentState() => _currentState with
        {
            PlayerPositions = new Dictionary<Player, int>(_mutablePositions)  // ✅ Fresh readonly copy
        };

        public bool IsGameInProgress => _gameStarted && !_currentState.IsGameOver;
        public Player? GetWinner() => _currentState.Winner;
    }
}
