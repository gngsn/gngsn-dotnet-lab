using System.Drawing;
using System.Drawing.Imaging;

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
            // Check if we're on a supported platform for System.Drawing
            if (!OperatingSystem.IsWindows() && !OperatingSystem.IsLinux() && !OperatingSystem.IsMacOS())
            {
                Console.WriteLine("Screenshot functionality not supported on this platform.");
                return;
            }

            // Font settings for rendering
            string fontName = "Consolas";
            int fontSize = 12;
            int charWidth = 7;
            int charHeight = 14;

            // Create bitmap
            int bitmapWidth = renderState.Width * charWidth;
            int bitmapHeight = renderState.Height * charHeight;

            using (var bitmap = new Bitmap(bitmapWidth, bitmapHeight))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Set background to black
                graphics.Clear(System.Drawing.Color.Black);

                // Try to use Consolas font, fallback to monospace
                Font font;
                try
                {
                    font = new Font(fontName, fontSize, FontStyle.Regular);
                }
                catch
                {
                    font = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Regular);
                }

                // Render each character
                for (int y = 0; y < renderState.Height; y++)
                {
                    for (int x = 0; x < renderState.Width; x++)
                    {
                        char ch = renderState.CurrentCharBuffer[y, x];
                        ConsoleColor consoleColor = renderState.CurrentColorBuffer[y, x];

                        if (ch != ' ')
                        {
                            System.Drawing.Color drawColor = ConvertConsoleColor(consoleColor);
                            using (var brush = new SolidBrush(drawColor))
                            {
                                graphics.DrawString(ch.ToString(), font, brush,
                                    x * charWidth, y * charHeight);
                            }
                        }
                    }
                }

                font.Dispose();

                // Save screenshot
                screenshotCounter++;
                string filename = Path.Combine(screenshotFolder,
                    $"screenshot_{screenshotCounter:D4}.png");
                bitmap.Save(filename, ImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            // Silently handle screenshot errors to not interrupt gameplay
            Console.WriteLine($"Screenshot error: {ex.Message}");
        }
    }

    private static System.Drawing.Color ConvertConsoleColor(ConsoleColor consoleColor)
    {
        return consoleColor switch
        {
            ConsoleColor.Black => System.Drawing.Color.Black,
            ConsoleColor.DarkBlue => System.Drawing.Color.DarkBlue,
            ConsoleColor.DarkGreen => System.Drawing.Color.DarkGreen,
            ConsoleColor.DarkCyan => System.Drawing.Color.DarkCyan,
            ConsoleColor.DarkRed => System.Drawing.Color.DarkRed,
            ConsoleColor.DarkMagenta => System.Drawing.Color.DarkMagenta,
            ConsoleColor.DarkYellow => System.Drawing.Color.Orange,
            ConsoleColor.Gray => System.Drawing.Color.Gray,
            ConsoleColor.DarkGray => System.Drawing.Color.DarkGray,
            ConsoleColor.Blue => System.Drawing.Color.Blue,
            ConsoleColor.Green => System.Drawing.Color.Green,
            ConsoleColor.Cyan => System.Drawing.Color.Cyan,
            ConsoleColor.Red => System.Drawing.Color.Red,
            ConsoleColor.Magenta => System.Drawing.Color.Magenta,
            ConsoleColor.Yellow => System.Drawing.Color.Yellow,
            ConsoleColor.White => System.Drawing.Color.White,
            _ => System.Drawing.Color.White
        };
    }
}
