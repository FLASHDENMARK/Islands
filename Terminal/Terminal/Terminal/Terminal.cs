using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Terminal
{
    public static class Terminal
    {
        static List<Command> Commands = new List<Command>();

        /// <summary>
        /// Adds commands from a class belonging to an intance
        /// </summary>
        /// <param name="instance"></param>
        public static void AddInstanceCommands (object instance)
        {
            Type type = instance.GetType();
            MethodInfo[] methods = type.GetMethods().
                Where(s => s.GetCustomAttribute(typeof(TerminalCommand)) != null).ToArray<MethodInfo>();

            foreach (MethodInfo mInfo in methods)
            {
                Command command = new Command(instance, mInfo);
                Commands.Add(command);
            }
        }

        /// <summary>
        /// Adds commands from a static class
        /// </summary>
        /// <param name="staticInstance"></param>
        public static void AddStaticCommands (Type classType)
        {
            MethodInfo[] methods = classType.GetMethods().
                Where(i => i.GetCustomAttribute(typeof(TerminalCommand)) != null).ToArray<MethodInfo>();

            foreach (MethodInfo mInfo in methods)
            {
                Command command = new Command(null, mInfo);
                Commands.Add(command);
            }
        }

        /// <summary>
        /// Returns a command with a specific name
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Command GetCommand (string input)
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
        /// Parses 'parameter' to 'type' if possible. Returns null otherwise
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Prints every command available
        /// </summary>
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