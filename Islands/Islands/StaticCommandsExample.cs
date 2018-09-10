using System;
using Terminal;

public static class StaticCommandsExample
{
    [TerminalCommand(Description = "Example 1")]
    public static void Example1 ()
    {
        Console.WriteLine("Example 1");
    }

    [TerminalCommand(Description = "Example 2 int")]
    public static void Example2 (int example)
    {
        Console.WriteLine("Example 2: " + example);
    }

    [TerminalCommand(Description = "Example 2 string")]
    public static void Example2(string example)
    {
        Console.WriteLine("Example 2: " + example);
    }

    [TerminalCommand(Description = "Example 3")]
    public static void Example3 (string example, float example2)
    {
        Console.WriteLine("Example 3 " + example + "" + example2);
    }

    [TerminalCommand(Description = "Example 4")]
    public static void Example4 (bool example, string example2, int example3)
    {
        Console.WriteLine("Example 4 " + example + example2 + example3);
    }

    [TerminalCommand(Name = "NAME OVERWRITTEN", Description = "overwritten name example")]
    public static void ExampleName (bool example, string example2, int example3)
    {
        Console.WriteLine("Example 4 " + example + example2 + example3);
    }
}

