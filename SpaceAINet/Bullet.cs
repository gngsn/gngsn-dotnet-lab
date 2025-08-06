public class Bullet
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Direction { get; private set; } // -1 for up, 1 for down
    public char Character { get; private set; }
    public ConsoleColor Color { get; private set; }
    public bool IsPlayerBullet { get; private set; }
    
    public Bullet(int x, int y, int direction, char character, ConsoleColor color, bool isPlayerBullet)
    {
        X = x;
        Y = y;
        Direction = direction;
        Character = character;
        Color = color;
        IsPlayerBullet = isPlayerBullet;
    }
    
    public void Update()
    {
        Y += Direction;
    }
    
    public void Render(RenderState renderState, int offsetX, int offsetY)
    {
        renderState.SetChar(offsetX + X, offsetY + Y, Character, Color);
    }
    
    public bool CollidesWith(int targetX, int targetY)
    {
        return X == targetX && Y == targetY;
    }
    
    public bool CollidesWithEnemy(Enemy enemy, int offsetX, int offsetY)
    {
        if (enemy.IsAlive)
        {
            // Check collision with enemy's position (considering enemy width)
            int enemyScreenX = offsetX + enemy.X;
            int enemyScreenY = offsetY + enemy.Y;
            int bulletScreenX = offsetX + X;
            int bulletScreenY = offsetY + Y;
            
            // Enemy can be 2 or 3 characters wide
            int enemyWidth = enemy.Width;
            
            return bulletScreenY == enemyScreenY && 
                   bulletScreenX >= enemyScreenX && 
                   bulletScreenX < enemyScreenX + enemyWidth;
        }
        return false;
    }
}
