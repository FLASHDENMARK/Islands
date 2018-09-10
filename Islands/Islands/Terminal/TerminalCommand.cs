using System;

namespace Terminal
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TerminalCommand : Attribute
    {
        public TerminalCommand () { }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
