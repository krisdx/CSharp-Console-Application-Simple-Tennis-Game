using System;
using System.Threading;
using System.Diagnostics;

public class TennisGame
{
    private const int PlayerWidth = 6;
    private const char PlayerBody = '_';
    private const char Ball = 'O';

    static string direction = string.Empty;
    static int ballRow = 0;
    static int ballCol = 0;

    private static string[] directions = new string[] { "upRight", "upLeft", "downLeft", "downRight" };

    public static void Main()
    {
        Console.CursorVisible = false;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;

        int playerPosition = Console.WindowWidth / 2;
        Console.SetCursorPosition(playerPosition, Console.WindowHeight - 1);
        PrintPlayer(playerPosition);

        Random randomGenerator = new Random();
        direction = directions[randomGenerator.Next(0, directions.Length)];

        ballRow = randomGenerator.Next(0, 2);
        ballCol = randomGenerator.Next(0, 2);
        Console.SetCursorPosition(ballCol, ballRow);
        Console.Write(Ball);

        Stopwatch timePlayed = new Stopwatch();
        timePlayed.Start();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                {
                    playerPosition += 3;
                    if (playerPosition > Console.WindowWidth - PlayerWidth - 1)
                    {
                        playerPosition = Console.WindowWidth - PlayerWidth - 1;
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    playerPosition -= 3;
                    if (playerPosition < 0)
                    {
                        playerPosition = 0;
                    }
                }
            }

            Thread.Sleep(45);
            Console.Clear();

            PrintPlayer(playerPosition);
            PrintBall();

            if (ballRow == Console.WindowHeight - 1 &&
                (ballCol < playerPosition || ballCol > (playerPosition + PlayerWidth)))
            {
                timePlayed.Stop();

                Console.Clear();

                Console.SetCursorPosition((Console.WindowWidth / 2) / 2, Console.WindowHeight / 2);
                Console.WriteLine("*** Game Over! ***");
                Console.SetCursorPosition((Console.WindowWidth / 2) / 2, Console.WindowHeight / 2 + 1);
                Console.Write("Time Played: {0}", timePlayed.Elapsed);
                Console.Beep(2500, 500);
                Console.WriteLine();
                return;
            }
        }
    }

    public static void PrintBall()
    {
        if (direction == "upRight")
        {
            ballRow--;
            if (ballRow <= 0)
            {
                ballRow = 0;
                direction = "downRight";
            }

            ballCol++;
            if (ballCol >= Console.WindowWidth - 1)
            {
                ballCol = Console.WindowWidth - 1;
                direction = "upLeft";
            }
        }
        else if (direction == "upLeft")
        {
            ballRow--;
            if (ballRow <= 0)
            {
                ballRow = 0;
                direction = "downLeft";
            }

            ballCol--;
            if (ballCol <= 0)
            {
                ballCol = 0;
                direction = "upRight";
            }
        }
        else if (direction == "downLeft")
        {
            ballRow++;
            if (ballRow >= Console.WindowHeight - 1)
            {
                ballRow = Console.WindowHeight - 1;
                direction = "upLeft";
            }

            ballCol--;
            if (ballCol <= 0)
            {
                ballCol = 0;
                direction = "downRight";
            }
        }
        else if (direction == "downRight")
        {
            ballRow++;
            if (ballRow >= Console.WindowHeight - 1)
            {
                ballRow = Console.WindowHeight - 1;
                direction = "upRight";
            }

            ballCol++;
            if (ballCol >= Console.WindowWidth - 1)
            {
                ballCol = Console.WindowWidth - 1;
                direction = "downLeft";
            }
        }

        Console.SetCursorPosition(ballCol, ballRow);
        Console.Write(Ball);
    }

    public static void PrintPlayer(int playerPosition)
    {
        for (int i = 0; i < PlayerWidth; i++)
        {
            Console.SetCursorPosition(playerPosition + i, Console.WindowHeight - 1);
            Console.Write(PlayerBody);
        }
    }
}