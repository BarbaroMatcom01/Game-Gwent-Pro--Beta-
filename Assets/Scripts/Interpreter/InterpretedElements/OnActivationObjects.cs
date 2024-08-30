using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;


public partial class OnActivationObject : InterpretedElement
{
    public EffectInfo Info { get; }
    public Selector Selector { get; }
    public OnActivationObject PostAction { get; }
    public OnActivationObject(EffectInfo info, Selector selector, OnActivationObject postAction)
    {
        Info = info;
        Selector = selector;
        PostAction = postAction;
    }
    public void ActivateEffect(List<InterpretedEffect> effects, GameManager gameManager)
    {
        var effect = effects.FirstOrDefault(e => e.Name == this.Info.Name);

        if (effect != null)
        {
            if (Selector != null)
            {
                if (Selector.Source == "deck" || Selector.Source == "otherDeck")
                {
                    effect.Action.InvokeAction(Info, GetTargetsCardData(gameManager), gameManager);
                }
                else
                {
                    effect.Action.InvokeAction(Info, GetTargetsCard(gameManager), gameManager);
                }
            }
        }
        else
        {
            Debug.LogWarning($"Efecto {this.Info.Name} no encontrado en la lista de efectos.");
        }
    }

    private List<Card> GetTargetsCard(GameManager gameManager)
    {
        if (Selector is null) return new List<Card>();
        var targetsSource = GetSourceCard(gameManager);
        var filtredCard = new List<Card>();

        foreach (var target in targetsSource)
        {
            Debug.Log(target.Name.ToString());
          
            if ((bool)Selector.Delegate.InvokeDelegate(target))
            {
                filtredCard.Add(target);
            }
        }
        if (Selector.Single)
        { 
            return filtredCard.Count > 0 ? new List<Card>() { filtredCard[0] } : new();
        }
        return filtredCard;
    }

    private List<CardData> GetTargetsCardData(GameManager gameManager)
    {
        if (Selector is null) return new List<CardData>();
        var targetsSource = GetSourceCardData(gameManager);
        var filtredCardData = new List<CardData>();

        foreach (var target in targetsSource)
        {
            if ((bool)Selector.Delegate.InvokeDelegate(target))
            {
                filtredCardData.Add(target);
            }
        }
        if (Selector.Single)
        {
            return filtredCardData.Count > 0 ? new List<CardData>() { filtredCardData[0] } : new();
        }
        return filtredCardData;
    }

    private List<Card> GetSourceCard(GameManager gameManager)
    {
        int player = (int)GameManager.Instance.CurrentPlayer;
        int otherPlayer = (player + 1) % 2;
        return Selector.Source switch
        {
            "hand" => gameManager.HandOfPlayer(player),
            "otherHand" => gameManager.HandOfPlayer(otherPlayer),
            "field" => gameManager.FieldOfPlayer(player),
            "otherField" => gameManager.FieldOfPlayer(otherPlayer),
            "board" => gameManager.BoardCards(),
            _ => throw new Exception()
        };
    }
    private List<CardData> GetSourceCardData(GameManager gameManager)
    {
        int player = (int)GameManager.Instance.CurrentPlayer;
        int otherPlayer = (player + 1) % 2;
        
        return Selector.Source switch
        {
            "deck" => gameManager.DeckOfPlayer(player),
            "otherDeck" => gameManager.DeckOfPlayer(otherPlayer),
            _ => throw new Exception()
        };
    }
}

public partial class EffectInfo : InterpretedElement
{
    public string Name { get; }
    public Dictionary<string, object> Param { get; }
    public EffectInfo(string name, Dictionary<string, object> param)
    {
        Name = name;
        Param = param;
    }
}

public partial class Selector : InterpretedElement
{
    public bool Single { get; }
    public string Source { get; }
    public Delegate Delegate { get; }
    public Selector(bool single, string source, Delegate deleg)
    {
        Single = single;
        Source = source;
        Delegate = deleg;

    }
}
public partial class Delegate : InterpretedElement
{
    public List<string> Param { get; }
    public Expr Expr { get; }

    public Delegate(List<string> param, Expr expr)
    {
        Param = param;
        Expr = expr;
    }


    public object InvokeDelegate(params object[] args)
    {
        Interpreter interpreter = new Interpreter();
        Environment delegateEnvironment = new Environment(interpreter.environment);

        for (int i = 0; i < args.Length; i++)
        {
            delegateEnvironment.Define(Param[i], args[i]);

        }
        var a = interpreter.ExecuteBlockDelegate(Expr, delegateEnvironment);
        return a;
    }
}