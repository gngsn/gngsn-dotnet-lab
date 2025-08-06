public static class GameManager
{
    private static RenderState? renderState;
    private static Player? player;
    private static List<Enemy> enemies = new List<Enemy>();
    private static List<Bullet> enemyBullets = new List<Bullet>();
    
    private static int gameAreaLeft = 2;
    private static int gameAreaTop = 3;
    private static int gameAreaWidth;
    private static int gameAreaHeight;
    
    private static int score = 0;
    private static DateTime gameStartTime;
    private static int gameSpeed = 1;
    private static bool gameRunning = true;
    
    private static Random random = new Random();
    private static DateTime lastEnemyShot = DateTime.Now;
    private static DateTime lastAutoScreenshot = DateTime.Now;
    
    // Enemy movement
    private static int enemyDirection = 1; // 1 for right, -1 for left
    private static DateTime lastEnemyMove = DateTime.Now;
    
    public static void RunGameLoop(int speed)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        
        gameSpeed = speed;
        gameStartTime = DateTime.Now;
        
        InitializeGame();
        ScreenshotService.Initialize();
        
        while (gameRunning)
        {
            HandleInput();
            UpdateGame();
            RenderGame();
            
            // Control game speed
            int delay = gameSpeed switch
            {
                1 => 100, // Slow
                2 => 70,  // Medium
                3 => 40,  // Fast
                _ => 100
            };
            
            Thread.Sleep(delay);
        }
        
        ShowGameOver();
    }
    
    private static void InitializeGame()
    {
        gameAreaWidth = Console.WindowWidth - 4;
        gameAreaHeight = Console.WindowHeight - 6;
        
        renderState = new RenderState(Console.WindowWidth, Console.WindowHeight);
        player = new Player(gameAreaLeft, gameAreaTop, gameAreaWidth, gameAreaHeight);
        
        InitializeEnemies();
    }
    
    private static void InitializeEnemies()
    {
        enemies.Clear();
        
        // Top row (5 enemies): ><, oo, ><, oo, ><
        int enemyY = 4;
        int startX = 5;
        int spacing = 4;
        
        enemies.Add(new Enemy(startX, enemyY, EnemyType.TopRow1));
        enemies.Add(new Enemy(startX + spacing, enemyY, EnemyType.TopRow2));
        enemies.Add(new Enemy(startX + spacing * 2, enemyY, EnemyType.TopRow3));
        enemies.Add(new Enemy(startX + spacing * 3, enemyY, EnemyType.TopRow4));
        enemies.Add(new Enemy(startX + spacing * 4, enemyY, EnemyType.TopRow5));
        
        // Bottom row (3 enemies): /O\
        enemyY = 6;
        startX = 7;
        spacing = 6;
        
        enemies.Add(new Enemy(startX, enemyY, EnemyType.BottomRow));
        enemies.Add(new Enemy(startX + spacing, enemyY, EnemyType.BottomRow));
        enemies.Add(new Enemy(startX + spacing * 2, enemyY, EnemyType.BottomRow));
    }
    
    private static void HandleInput()
    {
        // Check if console input is available (not redirected)
        try
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        player?.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        player?.MoveRight();
                        break;
                    case ConsoleKey.Spacebar:
                        player?.Shoot();
                        break;
                    case ConsoleKey.S:
                        if (renderState != null)
                            ScreenshotService.CaptureScreenshot(renderState);
                        break;
                    case ConsoleKey.Q:
                        gameRunning = false;
                        break;
                }
            }
        }
        catch (InvalidOperationException)
        {
            // Console input is redirected or not available
            // Skip input handling for this frame
        }
    }
    
    private static void UpdateGame()
    {
        if (player == null || renderState == null) return;
        
        player.Update();
        UpdateEnemies();
        UpdateEnemyBullets();
        CheckCollisions();
        
        // Auto screenshot every 10 seconds
        if ((DateTime.Now - lastAutoScreenshot).TotalSeconds >= 10)
        {
            ScreenshotService.CaptureScreenshot(renderState);
            lastAutoScreenshot = DateTime.Now;
        }
        
        // Enemy shooting logic
        if ((DateTime.Now - lastEnemyShot).TotalSeconds >= 2)
        {
            TryEnemyShoot();
            lastEnemyShot = DateTime.Now;
        }
        
        // Check win condition
        if (enemies.All(e => !e.IsAlive))
        {
            gameRunning = false;
        }
    }
    
    private static void UpdateEnemies()
    {
        // Move enemies every 500ms
        if ((DateTime.Now - lastEnemyMove).TotalMilliseconds >= 500)
        {
            bool shouldMoveDown = false;
            
            // Check if any enemy hits the boundary
            foreach (var enemy in enemies.Where(e => e.IsAlive))
            {
                int nextX = enemy.X + enemyDirection;
                if (nextX <= 0 || nextX + enemy.Width >= gameAreaWidth - 1)
                {
                    shouldMoveDown = true;
                    break;
                }
            }
            
            if (shouldMoveDown)
            {
                // Move all enemies down and reverse direction
                foreach (var enemy in enemies.Where(e => e.IsAlive))
                {
                    enemy.Y++;
                }
                enemyDirection *= -1;
            }
            else
            {
                // Move all enemies horizontally
                foreach (var enemy in enemies.Where(e => e.IsAlive))
                {
                    enemy.X += enemyDirection;
                }
            }
            
            lastEnemyMove = DateTime.Now;
        }
    }
    
    private static void UpdateEnemyBullets()
    {
        for (int i = enemyBullets.Count - 1; i >= 0; i--)
        {
            enemyBullets[i].Update();
            
            // Remove bullets that are out of bounds
            if (enemyBullets[i].Y >= gameAreaHeight - 1)
            {
                enemyBullets.RemoveAt(i);
            }
        }
    }
    
    private static void TryEnemyShoot()
    {
        var shootingEnemies = enemies.Where(e => e.IsAlive && e.CanShoot()).ToList();
        if (shootingEnemies.Any())
        {
            var shooter = shootingEnemies[random.Next(shootingEnemies.Count)];
            var bullet = shooter.CreateBullet();
            if (bullet != null)
            {
                enemyBullets.Add(bullet);
            }
        }
    }
    
    private static void CheckCollisions()
    {
        if (player == null) return;
        
        // Player bullets vs enemies
        for (int i = player.Bullets.Count - 1; i >= 0; i--)
        {
            var bullet = player.Bullets[i];
            for (int j = 0; j < enemies.Count; j++)
            {
                var enemy = enemies[j];
                if (enemy.IsAlive && bullet.CollidesWithEnemy(enemy, 0, 0))
                {
                    enemy.IsAlive = false;
                    player.Bullets.RemoveAt(i);
                    score += 10;
                    break;
                }
            }
        }
        
        // Enemy bullets vs player
        for (int i = enemyBullets.Count - 1; i >= 0; i--)
        {
            var bullet = enemyBullets[i];
            if (bullet.CollidesWith(player.X, player.Y))
            {
                gameRunning = false; // Game over
                break;
            }
        }
    }
    
    private static void RenderGame()
    {
        if (renderState == null || player == null) return;
        
        renderState.SwapBuffers();
        
        // Draw game boundary
        DrawBoundary();
        
        // Draw UI
        DrawUI();
        
        // Render game objects
        player.Render(renderState);
        
        foreach (var enemy in enemies)
        {
            enemy.Render(renderState, gameAreaLeft, gameAreaTop);
        }
        
        foreach (var bullet in enemyBullets)
        {
            bullet.Render(renderState, gameAreaLeft, gameAreaTop);
        }
        
        // Update only changed characters
        for (int y = 0; y < renderState.Height; y++)
        {
            for (int x = 0; x < renderState.Width; x++)
            {
                if (renderState.HasChanged(x, y))
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = renderState.CurrentColorBuffer[y, x];
                    Console.Write(renderState.CurrentCharBuffer[y, x]);
                }
            }
        }
    }
    
    private static void DrawBoundary()
    {
        if (renderState == null) return;
        
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        
        // Draw corners
        renderState.SetChar(0, 0, '┌', ConsoleColor.White);
        renderState.SetChar(width - 1, 0, '┐', ConsoleColor.White);
        renderState.SetChar(0, height - 1, '└', ConsoleColor.White);
        renderState.SetChar(width - 1, height - 1, '┘', ConsoleColor.White);
        
        // Draw horizontal edges
        for (int x = 1; x < width - 1; x++)
        {
            renderState.SetChar(x, 0, '─', ConsoleColor.White);
            renderState.SetChar(x, height - 1, '─', ConsoleColor.White);
        }
        
        // Draw vertical edges
        for (int y = 1; y < height - 1; y++)
        {
            renderState.SetChar(0, y, '│', ConsoleColor.White);
            renderState.SetChar(width - 1, y, '│', ConsoleColor.White);
        }
    }
    
    private static void DrawUI()
    {
        if (renderState == null || player == null) return;
        
        var elapsed = DateTime.Now - gameStartTime;
        string uiText = $" Score: {score:D4}   Time: {elapsed.TotalSeconds:F0}s   Bullets: {player.GetActiveBulletCount()}/{player.MaxBullets} ";
        
        int x = 2;
        int y = 1;
        
        for (int i = 0; i < uiText.Length && x + i < Console.WindowWidth - 2; i++)
        {
            renderState.SetChar(x + i, y, uiText[i], ConsoleColor.White);
        }
    }
    
    private static void ShowGameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("GAME OVER");
        
        Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Final Score: {score}");
        
        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 + 3);
        Console.WriteLine("Press any key to exit...");
        
        Console.ReadKey();
    }
    
    public static RenderState? GetRenderState()
    {
        return renderState;
    }
}
