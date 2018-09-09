using System;

namespace Islands.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommand : Attribute
    {
        public ConsoleCommand () { }

        public string Description { get; set; }
        public bool DeveloperOnly { get; set; }
    }
}
