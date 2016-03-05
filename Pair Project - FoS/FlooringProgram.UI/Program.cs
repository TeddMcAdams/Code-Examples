using FlooringProgram.UI.Workflows;

namespace FlooringProgram.UI
{
    internal static class Program
    {
        private static void Main()
        {
            var menu = new MainMenu();
            menu.Execute();
        }
    }
}