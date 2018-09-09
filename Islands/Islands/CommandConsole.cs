using System;
using System.Linq;
using System.Reflection;
using Islands.Exceptions;
using Islands.Attributes;
using System.Collections.Generic;

namespace Islands.Service_Providers.Command_Console
{
    public static class CommandConsole
    {
        static List<Command> Commands = new List<Command>();

        // Collects every method in the object decorated with a [ConsoleCommand] attribute
        public static void AddCommands (object instance)
        {
            Type type = instance.GetType();
            MethodInfo[] methods = type.GetMethods().
                Where(s => s.GetCustomAttribute(typeof(ConsoleCommand)) != null).ToArray<MethodInfo>();

            foreach (MethodInfo mInfo in methods)
            {
                Command command = new Command(instance, mInfo);
                Commands.Add(command);
            }
        }

        public static Command ParseInput (string input)
        {
            string trimmedInput = input.Trim(' ').ToLower();

            if (Commands.Any(c => c.Name.ToLower() == trimmedInput))
            {
                return Commands.First(c => c.Name.ToLower() == trimmedInput);
            }
            else
            {
                throw new InvalidCommandException("The command '" + input + "' could not be found.");
            }
        }

        /// <summary>
        /// Checks if 'parameter' can be parsed to a specific type. If so it is returned in 'result' as an object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <param name="result"></param>
        /// <returns>Returns true or false. Will output result if true</returns>
        public static object ParseParameter (Type type, string parameter)
        {
            if (type == typeof(Boolean))
            {
                if (bool.TryParse(parameter, out bool res))
                    return res;
                else
                    return null;
            }
            else if (type == typeof(int))
            {
                if (int.TryParse(parameter, out int res))
                    return res;
                else
                    return null;
            }
            else if (type == typeof(float))
            {
                if (float.TryParse(parameter, out float res))
                    return res;
                else
                    return null;
            }
            // This is technically a catch all
            else
            {
                return parameter;
            }
        }

        // Prints all console commands
        public static void PrintCommands ()
        {
            foreach (Command cmd in Commands)
            {
                string parameters = string.Join(", ", cmd.Method.GetParameters().Select(x => x.ParameterType.Name));

                Console.WriteLine(string.Format("{0}.{1}({2}) // {3}", cmd.Class, cmd.Name, parameters, cmd.Description));
            }
        }
    }
}