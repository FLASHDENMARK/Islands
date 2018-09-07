using System;
using Islands.Exceptions;
using Islands.Service_Providers.Command_Console;

// Invoke a method based on string
// Collect methods with attributes more dynamically (instead of passing every class manually)
// Exposing the parameters after you've type the method name = strongly typed commands

namespace Islands
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            CommandConsole console = new CommandConsole();
            LogicProvider provider = new LogicProvider();

            console.PrintCommands();

            // Test stuff
            do
            {
                try
                {
                    string input = Console.ReadLine();
                    console.Executecommand(input);
                }
                catch (InvalidCommandException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (true);
        }
    }
}
