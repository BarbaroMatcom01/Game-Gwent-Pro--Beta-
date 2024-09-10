using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
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
            throw new RuntimeError(null, "ActionParams cannot be null.");
        }

        if (stmt.ActionBlock == null)
        {
            throw new RuntimeError(null, "ActionBlock cannot be null.");
        }

        Action action = new Action(stmt.ActionParams, stmt.ActionBlock);

        InterpretedEffect effect = new InterpretedEffect(name, @params, action);

        effects.Add(effect);
        return null;
    }

    public object VisitFunctionCall(FunctionCall expr)
    {
        object l = Evaluate(expr.LeftExpression);
        if (l is GameManager gameManager)
        {
            switch (expr.function)
            {
                case "DeckOfPlayer":
                    return gameManager.DeckOfPlayer(Convert.ToInt32(Evaluate(expr.args[0])));
                case "HandOfPlayer":
                    return gameManager.HandOfPlayer(Convert.ToInt32(Evaluate(expr.args[0])));
                case "FieldOfPlayer":
                    return gameManager.FieldOfPlayer(Convert.ToInt32(Evaluate(expr.args[0])));
                default:
                    throw new Exception();
            }
        }
        else if (l is List<CardData> cardDataList)
        {
            switch (expr.function)
            {
                case "Fined":

                case "Remove":
                    return cardDataList.Remove((CardData)Evaluate(expr.args[0]));
                case "Push":
                    object card = Evaluate(expr.args[0]);
                    if (card is CardData cardData)
                        cardDataList.Add(cardData);
                    else if (card is Card)
                    {
                        Card cardOwner = (Card)Evaluate(expr.args[0]);
                        if (cardOwner.Owner == 0)
                            Board.Instance.PlayerOneSide.Deck.AddCard((Card)card);
                        else
                            Board.Instance.PlayerTwoSide.Deck.AddCard((Card)card);
                    }
                    return typeof(void);
                case "SendBottom":
                    object cardSendBottom = Evaluate(expr.args[0]);
                    if (cardSendBottom is CardData cardDataSendBottom)
                        cardDataList.Add(cardDataSendBottom);
                    else if (cardSendBottom is Card)
                    {
                        Card cardOwner = (Card)Evaluate(expr.args[0]);
                        if (cardOwner.Owner == 0)
                            Board.Instance.PlayerOneSide.Deck.AddCard((Card)cardSendBottom);
                        else
                            Board.Instance.PlayerTwoSide.Deck.AddCard((Card)cardSendBottom);
                    }
                    return typeof(void);
                case "Pop":
                    var item = cardDataList[^1];
                    cardDataList.RemoveAt(cardDataList.Count - 1);
                    return item;
                case "Shuffle":
                    var rng = new System.Random();
                    cardDataList = cardDataList.OrderBy(a => rng.Next()).ToList();
                    return typeof(void);
                default:
                    throw new Exception();
            }
        }
        else if (l is List<Card> cardList)
        {
            switch (expr.function)

            {
                case "Fined":

                case "Push":
                    object cardPush = Evaluate(expr.args[0]);
                    CardManager.Instance.PushCardList((List<Card>)l, cardPush);
                    return typeof(void);
                case "Remove":
                    object cardRemove = Evaluate(expr.args[0]);
                    CardManager.Instance.RemoveCardList((List<Card>)l, cardRemove);
                    return typeof(void);
                case "Pop":
                    object cardPop = cardList[^1];
                    CardManager.Instance.RemoveCardList((List<Card>)l, cardPop);
                    return cardPop;
                case "Shuffle":
                    var rng = new System.Random();
                    cardList = cardList.OrderBy(a => rng.Next()).ToList();
                    return typeof(void);

                default:
                    throw new Exception();
            }
        }
        else if (l is List<object> objectList)
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
                case "Shuffle":
                    var rng = new System.Random();
                    objectList = objectList.OrderBy(a => rng.Next()).ToList();
                    return typeof(void);
                default:
                    throw new Exception();
            }
        }
        return null;
    }

    public object VisitPropertyGetter(PropertyGetter expr)
    {
        object l = Evaluate(expr.Left);
        if (l is Unit unit)
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
                case "Owner":
                    return unit.Owner;
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
                case "Owner":
                    return special.Owner;
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
                case "Owner":
                    return unitCardData.Owner;
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
                case "Owner":
                    return specialCardData.Owner;
                default:
                    throw new Exception($"Property '{expr.PropertyName}' not found.");
            }
        }
        else if (l is List<object> objectList)
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
        else if (l is GameManager gameManager)
        {
            switch (expr.PropertyName)
            {
                case "Board":
                    return GameManager.Instance.BoardCards();
                case "TriggerPlayer":
                    return (int)GameManager.Instance.CurrentPlayer;
                case "Hand":
                    return GameManager.Instance.HandOfPlayer((int)GameManager.Instance.CurrentPlayer);
                case "Field":
                    return GameManager.Instance.FieldOfPlayer((int)GameManager.Instance.CurrentPlayer);
                case "Deck":
                    return GameManager.Instance.DeckOfPlayer((int)GameManager.Instance.CurrentPlayer);
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

                    unit.Power = int.Parse(Evaluate(expr.Value).ToString());
                    break;
                default:
                    throw new Exception();
            }
        }
        else if (l is UnitCardData unitCardData)
        {
            switch (expr.PropertyName)
            {
                case "Power":

                    unitCardData.Power = int.Parse(Evaluate(expr.Value).ToString());
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