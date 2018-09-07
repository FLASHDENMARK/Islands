using System;
using System.Linq;
using Islands.Attributes;
using System.Reflection;
using Islands.Exceptions;

namespace Islands.Service_Providers.Command_Console
{
    public class CommandConsole
    {
        MethodInfo[] Commands = GetCommands();

        // Prints all console commands
        public void PrintCommands ()
        { 
            foreach (MethodInfo cmd in this.Commands)
            {
                string parameters = string.Join(", ", cmd.GetParameters().Select(x => x.ParameterType.Name));
                Console.WriteLine(string.Format("{0}.{1}({2})", cmd.DeclaringType.Name, cmd.Name, parameters));
            }  
        }

        public void Executecommand (string input)
        {
            MethodInfo cmd = ParseInput(input);

            // Execute the cmd right here!
        }

        MethodInfo ParseInput (string input)
        {
            if (Commands.Any(c => c.Name.ToLower() == input.ToLower()))
            {
                return Commands.First(c => c.Name.ToLower() == input.ToLower());
                // Check parameters
                // Build a command
                // return a command
                // return null;
            }
            else
            {
                throw new InvalidCommandException("The command '" + input + "' could not be found.");
            }
        }

        // Returns every method decorated with the [ConsoleCommand] attribute
        private static MethodInfo[] GetCommands ()
        {
            MethodInfo[] methodInfo = typeof(Logger).GetMethods().
                Where(i => i.GetCustomAttribute(typeof(ConsoleCommand)) != null).ToArray<MethodInfo>();

            return methodInfo;
        }
    }
}
