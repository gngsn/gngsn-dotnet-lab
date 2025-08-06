using SpaceAINet.Screenshot;

public class Enemy
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Sprite { get; private set; } = string.Empty;
    public ConsoleColor Color { get; private set; }
    public bool IsAlive { get; set; }
    public int Width { get; private set; }
    public EnemyType Type { get; private set; }

    public Enemy(int x, int y, EnemyType type)
    {
        X = x;
        Y = y;
        Type = type;
        IsAlive = true;

        switch (type)
        {
            case EnemyType.TopRow1:
            case EnemyType.TopRow3:
            case EnemyType.TopRow5:
                Sprite = "><";
                Color = ConsoleColor.Red;
                Width = 2;
                break;
            case EnemyType.TopRow2:
            case EnemyType.TopRow4:
                Sprite = "oo";
                Color = ConsoleColor.Red;
                Width = 2;
                break;
            case EnemyType.BottomRow:
                Sprite = "/O\\";
                Color = ConsoleColor.DarkYellow;
                Width = 3;
                break;
        }
    }

    public void Render(RenderState renderState, int offsetX, int offsetY)
    {
        if (IsAlive)
        {
            for (int i = 0; i < Sprite.Length; i++)
            {
                renderState.SetChar(offsetX + X + i, offsetY + Y, Sprite[i], Color);
            }
        }
    }

    public bool CanShoot()
    {
        return IsAlive && Type == EnemyType.BottomRow; // Only bottom row enemies can shoot
    }

    public Bullet? CreateBullet()
    {
        if (CanShoot())
        {
            int bulletX = X + Width / 2; // Center of enemy
            return new Bullet(bulletX, Y + 1, 1, 'v', ConsoleColor.White, false);
        }
        return null;
    }
}

public enum EnemyType
{
    TopRow1,
    TopRow2,
    TopRow3,
    TopRow4,
    TopRow5,
    BottomRow
}
