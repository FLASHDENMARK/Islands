using System;

namespace Islands
{
    class ParseToken : ICloneable 
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public ParseToken (string value, TokenType type)
        {
            this.Value = value;
            this.Type = type;
        }

        public override string ToString ()
        {
            return "<" + Type + ", \'" + Value + "\'>";
        }

        public object Clone ()
        {
            return this.MemberwiseClone();
        }
    }
}
