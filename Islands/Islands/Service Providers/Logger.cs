using System;
using Islands.Attributes;
using System.Collections.Generic;
using System.Text;

namespace Islands
{
    public class Logger
    {
        public bool isLogging = true;

        [ConsoleCommand]
        public void MethodWithAttribute (bool log, int number, string str)
        {
            isLogging = !isLogging;
        }

        [ConsoleCommand]
        public void LogState (bool state)
        {
            isLogging = state;
        }

        [ConsoleCommand]
        public void LogLevel ()
        {
            Console.WriteLine("Log Level called through reflection!");
        }
    }
}
