# Space-AI-NET

A modular, flicker-free console Space Invaders-style game built with C# and .NET 9.0, featuring AI integration capabilities and cross-platform screenshot functionality.

## Features

- **Double-buffered rendering** - Smooth, flicker-free graphics using character-level diff updates
- **Unicode box-drawing characters** - Beautiful game boundary using proper Unicode characters (┌ ┐ └ ┘ ─ │)
- **Multi-project architecture** - Clean separation of concerns across multiple projects
- **Cross-platform screenshots** - HTML and text-based screenshot capture without graphics dependencies
- **AI integration ready** - Framework for Ollama and Azure AI Foundry integration
- **Multiple game speeds** - Choose from Slow, Medium, or Fast gameplay
- **Enhanced positioning** - Enemies at the top, player at the bottom for maximum separation
- **Unlimited bullet range** - Bullets travel the full screen distance
- **Victory conditions** - Proper win/lose states with colored victory screens

## Game Controls

- **←** Move Left
- **→** Move Right
- **SPACE** Shoot (unlimited range, up to 3 bullets at once)
- **S** Take Screenshot
- **Q** Quit Game

## Game Elements

### Player
- Rendered as 'A' in cyan color
- Positioned at the bottom of the screen for maximum separation from enemies
- Can fire up to 3 bullets simultaneously with unlimited range

### Enemies
- **Top Row (5 enemies)**: `><`, `oo`, `><`, `oo`, `><` in red - positioned at Y=0 (very top)
- **Bottom Row (3 enemies)**: `/O\` in dark yellow - positioned at Y=2 (still near top)
- Move in sweep formation, changing direction when hitting boundaries
- Move down when reaching screen edges - game over if they reach the bottom
- Only bottom row enemies can shoot

### Bullets
- **Player bullets**: `^` moving upward
- **Enemy bullets**: `v` moving downward
- Collision detection with appropriate targets

### Win/Lose Conditions
- **Victory**: Defeat all enemies - shows green "VICTORY!" message
- **Defeat**: Get hit by enemy bullet or let enemies reach the bottom - shows red "GAME OVER" message

## Project Architecture

The game follows a multi-project solution architecture:

```
SpaceAINet/
├── SpaceAINet.sln                     # Solution file
├── README.md                          # This file
└── src/                               # Source code
    ├── SpaceAINet.Console/            # Main game executable
    │   ├── Program.cs                 # Entry point
    │   ├── StartScreen.cs             # Welcome screen
    │   ├── GameManager.cs             # Game loop and state
    │   ├── Player.cs                  # Player entity
    │   ├── Enemy.cs                   # Enemy entities
    │   ├── Bullet.cs                  # Bullet physics
    │   └── appsettings.json           # AI configuration
    ├── SpaceAINet.Screenshot/         # Cross-platform screenshots
    │   ├── ScreenshotService.cs       # HTML/text screenshot capture
    │   └── RenderState.cs             # Double-buffered rendering
    ├── SpaceAINet.GameActionProcessor/ # AI integration framework
    │   ├── IGameActionProcessor.cs    # AI interface
    │   ├── OllamaGameActionProcessor.cs
    │   ├── AzureAIGameActionProcessor.cs
    │   └── Models.cs                  # Game state models
    └── SpaceAINet.ServiceDefaults/    # Shared configuration
        └── ServiceCollectionExtensions.cs
```

## Technical Requirements

- .NET 9.0 or later
- Console with UTF-8 support for Unicode characters
- Cross-platform compatible (Windows, macOS, Linux)

## Building and Running

```bash
# Build the solution
dotnet build

# Run the game
dotnet run --project src/SpaceAINet.Console
```

## Game Speed Options

1. **Slow (default)** - 100ms per frame
2. **Medium** - 70ms per frame  
3. **Fast** - 40ms per frame

Select speed at startup or press ENTER for default.

## Screenshots

Screenshots are automatically saved to the `screenshoots` folder:
- **Automatic screenshots** every 10 seconds during gameplay
- **Manual screenshots** with the 'S' key
- **Cross-platform compatibility** - saved as HTML and text files
- **No graphics dependencies** - works on all .NET platforms

## AI Integration (Future)

The game includes a framework for AI integration:
- **Ollama support** - Local AI model integration
- **Azure AI Foundry** - Cloud AI service integration
- **Game state analysis** - AI can analyze current game state
- **Action suggestions** - AI can suggest optimal moves

Configure AI settings in `src/SpaceAINet.Console/appsettings.json`.

## Platform Compatibility

The game runs on **Windows, macOS, and Linux** with full cross-platform compatibility. No platform-specific dependencies required.
