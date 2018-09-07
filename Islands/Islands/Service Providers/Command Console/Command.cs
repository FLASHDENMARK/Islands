using System;
using System.Reflection;

namespace Islands.Service_Providers.Command_Console
{
    class Command
    {
        public string Name { get; set; }
        public ParameterInfo[] Parameters { get; set; }

        public Command (string name, ParameterInfo[] parameters)
        {
            this.Name = name;
            this.Parameters = parameters;
        }

        public override string ToString ()
        {
            string result = this.Name + " (";

            foreach (ParameterInfo parameter in this.Parameters)
            {
                result += parameter.ParameterType.ToString() + " ";
                result += parameter.Name + ", ";
            }

            result += ")";

            return result;
        }
    }
}
