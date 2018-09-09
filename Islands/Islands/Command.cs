using Islands.Attributes;
using System;
using System.Reflection;

namespace Islands.Service_Providers.Command_Console
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
            this.Instance = instance;
            this.Method = method;
            this.Name = method.Name;
            this.Class = method.DeclaringType.ToString();
            this.Description = ((ConsoleCommand)Attribute.GetCustomAttribute(method, typeof(ConsoleCommand))).Description;
            this.Parameters = Method.GetParameters();
        }

        public void Invoke (object[] parameters)
        {
            this.Method.Invoke(this.Instance, parameters);
        }

        public void Invoke ()
        {
            this.Method.Invoke(this.Instance, null);
        }
    }
}