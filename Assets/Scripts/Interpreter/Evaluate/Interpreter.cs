   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
    public partial class Interpreter : IVisitorExp<object>, IVisitorStmt<object>
    {
        public Environment environment = new Environment();
        private List<InterpretedCard> cards;
        private List<InterpretedEffect> effects;

        public Interpreter()
        {
            cards = new List<InterpretedCard>();
            effects = new List<InterpretedEffect>();
        }
    }
