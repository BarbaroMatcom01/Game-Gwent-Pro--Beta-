   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
       
  public partial class Action : InterpretedElement
    {
        public List<Token> ActionParams { get; }
       
        public Block ActionBlock { get; }
       
        public Action(List<Token> actionParams, Block actionBlock)
        {
            ActionParams = actionParams;
            ActionBlock = actionBlock;
        }
        public void InvokeAction(EffectInfo effectInfo ,params object[] args )
        {
            Interpreter interpreter = new Interpreter();
            Environment actionEnvironment = new Environment(interpreter.environment);
            
            for (int i = 0; i < ActionParams.Count; i++)
            {
                actionEnvironment.Define(ActionParams[i].Value, args[i]);
            }
            foreach (var param in effectInfo.Param)
            {   
                Debug.Log("el param es "+ param.Key);
                actionEnvironment.Define(param.Key,param.Value);
                Debug.Log($"No se esta anadiendo nada creo");
                
            }
            interpreter.ExecuteBlock(ActionBlock.Statements, actionEnvironment);
        }
    }