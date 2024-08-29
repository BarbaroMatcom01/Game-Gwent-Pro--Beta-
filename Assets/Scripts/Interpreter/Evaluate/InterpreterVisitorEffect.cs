using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class Interpreter
{
    public object VisitEffectStmt(EffectStmt stmt)
    {
        string name = (string)Evaluate(stmt.NameValue);

        Dictionary<string, object> @params = new Dictionary<string, object>();
        for (int i = 0; i < stmt.ParamsTokens.Count; i++)
        {
            string paramName = stmt.ParamsTokens[i].Value;
            object paramValue = stmt.ParamsValues[i].Value;
            @params[paramName] = paramValue;
        }

        if (stmt.ActionParams == null)
        {
            throw new ArgumentNullException(nameof(stmt.ActionParams), "ActionParams cannot be null.");
        }

        if (stmt.ActionBlock == null)
        {
            throw new ArgumentNullException(nameof(stmt.ActionBlock), "ActionBlock cannot be null.");
        }

        Action action = new Action(stmt.ActionParams, stmt.ActionBlock);

        InterpretedEffect effect = new InterpretedEffect(name, @params, action);

        effects.Add(effect);
        return null;
    }

    public object VisitFunctionCall(FunctionCall expr)
    {
        object l = Evaluate(expr.LeftExpression);
        if (l is List<object> objectList)
        {
            switch (expr.function)
            {
                case "Remove":
                    return objectList.Remove(Evaluate(expr.args[0]));
                case "Push":
                    objectList.Add(Evaluate(expr.args[0]));
                    return typeof(void);
                case "Pop":
                    var item = objectList[^1];
                    objectList.RemoveAt(objectList.Count - 1);
                    return item;
                default:
                    throw new Exception();
            }
        }
        return null;
    }

    public object VisitPropertyGetter(PropertyGetter expr)
    {
        object l = Evaluate(expr.Left);
        if (l is List<object> objectList)
        {
            switch (expr.PropertyName)
            {
                case "Count":
                    return objectList.Count;
                case "Indexer":
                    return objectList[Convert.ToInt32(Evaluate(expr.Args[0]))];
                default:
                    throw new Exception();
            }
        }
        else if (l is List<Card> cardList)
        {
            switch (expr.PropertyName)
            {
                case "Count":
                    return double.Parse(cardList.Count.ToString());
                case "Indexer":
                    return cardList[Convert.ToInt32(Evaluate(expr.Args[0]))];
                default:
                    throw new Exception();
            }
        }

        else if (l is List<CardData> cardDataList)
        {
            switch (expr.PropertyName)
            {
                case "Count":
                    return double.Parse(cardDataList.Count.ToString());
                case "Indexer":
                    return cardDataList[Convert.ToInt32(Evaluate(expr.Args[0]))];
                default:
                    throw new Exception();
            }
        }
        else if (l is Unit unit)
        {
            switch (expr.PropertyName)
            {
                case "Name":
                    return unit.Name;
                case "Faction":
                    return unit.Faction;
                case "Power":
                    return unit.Power;
                case "Range":
                    return unit.AttackType;
                case "Type":
                    return unit.UnitType;
                default:
                    throw new Exception($"Property '{expr.PropertyName}' not found.");
            }
        }
        else if (l is Special special)
        {
            switch (expr.PropertyName)
            {
                case "Name":
                    return special.Name;
                case "Faction":
                    return special.Faction;
                case "Type":
                    return special.SpecialType;
                default:
                    throw new Exception($"Property '{expr.PropertyName}' not found.");
            }
        }
        else if (l is UnitCardData unitCardData)
        {
            switch (expr.PropertyName)
            {
                case "Name":
                    return unitCardData.Name;
                case "Faction":
                    return unitCardData.Faction;
                case "Power":
                    return unitCardData.Power;
                case "Range":
                    return unitCardData.AttackType;
                case "Type":
                    return unitCardData.UnitType;
                default:
                    throw new Exception($"Property '{expr.PropertyName}' not found.");
            }
        }
        else if (l is SpecialCardData specialCardData)
        {
            switch (expr.PropertyName)
            {
                case "Name":
                    return specialCardData.Name;
                case "Faction":
                    return specialCardData.Faction;
                case "Type":
                    return specialCardData.SpecialType;
                default:
                    throw new Exception($"Property '{expr.PropertyName}' not found.");
            }
        }
        else if (l is GameManager gameManager)
        {
            switch (expr.PropertyName)
            {
                case "Board":
                    return gameManager.BoardCards();
                case "TriggerPlayer":
                    return gameManager.TriggerPlayer;
                case "Hand":
                    return gameManager.HandOfPlayer(gameManager.TriggerPlayer);
                case "Field":
                    return gameManager.FieldOfPlayer(gameManager.TriggerPlayer);
                case "Deck":
                    return gameManager.DeckOfPlayer(gameManager.TriggerPlayer);
                default:
                    throw new Exception($"Property '{expr.PropertyName}' not found.");
            }
        }
        return null;
    }

    public object VisitPropertySetter(PropertySetter expr)
    {
        object l = Evaluate(expr.Left);
        if (l is Unit unit)
        {
            switch (expr.PropertyName)
            {
                case "Power":
                   
                      unit.Power = (int)Evaluate(expr);
                       break;
                    
                default:
                    throw new Exception();
            }
        }
        else if (l is List<object> objectList)
        {
            switch (expr.PropertyName)
            {

                case "Indexer":
                    var value = Evaluate(expr.Value);
                    objectList[Convert.ToInt32(Evaluate(expr.Args[0]))] = value;
                    return value;
                default:
                    throw new Exception();
            }
        }
        return null;
    }
}