using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Resources;
using TMPro;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private DeckData deckData;
    public List<InterpretedEffect> effects = new();
    public static CardFactory Instance;
    void Start()
    {
        Instance = this;

        Debug.Log("CardFactory instance initialized.");
    }

    public void ProcessInput(string inputText)
    {

        // string filePath = @"C:\Users\Barbaro\Documents\Personal\Estudio\Programación\Proyectos Pro\Proyectos de la escuela\Proyecto Gwent Pro\Gwwn-Pro (Segundo Proyecto)\Assets\Scripts\Interpreter\Input\input.txt";

        string input = File.ReadAllText(inputText);

        List<Token> listTokens = Lexer.LexicalAnalysis(input);

        if (Lexer.HasErrors())
        {
            Console.WriteLine("Errors were found during lexical analysis:");
            foreach (var error in Lexer.GetErrors())
            {
                Console.WriteLine($"Line {error.Line}, Column {error.Column}: {error.Value} {error.Message}");
                ErrorReporter.Instance.Report($"Line {error.Line}, Column {error.Column}: {error.Value} {error.Message}");

            }
            return;
        }

        Parser parser = new Parser(listTokens);
        List<Stmt> statements = parser.Parse();

        if (parser.SyntaxError.HasErrors())
        {
            Console.WriteLine("Errors were found during parsing:");
            foreach (var error in parser.SyntaxError.Errors)
            {
                Console.WriteLine($"Line {error.Line}, Column {error.Column}: {error.Value} {error.Message}");
                ErrorReporter.Instance.Report($"Line {error.Line}, Column {error.Column}: {error.Value} {error.Message}");
            }
            return;
        }


        Interpreter interpreter = new Interpreter();
        interpreter.Interpret(statements);

        List<InterpretedCard> interpretedCards = interpreter.GetCards();

        foreach (var effect in interpreter.GetEffects())
        {
            effects.Add(effect);
        }

        foreach (var card in interpretedCards)
        {
            Debug.Log($"Carta: {card.Name}, Tipo: {card.Type}, Facción: {card.Faction}, Rango: {card.Range}, Poder: {card.Power},Desciption :{card.Description}");
        }

        foreach (var effect in effects)
        {
            Debug.Log($"Effect Name: {effect.Name}");
            foreach (var param in effect.Params)
            {
                Debug.Log($"  Param: {param.Key}, Type: {param.Value}");
            }
        }

        ConvertInterpretedCards(interpretedCards, deckData);
    }

    public void DeleteInterpreterCard()
    {
        foreach (var Card in deckData.DeckCards)
        {
            if (Card.Factory == "Interpreter")
            {
                deckData.DeckCards.Remove(Card);
            }
        }
    }
    void ConvertInterpretedCards(List<InterpretedCard> interpretedCards, DeckData deckData)
    {
        foreach (var interpretedCard in interpretedCards)
        {
            CardData cardData;

            if (interpretedCard.Type == "Golden" || interpretedCard.Type == "Silver")
            {
                UnitCardData unitCardData = ScriptableObject.CreateInstance<UnitCardData>();
                unitCardData.Name = interpretedCard.Name;
                unitCardData.Faction = interpretedCard.Faction;
                unitCardData.Power = interpretedCard.Power;
                unitCardData.Description = interpretedCard.Description;
                unitCardData.Skill = Skills.Special;
                unitCardData.Factory = "Interpreter";

                AttackType attackType;
                Enum.TryParse(interpretedCard.Range, out attackType);
                unitCardData.AttackType = attackType;

                UnitType unitType;
                Enum.TryParse(interpretedCard.Type, out unitType);
                unitCardData.UnitType = unitType;

                unitCardData.CardImage = GetUnitCardImage(unitCardData.AttackType);
                unitCardData.TypeIcon = GetUnitTypeIcon(unitCardData.AttackType);
                unitCardData.PowerImage = GetPowerImage(unitCardData.Power, unitCardData.UnitType);

                unitCardData.OnActivation = interpretedCard.OnActivation;
                cardData = unitCardData;
            }
            else if (interpretedCard.Type == "Special")
            {
                SpecialCardData specialCardData = ScriptableObject.CreateInstance<SpecialCardData>();
                specialCardData.Name = interpretedCard.Name;
                specialCardData.Faction = interpretedCard.Faction;
                specialCardData.Description = interpretedCard.Description;
                specialCardData.Factory = "Interpreter";

                SpecialType specialType;
                Enum.TryParse(interpretedCard.Range, out specialType);
                specialCardData.SpecialType = specialType;

                specialCardData.CardImage = GetSpecialCardImage(specialCardData.SpecialType);
                specialCardData.TypeIcon = GetSpecialTypeIcon(specialCardData.SpecialType);
                specialCardData.OnActivation = null;

                cardData = specialCardData;
            }
            else
            {
                Debug.LogError("Tipo de carta desconocido: " + interpretedCard.Type);
                continue;
            }

            deckData.DeckCards.Add(cardData);
        }
    }
    Sprite GetUnitCardImage(AttackType attackType)
    {
        if (attackType == AttackType.Melee)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/M");
        }
        else if (attackType == AttackType.Ranged)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/R");
        }
        else if (attackType == AttackType.Siege)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/S");
        }
        return null;
    }
    Sprite GetSpecialCardImage(SpecialType specialType)
    {
        if (specialType == SpecialType.Rain)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/LL");
        }
        else if (specialType == SpecialType.Storm)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/T");
        }
        else if (specialType == SpecialType.Snow)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/N");
        }
        return null;
    }
    Sprite GetUnitTypeIcon(AttackType attackType)
    {
        if (attackType == AttackType.Melee)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/m");
        }
        else if (attackType == AttackType.Ranged)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/r");
        }
        else if (attackType == AttackType.Siege)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/s");
        }
        return null;

    }
    Sprite GetSpecialTypeIcon(SpecialType specialType)
    {
        if (specialType == SpecialType.Rain)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/ll");
        }
        else if (specialType == SpecialType.Storm)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/t");
        }
        else if (specialType == SpecialType.Snow)
        {
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/n");
        }
        return null;

    }
    Sprite GetPowerImage(int power, UnitType unitType)
    {
        if (unitType == UnitType.Golden && power == 0)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g0");
        else if (unitType == UnitType.Golden && power == 1)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g1");
        else if (unitType == UnitType.Golden && power == 2)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g2");
        else if (unitType == UnitType.Golden && power == 3)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g3");
        else if (unitType == UnitType.Golden && power == 4)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g4");
        else if (unitType == UnitType.Golden && power == 5)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g5");
        else if (unitType == UnitType.Golden && power == 6)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g6");
        else if (unitType == UnitType.Golden && power == 7)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g7");
        else if (unitType == UnitType.Golden && power == 8)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g8");
        else if (unitType == UnitType.Golden && power == 9)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g9");
        else if (unitType == UnitType.Silver && power == 0)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s0");
        else if (unitType == UnitType.Silver && power == 1)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s1");
        else if (unitType == UnitType.Silver && power == 2)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s2");
        else if (unitType == UnitType.Silver && power == 3)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s3");
        else if (unitType == UnitType.Silver && power == 4)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s4");
        else if (unitType == UnitType.Silver && power == 5)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s5");
        else if (unitType == UnitType.Silver && power == 6)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s6");
        else if (unitType == UnitType.Silver && power == 7)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s7");
        else if (unitType == UnitType.Silver && power == 8)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s8");
        else if (unitType == UnitType.Silver && power == 9)
            return Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/s9");
        return null;
    }
}