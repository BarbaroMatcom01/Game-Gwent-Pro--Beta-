using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class Parser
{
    private Expr DotChainExpressions()
    {
        Expr left = Primary();

        while (Check(TokenType.Dot) || Check(TokenType.Left_Brackets) || Check(TokenType.Plus_Plus) || Check(TokenType.Minus_Minus))
        {
            if (MatchPrefix(TokenType.Dot, TokenType.Identifier, TokenType.Left_Paren))
            {
                left = FunctionCall(left);
            }
            else if (MatchPrefix(TokenType.Dot, TokenType.Identifier))
            {
                left = PropertyGetter(left);
            }

            else if (MatchPrefix(TokenType.Plus_Plus))
            {
                left = Increment(left);
            }
            else if (MatchPrefix(TokenType.Minus_Minus))
            {
                left = Decrement(left);
            }
            else
            {
                left = Indexer(left);
            }
        }
        return left;
    }

    private Expr FunctionCall(Expr expr)
    {
        Consume(TokenType.Dot, "Expected '.'");
        string id = Consume(TokenType.Identifier, "Expected 'identifier'").Value;
        List<Expr> args;
        Consume(TokenType.Left_Paren, "Expected '('");
        if (Check(TokenType.Right_Paren))
        {
            args = new();
        }
        else
        {
            args = new()
                {
                    Expression()
                };
            while (Match(TokenType.Comma))
            {
                args.Add(Expression());
            }

        }
        Consume(TokenType.Right_Paren, "Expected ')'");
        return new FunctionCall(expr, id, args);
    }
    private Expr PropertyGetter(Expr expr)
    {
        Consume(TokenType.Dot, "Expected '.'");
        string id = Consume(TokenType.Identifier, "Expected 'identifier'").Value;
        return new PropertyGetter(expr, id);
    }
    private Expr Increment(Expr expr)
    {
         var tok = Consume(TokenType.Plus_Plus, "Expected '++' after expression.");
            var incrementedExpr = new Binary(expr, new Token(TokenType.Plus, "+", tok.Line, tok.Column), new Literal(1));
            return AssignIncrementOrDecrement(expr, incrementedExpr);
    }
    private Expr Decrement(Expr expr)
    {
         var tok = Consume(TokenType.Minus_Minus, "Expected '--' after expression.");
            var decrementedExpr = new Binary(expr, new Token(TokenType.Minus, "-", tok.Line, tok.Column), new Literal(1));
            return AssignIncrementOrDecrement(expr, decrementedExpr);
    }
     private Expr AssignIncrementOrDecrement(Expr variable, Expr value)
        {
            if (variable is Variable varExpr)
            {
                return new Assign(varExpr.Name, value);
            }
            throw new ParseError();
        }
    private Expr Indexer(Expr expr)
    {
        Consume(TokenType.Left_Brackets, "Expected '['");
        Expr exp = Expression();
        Consume(TokenType.Right_Brackets, "Expected ']'");
        return new PropertyGetter(expr, "Indexer", new List<Expr> { exp });
    }
}