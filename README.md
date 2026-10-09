# Hexflip (Unity / C#)

A hex-grid board game in the style of Reversi/Othello, made solo for a Howest DAE assignment. Players place pieces on a hexagonal board, and a move is valid when it jumps a line of opponent pieces, which are then flipped. The game includes undo/redo, a menu, a pause screen and a game-over screen.

## Project structure
The code is split into three assemblies, defined with `.asmdef` files:
- **CommandPattern** and **StatePattern**: small reusable libraries with no Unity dependency.
- **Hex.Model**: the game logic, pure C# with no Unity references.
- **Hex.View**: everything that uses Unity (views, animation, UI, game states).

## Files

### CommandPattern
| File | Purpose |
|---|---|
| `ICommand.cs` | Interface with `Execute()` and `Undo()`. |
| `CommandInvoker.cs` | Executes commands and keeps an undo stack and a redo stack. |

### StatePattern
| File | Purpose |
|---|---|
| `IState.cs` | Interface with `OnEnter()`, `Update()` and `OnExit()`. |
| `StateMachine.cs` | Holds the current state and switches states with `MoveToState()`. |

### HexGame/Model
| File | Purpose |
|---|---|
| `Board.cs` | Stores tiles and pieces in dictionaries keyed by grid position. Handles tile clicks and hovers, spawning, removing and flipping pieces, and runs moves through the command invoker for undo/redo. |
| `GridPosition.cs` | Cube-coordinate struct (Q, R, S) with directions, neighbours, distance and operator overloads. Also contains `FracHexCoordinate` with cube rounding. |
| `Tile.cs` | Tile model with three highlight states that raise change events, plus lists of pieces to flip and tiles to highlight. |
| `Piece.cs` | Piece model with its color. Raises `Flipped` and `Removed` events. |
| `Player.cs` | Tracks a player's score and proportional score. |
| `PieceEventArgs.cs`, `PositionEventArgs.cs`, `FloatEventArguments.cs` | Custom `EventArgs` classes that carry a piece, a position or a float. |

### HexGame/Model/HexEngine
| File | Purpose |
|---|---|
| `HexEngine.cs` | Game rules: sets up the starting board, switches turns, finds valid tiles, counts pieces and scores, detects game over and the winner. |
| `BaseMoveSet.cs` | Finds valid moves for a piece by scanning the six hex directions and collecting the pieces that would be flipped. |
| `Commands/PlacePieceCommand.cs` | Command that places a piece. Undo removes it. |
| `Commands/FlippSinglePiece.cs` | Command that flips one piece. Undo flips it back. |

### HexGame/View
| File | Purpose |
|---|---|
| `HexGame.cs` | Links the engine to the Unity side: creates the `HexEngine`, forwards its events, exposes scores, and handles the undo/redo keys. |
| `BoardView.cs` | Creates the `Board` model, links each `TileView` to a model tile, and instantiates piece views when pieces spawn. |
| `TileView.cs` | Handles pointer click, enter and exit on a tile, and swaps materials to show highlight states. |
| `PieceView.cs` | Visual for a piece. Sets the starting orientation and destroys itself when the piece is removed. |
| `PositionHelper.cs` | Extension methods that convert between world positions and grid positions. |
| `Animations/PieceAnimator.cs` | Coroutine animation that flips a piece in an arc. It handles being interrupted mid-flip. |

### HexGame/View/States
| File | Purpose |
|---|---|
| `GameStateSystem.cs` | MonoBehaviour that creates all states and runs the state machine. Persists across scenes. |
| `MenuState.cs` | Loads the menu scene and listens for the new-game button. |
| `NewGameState.cs` | Loads the game scene asynchronously, then moves to the game state. |
| `GameState.cs` | Active gameplay. Opens pause on Escape and moves to game over when the game ends. |
| `PauseState.cs` | Shows the pause UI. Escape resumes. |
| `GameOverState.cs` | Shows the game-over UI with the winner. |

### HexGame/View/UI
| File | Purpose |
|---|---|
| `MenuUI.cs`, `PauseUI.cs`, `GameOverUI.cs` | UI Toolkit screens that expose their button clicks as C# events. |
| `GameplayUI.cs` | Updates the in-game panels with the active player, piece counts and scores. |
| `PlayerView.cs` | Plain C# class that wraps one player panel and toggles its active style. |

### HexGame/Tests
| File | Purpose |
|---|---|
| `BoardTester.cs` | Debug script that spawns and removes a piece on the board with the Space key. |

## Patterns and concepts
- **Model-View separation:** `Hex.Model` has no Unity references, and the view only listens to model events.
- **Observer pattern / event-driven design:** `EventHandler<T>`, custom `EventArgs`, and `Action` events for UI buttons.
- **Command pattern with undo/redo:** `ICommand`, `CommandInvoker`, and two stacks.
- **State pattern / finite state machine:** `IState`, `StateMachine`, and five game states.
- **Assembly definitions (`.asmdef`):** separate assemblies with enforced dependencies.
- **Hex grid math:** cube coordinates (Q + R + S = 0), cube rounding, neighbour directions, cube distance, and world-to-grid conversion.
- **Value-type design:** `GridPosition` is a struct with overridden `Equals`/`GetHashCode` so it works as a dictionary key, plus operator overloads.
- **Extension methods:** `PositionHelper`.
- **Properties with change notification:** setters on `Tile` and `Piece` raise events only when the value actually changes.
- **Rule evaluation:** move generation scans six directions and collects pieces to flip, plus game-over and winner detection.
- **Unity UI Toolkit:** `UIDocument`, `VisualElement` queries, and USS class toggling.
- **Unity EventSystems:** `IPointerClickHandler`, `IPointerEnterHandler`, `IPointerExitHandler`.
- **Coroutines and interpolation:** `Quaternion.Slerp` and a sine-based hop, with interruption handling.
- **Asynchronous scene loading:** `LoadSceneAsync` with completion callbacks, and `DontDestroyOnLoad`.
- **Defensive checks:** `Debug.Assert` for the cube-coordinate rule and `Assert.IsNotNull` in views.
