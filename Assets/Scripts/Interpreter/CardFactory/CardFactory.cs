using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private DeckData deckData;
    public List<InterpretedEffect> effects;
    public static CardFactory Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ProcessInput()
    {

        string filePath = @"C:\Users\Barbaro\Documents\Personal\Estudio\Programación\Proyectos Pro\Proyectos de la escuela\Proyecto Gwent Pro\Gwwn-Pro (Segundo Proyecto)\Assets\Scripts\Interpreter\Input\input.txt";
        string input = File.ReadAllText(filePath);

        List<Token> listTokens = Lexer.LexicalAnalysis(input);

        Parser parser = new Parser(listTokens);
        List<Stmt> statements = parser.Parse();

        Interpreter interpreter = new Interpreter();
        interpreter.Interpret(statements);

        List<InterpretedCard> interpretedCards = interpreter.GetCards();

        effects = interpreter.GetEffects();

        ConvertInterpretedCards(interpretedCards, deckData); // Pasar deckData como parámetro
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

                AttackType attackType;
                Enum.TryParse(interpretedCard.Range, out attackType);
                unitCardData.AttackType = attackType;
             
                if (interpretedCard.Type == "Golden")
                    unitCardData.UnitType = UnitType.Golden;
                if (interpretedCard.Type == "Silver")
                    unitCardData.UnitType = UnitType.Silver;
               
                unitCardData.CardImage = Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Image/CardImage");
                
                unitCardData.TypeIcon = Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Type/m");
                
                unitCardData.PowerImage = Resources.Load<Sprite>("Imagenes del juego/Recursos 2/Power/g8");

                unitCardData.OnActivation = interpretedCard.OnActivation;
                cardData = unitCardData;
            }
            else
            {
                Debug.LogError("Tipo de carta desconocido: " + interpretedCard.Type);
                continue;
            }

            deckData.DeckCards.Add(cardData); // Agregar la carta al DeckData
        }
    }
}