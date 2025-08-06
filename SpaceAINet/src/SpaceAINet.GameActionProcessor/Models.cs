namespace SpaceAINet.GameActionProcessor;

/// <summary>
/// Represents a game action that can be taken by a player or AI
/// </summary>
public enum GameAction
{
    None,
    MoveLeft,
    MoveRight,
    Shoot,
    Quit
}

/// <summary>
/// Represents the current state of the game for AI analysis
/// </summary>
public class GameState
{
    public char[,] GameBoard { get; set; } = new char[0, 0];
    public ConsoleColor[,] ColorBoard { get; set; } = new ConsoleColor[0, 0];
    public int PlayerX { get; set; }
    public int PlayerY { get; set; }
    public int Score { get; set; }
    public int BulletCount { get; set; }
    public List<EnemyPosition> Enemies { get; set; } = new();
    public List<BulletPosition> PlayerBullets { get; set; } = new();
    public List<BulletPosition> EnemyBullets { get; set; } = new();
}

/// <summary>
/// Represents an enemy position on the game board
/// </summary>
public class EnemyPosition
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsAlive { get; set; }
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// Represents a bullet position on the game board
/// </summary>
public class BulletPosition
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPlayerBullet { get; set; }
}
