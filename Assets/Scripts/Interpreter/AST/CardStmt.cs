   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
     public class CardStmt : Stmt
    {
         public Token Type { get; }
        public Expr TypeValue { get; }
        public Token Name { get; }
        public Expr NameValue { get; }
        public Token Faction { get; }
        public Expr FactionValue { get; }
        public Token Power { get; }
        public Expr PowerValue { get; }
        public Token Range { get; }
        public Expr RangeValue { get; }
        public Token Description { get; }
        public Expr DescriptionValue { get; }
        public List<OnActivationExpr> OnActivationExprs { get; }

        public CardStmt(Token type, Expr typeValue, Token name, Expr nameValue, Token faction, Expr factionValue, Token power, Expr powerValue, Token range, Expr rangeValue, Token description, Expr descriptionValue, List<OnActivationExpr> onActivationExprs)
        {
            Type = type;
            TypeValue = typeValue;
            Name = name;
            NameValue = nameValue;
            Faction = faction;
            FactionValue = factionValue;
            Power = power;
            PowerValue = powerValue;
            Range = range;
            RangeValue = rangeValue;
            Description = description;
            DescriptionValue = descriptionValue;
            OnActivationExprs = onActivationExprs;
        }

        public override T Accept<T>(IVisitorStmt<T> visitor)
        {
            return visitor.VisitCardStmt(this);
        }
    }