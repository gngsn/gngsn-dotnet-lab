# Space.AI.NET()

A modular, flicker-free console Space Invaders-style game built with C# and .NET 9.0.

## Features

- **Double-buffered rendering** - Smooth, flicker-free graphics using character-level diff updates
- **Unicode box-drawing characters** - Beautiful game boundary using proper Unicode characters (┌ ┐ └ ┘ ─ │)
- **Modular architecture** - Clean separation of concerns across multiple classes
- **Screenshot capture** - Automatic and manual screenshot functionality with proper font rendering
- **Multiple game speeds** - Choose from Slow, Medium, or Fast gameplay
- **Collision detection** - Player and enemy bullet interactions
- **Enemy AI** - Enemies move in formation and shoot strategically

## Game Controls

- **←** Move Left
- **→** Move Right
- **SPACE** Shoot (up to 3 bullets at once)
- **S** Take Screenshot
- **Q** Quit Game

## Game Elements

### Player
- Rendered as 'A' in cyan color
- Moves left and right in the lower area of the screen
- Can fire up to 3 bullets simultaneously

### Enemies
- **Top Row (5 enemies)**: `><`, `oo`, `><`, `oo`, `><` in red
- **Bottom Row (3 enemies)**: `/O\` in dark yellow
- Move in sweep formation, changing direction when hitting boundaries
- Only bottom row enemies can shoot

### Bullets
- **Player bullets**: `^` moving upward
- **Enemy bullets**: `v` moving downward
- Collision detection with appropriate targets

## Architecture

The game follows a modular design with the following components:

- **Program.cs** - Entry point and game initialization
- **StartScreen.cs** - Welcome screen and instructions
- **GameManager.cs** - Main game loop and state management
- **Player.cs** - Player entity and controls
- **Enemy.cs** - Enemy entities and AI
- **Bullet.cs** - Bullet physics and collision
- **RenderState.cs** - Double-buffered rendering system
- **ScreenshotService.cs** - Screenshot capture functionality

## Technical Requirements

- .NET 9.0 or later
- Console with UTF-8 support for Unicode characters
- System.Drawing.Common package for screenshot functionality

## Building and Running

```bash
dotnet build
dotnet run
```

## Game Speed Options

1. **Slow (default)** - 100ms per frame
2. **Medium** - 70ms per frame  
3. **Fast** - 40ms per frame

Select speed at startup or press ENTER for default.

## Screenshots

Screenshots are automatically saved to the `screenshoots` folder:
- Automatic screenshots every 10 seconds during gameplay
- Manual screenshots with the 'S' key
- Saved as PNG files with proper font rendering

## Platform Compatibility

The game runs on Windows, macOS, and Linux. Screenshot functionality requires platform support for System.Drawing.Common.
