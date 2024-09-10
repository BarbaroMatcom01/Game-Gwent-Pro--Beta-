using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class Parser
{
    private Stmt EffectDeclaration()
    {
        Token nameToken = null;
        Expr nameValue = null;

        List<Token> paramsTokens = new List<Token>();
        List<Token> paramsValues = new List<Token>();

        List<Token> actionParams = new List<Token>();
        Block actionBlock = null;

        Console.WriteLine("Starting EffectDeclaration");
        Consume(TokenType.Left_Brace, "Expected '{' after 'effect'.");

        while (!Check(TokenType.Right_Brace) && !IsAtEnd())
        {
            if (Match(TokenType.Identifier))
            {
                if (Previous().Value == "Name")
                {
                    ParseProperty(ref nameToken, ref nameValue, "Name");
                }
                else if (Previous().Value == "Params")
                {
                    Consume(TokenType.Colon, "Expected ':' after 'Params Declaration'");
                    Consume(TokenType.Left_Brace, "Expected '{' after ':' in Params Declaration");

                    while (!Check(TokenType.Right_Brace) && !IsAtEnd())
                    {
                        Token paramToken = Consume(TokenType.Identifier, "Expected parameter name in Params Declaration");
                        Consume(TokenType.Colon, "Expected ':' after parameter name in Params Declaration");
                        Token paramValue = Consume(TokenType.Identifier, "Expected parameter name in Params Declaration");
                        if (paramValue.Value != "Number" && paramValue.Value != "String" && paramValue.Value != "Bool")
                        {
                            // throw new ParseError();
                            ReportError(paramValue, "Invalid parameter type in Params Declaration");
                        }

                        paramsTokens.Add(paramToken);
                        paramsValues.Add(paramValue);
                    }

                    Consume(TokenType.Right_Brace, "Expected '}' after Params Declaration");
                    Consume(TokenType.Comma, "Expected ',' after parameter value in Params Declaration");
                }
                else if (Previous().Value == "Action")
                {
                    Consume(TokenType.Colon, "Expected ':' after 'Action Declaration'");
                    Consume(TokenType.Left_Paren, "Expect '(' after 'Action'.");
                    while (!Check(TokenType.Right_Paren) && !IsAtEnd())
                    {
                        actionParams.Add(Consume(TokenType.Identifier, "Expect parameter name."));
                        if (!Check(TokenType.Right_Paren))
                        {
                            Consume(TokenType.Comma, "Expect ',' between parameters.");
                        }
                    }
                    Consume(TokenType.Right_Paren, "Expect ')' after action parameters.");


                    if (Match(TokenType.Lambda))
                    {
                        Console.WriteLine("Parsing action block...");
                        actionBlock = (Block)Statement();
                    }
                    if (actionBlock == null)
                    {
                        Console.WriteLine("Warning: actionBlock is null after Statement() call.");
                    }
                    else
                    {
                        Consume(TokenType.Right_Brace, "Expected '}' after action block.");
                        Console.WriteLine("actionBlock successfully parsed.");
                        break;
                    }

                }
                else
                {
                    // throw new ParseError();
                     ReportError(Previous(), "Unexpected identifier in Effect Declaration");
                }
            }
        }
        // Consume(TokenType.Right_Brace, "Expected '}' after card properties.");
        Console.WriteLine("Finished EffectDeclaration");

        return new EffectStmt(nameToken, nameValue, paramsTokens, paramsValues, actionParams, actionBlock);
    }
}