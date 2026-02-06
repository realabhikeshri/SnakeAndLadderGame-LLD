
using SnakeAndLadderGame_LLD.Boards;
using SnakeAndLadderGame_LLD.Dices;
using SnakeAndLadderGame_LLD.Events;
using SnakeAndLadderGame_LLD.Players;
using SnakeAndLadderGame_LLD.GameEngines;


namespace SnakeAndLadderGame_LLD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🐍🪜 Snake & Ladder Game -  Demo");
            Console.WriteLine("=========================================\n");

            var snakes = new List<Snake>
            {
                new Snake(17, 7), new Snake(54, 34), new Snake(62, 19), new Snake(98, 79)
            };

            var ladders = new List<Ladder>
            {
                new Ladder(3, 22), new Ladder(5, 8), new Ladder(11, 28), new Ladder(45, 50)
            };

            var gameEngine = new GameEngine(100, snakes, ladders);

            
            var eventListener = new GameEventListener();
            eventListener.OnEventReceived += OnEventListenerHandler;
            gameEngine.AddEventListener(eventListener);

            
            gameEngine.AddPlayer(new Player("Alice", "P1"));
            gameEngine.AddPlayer(new Player("Bob", "P2"));
            gameEngine.AddPlayer(new Player("Charlie", "P3"));

            
            gameEngine.OnGameEvent += OnDirectGameEventHandler;

            try
            {
                gameEngine.InitializeGame(100, snakes, ladders);
                gameEngine.StartGame();

                Console.WriteLine("\n🎮 Game Started! Press Enter to roll dice for current player...\n");

                while (gameEngine.IsGameInProgress)
                {
                    Console.WriteLine("\nPress Enter for next turn...");
                    Console.ReadLine();

                    var state = gameEngine.GetCurrentState();
                    var currentPlayerIndex = state.CurrentPlayerIndex;
                    var currentPlayer = state.Players[currentPlayerIndex];

                    Console.WriteLine($"\n🎲 {currentPlayer.Name}'s turn (Player {currentPlayerIndex + 1}/{state.Players.Count})");
                    gameEngine.RollDiceForPlayer(currentPlayer);

                    PrintGameState(state);
                }

                var winner = gameEngine.GetWinner();
                Console.WriteLine($"\n🏆 FINAL WINNER: {winner?.Name}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void OnDirectGameEventHandler(GameEvent gameEvent)
        {
            Console.ForegroundColor = gameEvent.Type switch
            {
                GameEventType.GameStarted => ConsoleColor.Cyan,
                GameEventType.PlayerMoved => ConsoleColor.White,
                GameEventType.GameEnded => ConsoleColor.Yellow,
                _ => ConsoleColor.Gray
            };
            Console.WriteLine($"[DIRECT] 📢 {gameEvent.Type}: {gameEvent.Message}");
            Console.ResetColor();
        }

        private static void OnEventListenerHandler(GameEvent gameEvent)
        {
            Console.ForegroundColor = gameEvent.Type switch
            {
                GameEventType.GameStarted => ConsoleColor.Cyan,
                GameEventType.PlayerMoved => ConsoleColor.Green,
                GameEventType.GameEnded => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };
            Console.WriteLine($"[LISTENER] 🎙️ {gameEvent.Type}: {gameEvent.Message}");
            Console.ResetColor();
        }

        private static void PrintGameState(GameState state)
        {
            Console.WriteLine("\n📊 Current Standings:");
            Console.WriteLine(new string('=', 40));

            var sortedPlayers = state.Players
                .Select(p => new { Player = p, Position = state.PlayerPositions.GetValueOrDefault(p, 0) })
                .OrderByDescending(x => x.Position)
                .ToList();

            foreach (var playerInfo in sortedPlayers)
            {
                var position = playerInfo.Position;
                var status = position == 100 ? "🏆 WINNER!" : "";
                var progress = new string('█', Math.Min(position / 4, 25));
                var empty = new string('░', 25 - progress.Length);

                Console.WriteLine($"  {playerInfo.Player.Name,-8}: {position}/100 [{progress}{empty}] {status}");
            }

            Console.WriteLine($"  Next turn: {state.Players[state.CurrentPlayerIndex].Name}");
            Console.WriteLine(new string('=', 40));
        }
    }
}
