// Space.AI.NET() - Console Space Invaders Game
// Set UTF-8 encoding for box-drawing characters
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;

// Show start screen
StartScreen.Show();

// Read user input for speed selection
Console.WriteLine();
Console.Write("Select speed and press ENTER: ");
var input = Console.ReadLine();

int gameSpeed = 1; // Default to slow
if (int.TryParse(input, out int speed) && speed >= 1 && speed <= 3)
{
    gameSpeed = speed;
}

// Clear console and start game
Console.Clear();
GameManager.RunGameLoop(gameSpeed);
