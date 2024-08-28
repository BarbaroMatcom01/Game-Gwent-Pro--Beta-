   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
    public partial class Parser
    {
        private readonly List<Token> tokens;
        private int current = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public List<Stmt> Parse()
        {
            List<Stmt> statements = new List<Stmt>();
            while (!IsAtEnd())
            {
                Stmt stmt = Declaration();
                if (stmt != null)
                {
                    Console.WriteLine("Parsed statement: " + stmt.GetType().Name);
                    statements.Add(stmt);
                }
                else
                {
                    Console.WriteLine("No statement parsed.");
                }
            }
            return statements;
        }
    }