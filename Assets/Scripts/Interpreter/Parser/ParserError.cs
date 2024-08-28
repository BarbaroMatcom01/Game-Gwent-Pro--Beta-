   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
    public partial class Parser
    {
        private class ParseError : Exception { }
        private ParseError Error(Token token, string message)
        {
            return new ParseError();
        }
    }