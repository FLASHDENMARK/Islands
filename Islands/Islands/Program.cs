using System;
using System.Reflection;
using Islands.Exceptions;
using Islands.Service_Providers.Command_Console;

// TODO:
// Make work on static classes
// Condense namespaces
// Summaries
// Remove named parameter 'developer'?

namespace Islands
{
    class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = new Logger();
            CommandConsole.AddCommands(logger);

            LogicProvider provider = new LogicProvider();
            CommandConsole.AddCommands(provider);

            CommandConsole.PrintCommands();

            do
            {
                try
                {
                    string input = Console.ReadLine();
                    Command cmd = CommandConsole.ParseInput(input);
                    ParameterInfo[] calleeParameters = cmd.Parameters;
                    int parameterCount = calleeParameters.Length;
                    object[] invokeParameters = new object[parameterCount];

                    for (int i = 0; i < parameterCount; i++)
                    {
                        Console.WriteLine("Input: " + calleeParameters[i].ParameterType.Name);

                        string actualParameter = Console.ReadLine();

                        object objectParameter = CommandConsole.ParseParameter(calleeParameters[i].ParameterType, actualParameter);

                        if (objectParameter == null)
                        {
                            Console.WriteLine("Paremeter not accepted");
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
}