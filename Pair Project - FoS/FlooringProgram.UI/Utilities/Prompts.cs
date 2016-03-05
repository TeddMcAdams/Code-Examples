using System;

namespace FlooringProgram.UI.Utilities
{
    internal static class Prompts
    {
        internal static bool CheckForNotEmpty(string input)
        {
            return input.Length > 0;
        }

        internal static void InvalidInput()
        {
            Console.Write("\n  Invalid input. Press any key to try again. ");
            Console.ReadKey();
        }

        internal static bool CheckForCancel(string input)
        {
            return input.ToUpper() == "X";
        }

        internal static bool Confirmation()
        {
            Console.Write(" [ y / n ] ");
            ConsoleKeyInfo input = Console.ReadKey();
            return input.Key == ConsoleKey.Y;
        }
    }
}