using System;
using Islands.Attributes;

namespace Islands
{
    public class Logger
    {
        public bool isLogging = true;

        [ConsoleCommand(Description = "Description for MethodWithAttribute", DeveloperOnly = true)]
        public void MethodWithAttribute (bool log, int number, string str)
        {
            Console.WriteLine("METHODWITHATTRIBUTE: " + log + " " + number + " " + str);
            isLogging = !isLogging;
        }

        [ConsoleCommand(Description = "Enables / disables logging")]
        public void LogState (bool state)
        {
            Console.WriteLine("Log State called through reflection! STATE = " + state);
            isLogging = state;
        }

        [ConsoleCommand(Description = "Sets the log level. 0 = Informational, 1 = Warnings, 2 = Errors, 3 = All")]
        public void LogLevel ()
        {
            Console.WriteLine("Log Level called through reflection!");
        }
    }
}
