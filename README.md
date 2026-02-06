# Snake & Ladder Game

A complete C# implementation of the classic Snake & Ladder board game with production-ready architecture.

## 🗂️ Project Structure
SnakeAndLadderGame-LLD/
├── Program.cs
├── Boards/
│ ├── Board.cs
│ ├── Snake.cs
│ └── Ladder.cs
├── Dices/
│ └── Dice.cs
├── Players/
│ └── Player.cs
├── Events/
│ ├── GameEvents.cs
│ ├── IGameEventListener.cs
│ └── GameEventListener.cs
└── GameEngine/
├── GameEngine.cs
├── IGameEngine.cs
└── GameState.cs


## ✨ Features

- **Event-Driven Architecture** - Real-time game events with observer pattern
- **SOLID Principles** - Single responsibility, dependency inversion
- **Immutable Game State** - Thread-safe state management
- **Interactive Console UI** - Progress bars and live standings
- **Dual Event System** - Direct events + listener pattern
- **Production Error Handling** - Comprehensive exception management
- **Extensible Design** - Easy to add custom rules, dice, boards

## 🚀 Quick Start

```bash
# Clone & Build
git clone <repo>
cd SnakeAndLadderGame-LLD
dotnet restore
dotnet build

# Run the game
dotnet run

# Run tests
dotnet test


---
---
🎮 How to Play
Game auto-initializes with 3 players (Alice, Bob, Charlie)

Press Enter after each turn to roll dice for current player

Visual progress bars show current standings

Snakes send you back, Ladders boost you forward

First to 100 wins!

Design Patterns Used:

Observer (Events + Listeners)

Strategy (Pluggable Dice/Rules)

Immutable State Pattern

Dependency Inversion

## Author
Abhishek Keshri
