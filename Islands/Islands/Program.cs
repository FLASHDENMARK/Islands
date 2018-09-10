using Terminal;
using System;
using System.Reflection;

// TODO:
// Summaries
// better exceptions and error handling
// Fix Terminal namespace not working
// Condense AddInstanceCommands and AddStaticCommands into one?
// Hvis flere commands med samme navn på tværs af klasser -> specificer klassenavn
// håndter overloaded metoder?
//- Double
// - Byte
// - Long

// Nice to haves:
// Hot keys?
// User-expandable parser??
// More metadata about commands and where they are pulled from

class Program
{
    public static void Main (string[] args)
    {
        Logger logger = new Logger();

        Terminal.Terminal.AddInstanceCommands(logger);
        Terminal.Terminal.AddStaticCommands(typeof(StaticCommandsExample));

        Terminal.Terminal.PrintCommands();

        do
        {
            try
            {
                string input = Console.ReadLine();
                Command cmd = Terminal.Terminal.GetCommand(input);
                ParameterInfo[] calleeParameters = cmd.Parameters;
                int parameterCount = calleeParameters.Length;
                object[] invokeParameters = new object[parameterCount];

                for (int i = 0; i < parameterCount; i++)
                {
                    Console.WriteLine("Input: " + calleeParameters[i].ParameterType.Name);

                    string actualParameter = Console.ReadLine();
                    object objectParameter = Terminal.Terminal.ParseParameter(calleeParameters[i].ParameterType, actualParameter);

                    if (objectParameter == null)
                    {
                        Console.WriteLine("Parameter not accepted");
                        i = i - 1;
                        continue;
                    }
                    else
                        invokeParameters[i] = objectParameter;
                }

                cmd.Invoke(invokeParameters);
            }
            catch (InvalidCommandException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        while (true);
    }
}





// Add a named parameter "name" to TerminalCommand
