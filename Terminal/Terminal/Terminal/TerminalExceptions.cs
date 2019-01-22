﻿using System;

namespace Terminal
{
    class InvalidCommandException : Exception
    {
        public InvalidCommandException () { }

        public InvalidCommandException (string message) : base (message) { }

        public InvalidCommandException (string message, Exception inner) : base (message, inner) { } 
    }
}