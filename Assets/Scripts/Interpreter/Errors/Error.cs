   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
    public class Error
    {
        public string Value { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public string Messege { get; set; }

        public Error(string value, int line, int column, string messege)
        {
            Value = value;
            Line = line;
            Column = column;
            Messege = messege;
        }
    }
