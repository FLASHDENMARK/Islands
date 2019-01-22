using System;
using System.Collections.Generic;
using System.Text;

namespace Islands
{
    class CommandParser
    {
        private string Input { get; set; }
        private int CurrentIndex { get; set; }

        private char CurrentChar { get { return this.Input[this.CurrentIndex]; }  }
        private bool IsDone { get { return CurrentIndex == Input.Length; } }

        private Stack<ParseToken> Tokens = new Stack<ParseToken>();

        public CommandParser (string input)
        {
            this.Input = input;
            this.CurrentIndex = 0;
        }
     
        public void Parse ()
        {
            if (this.Input.Length == 0)
            {
                throw new Exception("Input cannot be empty.");
            }

            while (!IsDone)
            {
                if (char.IsLetter(CurrentChar))
                {
                    ScanLetters();
                }
                else if (char.IsDigit(CurrentChar))
                {
                    ScanDigits();
                }
                /*else if (CurrentChar == '"')
                {
                    ScanString();
                }*/
                /*else if (char.IsWhiteSpace(CurrentChar))
                {
                    Advance();
                }*/
                else if (CurrentChar == ',' || CurrentChar == ' ')
                {
                    ScanSeparator();
                }
                else
                {
                    throw new Exception("Invalid char: " + CurrentChar);
                }
            }
        }

        // Assumes that an identifier is present first
        // Assumes that a literal followes immediately after the identifier (separators are ignored)
        public bool ValidateTokenSequence ()
        {
            Stack<ParseToken> sequence = new Stack<ParseToken>();

            foreach (ParseToken p in Tokens)
            {
                sequence.Push(p.Clone() as ParseToken);
            }

            if (sequence.Pop().Type == TokenType.IDENTIFIER)
            {
                Console.WriteLine("First Pop is Identifier");

                while (sequence.Pop().Type == TokenType.SEPARATOR)
                {
                    Console.WriteLine("Skipping separator");
                    continue;
                }

                if (sequence.Pop().Type == TokenType.INTEGER_LITERAL)
                {
                    Console.WriteLine("Integer Literal identified!");
                    return true;
                }
                else
                    return false;
            }
            else
                return false; 
        }

        private void Advance ()
        {
            CurrentIndex++;
        }

        private void AddToken(string value, TokenType type)
        {
            Tokens.Push(new ParseToken(value, type));
        }

        public void PrintTokens ()
        {
            foreach (ParseToken t in Tokens)
            {
                Console.WriteLine(t.ToString());
            }
        }

        #region Scanners

        private void ScanLetters ()
        {
            StringBuilder builder = new StringBuilder();

            while (!IsDone && char.IsLetter(CurrentChar))
            {
                builder.Append(CurrentChar);

                Advance();
            }

            AddToken(builder.ToString(), TokenType.IDENTIFIER);
        }

        private void ScanDigits ()
        {
            StringBuilder builder = new StringBuilder();

            while (!IsDone && char.IsDigit(CurrentChar))
            {
                builder.Append(CurrentChar);

                Advance();
            }

            AddToken(builder.ToString(), TokenType.INTEGER_LITERAL);
        }

        private void ScanString ()
        {
            StringBuilder builder = new StringBuilder();

            while (!IsDone && (char.IsLetter(CurrentChar) || char.IsDigit(CurrentChar)))
            {
                if (CurrentChar == '"')
                {
                    Advance();
                    builder.Append(CurrentChar);
                    Advance();
                }
                else
                {

                }
            }

            AddToken(builder.ToString(), TokenType.STRING_LITERAL);
        }

        private void ScanSeparator ()
        {
            AddToken(CurrentChar.ToString(), TokenType.SEPARATOR);

            Advance();
        }

        #endregion Scanners
    }
}
