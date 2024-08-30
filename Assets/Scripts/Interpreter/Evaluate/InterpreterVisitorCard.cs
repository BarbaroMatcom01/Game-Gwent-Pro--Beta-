using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class Interpreter
{
    public object VisitCardStmt(CardStmt stmt)
    {
        string type = (string)Evaluate(stmt.TypeValue);
        string name = (string)Evaluate(stmt.NameValue);
        string faction = (string)Evaluate(stmt.FactionValue);
        string range = (string)Evaluate(stmt.RangeValue);
        object powerValue = Evaluate(stmt.PowerValue);
        int power;
        if (powerValue is string powerString && int.TryParse(powerString, out power))
        {
        }
        else if (powerValue is int powerInt)
        {
            power = powerInt;
        }
        else
        {
            throw new InvalidCastException("Unable to cast PowerValue to int.");
        }
        string description = (string)Evaluate(stmt.DescriptionValue);

        List<OnActivationObject> onActivation = new();
        for (int i = 0; i < stmt.OnActivationExprs.Count; i++)
        {
            onActivation.Add((OnActivationObject)Evaluate(stmt.OnActivationExprs[i]));
        }

        InterpretedCard card = new InterpretedCard(name, type, faction, range, power, description, onActivation);
        cards.Add(card);

        return null;
    }

    public object VisitOnActivationExpr(OnActivationExpr expr)
    {
        if (expr.OnActivation == null)
        {
            if (expr.SelectorExpr == null)
            {
                return new OnActivationObject((EffectInfo)Evaluate(expr.EffectInfoExpr), null, null);
            }
            else
            {
                return new OnActivationObject((EffectInfo)Evaluate(expr.EffectInfoExpr), (Selector)Evaluate(expr.SelectorExpr), null);
            }
        }
        {
            if (expr.SelectorExpr == null)
            {
                return new OnActivationObject((EffectInfo)Evaluate(expr.EffectInfoExpr), null, (OnActivationObject)Evaluate(expr.OnActivation));
            }
            else
            {
                return new OnActivationObject((EffectInfo)Evaluate(expr.EffectInfoExpr), (Selector)Evaluate(expr.SelectorExpr), (OnActivationObject)Evaluate(expr.OnActivation));
            }
        }
    }
    public object VisitEffectInfoExpr(EffectInfoExpr expr)
    {
        Dictionary<string, object> parm = new Dictionary<string, object>();

        foreach (var item in expr.Param)
        {
            parm.Add(item.Key, Evaluate(item.Value));
        }

        return new EffectInfo((string)Evaluate(expr.Name), parm);
    }

    public object VisitSelectorExpr(SelectorExpr expr)
    {

        return new Selector((bool)Evaluate(expr.Single), (string)Evaluate(expr.Source), (Delegate)Evaluate(expr.DelExpr));
    }

    public object VisitDelegateExpr(DelegateExpr expr)
    {
        return new Delegate(expr.ParamasDelegate,expr.Expr);
    }
}