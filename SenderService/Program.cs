using SenderService.Administration;
using System;

namespace SenderService
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputService.Write("Type [exit] to exit.", true, false, null);
            var consoleString = string.Empty;
            while (string.Compare(consoleString, "exit", StringComparison.OrdinalIgnoreCase) != 0)
            {
                Console.Write("> ");
                consoleString = Console.ReadLine();
            }
        }
    }
}
