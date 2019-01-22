using System;
using System.Reflection;

namespace Terminal
{
    public class Command
    {
        public object Instance { get; private set; }
        public MethodInfo Method { get; private set; }
        public string Name { get; private set; }
        public string Class { get; private set; }
        public string Description { get; private set; }
        public ParameterInfo[] Parameters { get; private set; }

        public Command (object instance, MethodInfo method)
        {
            TerminalCommand attribute = GetAttributeData(method);

            this.Instance = instance;
            this.Method = method;
            this.Class = method.DeclaringType.ToString();
            this.Description = attribute.Description;
            this.Parameters = method.GetParameters();

            this.Name = attribute.Name ?? method.Name;
        }

        public void Invoke (object[] parameters)
        {
            this.Method.Invoke(this.Instance, parameters);
        }

        public void Invoke ()
        {
            this.Method.Invoke(this.Instance, null);
        }

        private TerminalCommand GetAttributeData (MethodInfo method)
        {
            return (TerminalCommand)Attribute.GetCustomAttribute(method, typeof(TerminalCommand));
        }
    }
}