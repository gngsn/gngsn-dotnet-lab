namespace SpaceAINet.Screenshot;

public class RenderState
{
    public char[,] CurrentCharBuffer { get; set; }
    public ConsoleColor[,] CurrentColorBuffer { get; set; }
    public char[,] PreviousCharBuffer { get; set; }
    public ConsoleColor[,] PreviousColorBuffer { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public RenderState(int width, int height)
    {
        Width = width;
        Height = height;

        CurrentCharBuffer = new char[height, width];
        CurrentColorBuffer = new ConsoleColor[height, width];
        PreviousCharBuffer = new char[height, width];
        PreviousColorBuffer = new ConsoleColor[height, width];

        // Initialize buffers
        Clear();
    }

    public void Clear()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                CurrentCharBuffer[y, x] = ' ';
                CurrentColorBuffer[y, x] = ConsoleColor.Black;
                PreviousCharBuffer[y, x] = ' ';
                PreviousColorBuffer[y, x] = ConsoleColor.Black;
            }
        }
    }

    public void SwapBuffers()
    {
        // Copy current to previous
        Array.Copy(CurrentCharBuffer, PreviousCharBuffer, CurrentCharBuffer.Length);
        Array.Copy(CurrentColorBuffer, PreviousColorBuffer, CurrentColorBuffer.Length);

        // Clear current buffer
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                CurrentCharBuffer[y, x] = ' ';
                CurrentColorBuffer[y, x] = ConsoleColor.Black;
            }
        }
    }

    public void SetChar(int x, int y, char character, ConsoleColor color)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            CurrentCharBuffer[y, x] = character;
            CurrentColorBuffer[y, x] = color;
        }
    }

    public bool HasChanged(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return CurrentCharBuffer[y, x] != PreviousCharBuffer[y, x] ||
                   CurrentColorBuffer[y, x] != PreviousColorBuffer[y, x];
        }
        return false;
    }
}
