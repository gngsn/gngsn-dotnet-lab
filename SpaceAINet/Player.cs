public class Player
{
    public int X { get; set; }
    public int Y { get; set; }
    public int MaxBullets { get; private set; } = 3;
    public List<Bullet> Bullets { get; private set; }
    
    private int gameAreaWidth;
    private int gameAreaHeight;
    private int gameAreaLeft;
    private int gameAreaTop;
    
    public Player(int gameAreaLeft, int gameAreaTop, int gameAreaWidth, int gameAreaHeight)
    {
        this.gameAreaLeft = gameAreaLeft;
        this.gameAreaTop = gameAreaTop;
        this.gameAreaWidth = gameAreaWidth;
        this.gameAreaHeight = gameAreaHeight;
        
        // Start player at bottom center
        X = gameAreaWidth / 2;
        Y = gameAreaHeight - 2; // Near bottom of game area
        
        Bullets = new List<Bullet>();
    }
    
    public void MoveLeft()
    {
        if (X > 1) // Keep within game area boundaries
            X--;
    }
    
    public void MoveRight()
    {
        if (X < gameAreaWidth - 2) // Keep within game area boundaries
            X++;
    }
    
    public void Shoot()
    {
        if (Bullets.Count < MaxBullets)
        {
            Bullets.Add(new Bullet(X, Y - 1, -1, '^', ConsoleColor.White, true));
        }
    }
    
    public void Update()
    {
        // Update bullets
        for (int i = Bullets.Count - 1; i >= 0; i--)
        {
            Bullets[i].Update();
            
            // Remove bullets that are out of bounds
            if (Bullets[i].Y < gameAreaTop + 1)
            {
                Bullets.RemoveAt(i);
            }
        }
    }
    
    public void Render(RenderState renderState)
    {
        // Render player
        renderState.SetChar(gameAreaLeft + X, gameAreaTop + Y, 'A', ConsoleColor.Cyan);
        
        // Render bullets
        foreach (var bullet in Bullets)
        {
            bullet.Render(renderState, gameAreaLeft, gameAreaTop);
        }
    }
    
    public int GetActiveBulletCount()
    {
        return Bullets.Count;
    }
}
