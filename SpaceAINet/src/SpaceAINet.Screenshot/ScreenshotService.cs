namespace SpaceAINet.Screenshot;

public static class ScreenshotService
{
    private static string screenshotFolder = "screenshoots";
    private static int screenshotCounter = 0;

    public static void Initialize()
    {
        // Create and clear screenshot folder
        if (Directory.Exists(screenshotFolder))
        {
            Directory.Delete(screenshotFolder, true);
        }
        Directory.CreateDirectory(screenshotFolder);
        screenshotCounter = 0;
    }

    public static void CaptureScreenshot(RenderState renderState)
    {
        try
        {
            screenshotCounter++;

            // Create text-based screenshot
            var lines = new string[renderState.Height];

            for (int y = 0; y < renderState.Height; y++)
            {
                var chars = new char[renderState.Width];
                for (int x = 0; x < renderState.Width; x++)
                {
                    char ch = renderState.CurrentCharBuffer[y, x];
                    chars[x] = ch == '\0' ? ' ' : ch;
                }
                lines[y] = new string(chars);
            }

            // Save as text file
            string textFilename = Path.Combine(screenshotFolder, $"screenshot_{screenshotCounter:D4}.txt");
            File.WriteAllLines(textFilename, lines);

            // Also create HTML version with colors
            CreateHtmlScreenshot(renderState, screenshotCounter);

            Console.WriteLine($"Screenshot saved: {textFilename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Screenshot error: {ex.Message}");
        }
    }

    private static void CreateHtmlScreenshot(RenderState renderState, int counter)
    {
        string htmlFilename = Path.Combine(screenshotFolder, $"screenshot_{counter:D4}.html");

        using var writer = new StreamWriter(htmlFilename);

        writer.WriteLine("<!DOCTYPE html>");
        writer.WriteLine("<html><head>");
        writer.WriteLine("<title>Space.AI.NET() Screenshot</title>");
        writer.WriteLine("<style>");
        writer.WriteLine("body { background: black; font-family: 'Courier New', monospace; font-size: 12px; margin: 0; padding: 10px; }");
        writer.WriteLine("pre { margin: 0; line-height: 1.2; }");
        writer.WriteLine("</style>");
        writer.WriteLine("</head><body>");
        writer.WriteLine("<pre>");

        for (int y = 0; y < renderState.Height; y++)
        {
            for (int x = 0; x < renderState.Width; x++)
            {
                char ch = renderState.CurrentCharBuffer[y, x];
                ConsoleColor color = renderState.CurrentColorBuffer[y, x];

                if (ch == '\0') ch = ' ';

                string htmlColor = GetHtmlColor(color);
                writer.Write($"<span style=\"color: {htmlColor}\">");

                // Escape HTML special characters
                string escaped = ch switch
                {
                    '<' => "&lt;",
                    '>' => "&gt;",
                    '&' => "&amp;",
                    ' ' => "&nbsp;",
                    _ => ch.ToString()
                };

                writer.Write(escaped);
                writer.Write("</span>");
            }
            writer.WriteLine();
        }

        writer.WriteLine("</pre>");
        writer.WriteLine("</body></html>");
    }

    private static string GetHtmlColor(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "#000000",
            ConsoleColor.DarkBlue => "#000080",
            ConsoleColor.DarkGreen => "#008000",
            ConsoleColor.DarkCyan => "#008080",
            ConsoleColor.DarkRed => "#800000",
            ConsoleColor.DarkMagenta => "#800080",
            ConsoleColor.DarkYellow => "#808000",
            ConsoleColor.Gray => "#C0C0C0",
            ConsoleColor.DarkGray => "#808080",
            ConsoleColor.Blue => "#0000FF",
            ConsoleColor.Green => "#00FF00",
            ConsoleColor.Cyan => "#00FFFF",
            ConsoleColor.Red => "#FF0000",
            ConsoleColor.Magenta => "#FF00FF",
            ConsoleColor.Yellow => "#FFFF00",
            ConsoleColor.White => "#FFFFFF",
            _ => "#FFFFFF"
        };
    }
}
