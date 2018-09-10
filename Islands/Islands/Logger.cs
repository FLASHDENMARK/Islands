using System;
using Terminal;

public class Logger
{
    public bool isLogging = true;

    [TerminalCommand(Description = "Description for MethodWithAttribute")]
    public void MethodWithAttribute (bool log, int number, string str)
    {
        Console.WriteLine("METHODWITHATTRIBUTE: " + log + " " + number + " " + str);
        isLogging = !isLogging;
    }

    [TerminalCommand(Description = "Enables / disables logging")]
    public void LogState (bool state)
    {
        Console.WriteLine("Log State called through reflection! STATE = " + state);
        isLogging = state;
    }

    [TerminalCommand(Description = "Sets the log level. 0 = Informational, 1 = Warnings, 2 = Errors, 3 = All")]
    public void LogLevel ()
    {
        Console.WriteLine("Log Level called through reflection!");
    }

    [TerminalCommand(Name = "NAME OVERWRITTEN", Description = "overwritten name example")]
    public static void ExampleName(bool example, string example2, int example3)
    {
        Console.WriteLine("Example 4 " + example + example2 + example3);
    }
}

