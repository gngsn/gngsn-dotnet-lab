public static class StartScreen
{
    public static void Show()
    {
        Console.Clear();
        
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        
        // Calculate center positions
        string title = "Space.AI.NET()";
        string subtitle = "Built with .NET + AI for galactic defense";
        
        int titleX = (width - title.Length) / 2;
        int titleY = height / 4;
        
        int subtitleX = (width - subtitle.Length) / 2;
        int subtitleY = titleY + 2;
        
        // Display title centered
        Console.SetCursorPosition(titleX, titleY);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(title);
        
        Console.SetCursorPosition(subtitleX, subtitleY);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(subtitle);
        
        // Instructions and speed options - left-aligned
        int instructionsY = subtitleY + 4;
        int leftMargin = 4;
        
        Console.SetCursorPosition(leftMargin, instructionsY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("How to Play:");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 1);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("←   Move Left");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 2);
        Console.WriteLine("→   Move Right");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 3);
        Console.WriteLine("SPACE   Shoot");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 4);
        Console.WriteLine("S   Take Screenshot");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 5);
        Console.WriteLine("Q   Quit");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 7);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Select Game Speed:");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 8);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("[1] Slow (default)");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 9);
        Console.WriteLine("[2] Medium");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 10);
        Console.WriteLine("[3] Fast");
        
        Console.SetCursorPosition(leftMargin, instructionsY + 11);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press ENTER for default");
        
        Console.ResetColor();
    }
}
